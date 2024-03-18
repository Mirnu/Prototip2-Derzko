#if UNITY_EDITOR
#define INSIDE_UNITY
#endif

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
#if INSIDE_UNITY
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor.PackageManager;
#endif

namespace Needle.AutoInstaller
{
    public static partial class ManifestTools
    {
        private const string ScopedRegistriesKey = "scopedRegistries";
        private const string PackagesKey = "dependencies";
        private const int JsonIndentSpaceCount = 2;
        
        #region Packages
        
        // TODO this is currently a relatively slow call used in many places
        public static bool ContainsPackage(this JSONObject manifest, string packageName, string value = null)
        {
            if (!manifest.HasKey(PackagesKey))
                return false;

            var packages = manifest[PackagesKey].AsObject;
            if (!packages.HasKey(packageName))
                return false;

            if (!string.IsNullOrEmpty(value) && packages[packageName] != value)
                return false;

            return true;
        }

        public static void AddOrUpdatePackage(this JSONObject manifest, string packageName, string value)
        {
            if (!manifest.HasKey(PackagesKey))
            {
                Logging.LogError("Wow that is so weird. You don't have a valid manifest. We'll make one for you!");
                manifest.Add(PackagesKey, new JSONArray());
            }

            var packages = manifest[PackagesKey].AsObject;
            if (!packages.HasKey(packageName))
                packages.Add(packageName, value);
            else
                packages[packageName] = value;
        }

        public static void RemovePackage(this JSONObject manifest, string packageName)
        {
            if (!manifest.HasKey(PackagesKey))
            {
                return;
            }

            JSONNode toRemove = null;
            var packages = manifest[PackagesKey].AsObject;
            foreach (var kvp in packages)
            {
                if (kvp.Key == packageName)
                {
                    toRemove = kvp.Value;
                    break;
                }
            }

            if (toRemove != null)
                manifest[PackagesKey].Remove(toRemove);
        }

        public static Dictionary<string, string> GetPackageDict(this JSONObject manifest)
        {
            if (!manifest.HasKey(PackagesKey))
                return null;
            
            var packages = manifest[PackagesKey].AsObject;

            var dict = new Dictionary<string, string>();
            foreach (var kvp in packages)
            {
                dict.Add(kvp.Key, kvp.Value.Value);
            }
            return dict;
        }
        
        public static IEnumerable<KeyValuePair<string, JSONNode>> GetPackages(this JSONObject manifest)
        {
            if (!manifest.HasKey(PackagesKey))
                yield break;
            
            var packages = manifest[PackagesKey].AsObject;
            foreach (var kvp in packages)
            {
                yield return kvp;
            }
        }
        
        #endregion
        
        #region Scoped Registries
        
        public static bool ContainsRegistry(this JSONObject manifest, RegistryInfo registryInfo)
        {
            if (!manifest.HasKey(ScopedRegistriesKey))
            {
                return false;
            }

            JSONArray registries = manifest[ScopedRegistriesKey].AsArray;
            foreach (JSONNode regNode in registries)
            {
                RegistryInfo regInfo = RegistryInfo.FromJson(regNode.AsObject);
                if (registryInfo.Equals(regInfo))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool AddOrUpdateRegistry(this JSONObject manifest, RegistryInfo registryInfo)
        {
            if (!manifest.HasKey(ScopedRegistriesKey))
                manifest.Add(ScopedRegistriesKey, new JSONArray());

            JSONArray registriesJson = manifest[ScopedRegistriesKey].AsArray;
            var registries = registriesJson.Children
                .Select(x => RegistryInfo.FromJson((JSONObject) x))
                .ToList();
            
            // checks:
            // - registry with same name: forbidden.
            var sameNameRegistry = registries.FirstOrDefault(x => x.Name.Equals(registryInfo.Name, StringComparison.InvariantCultureIgnoreCase));
            if (sameNameRegistry.IsValid)
            {
                var jsonReg = registriesJson[registries.IndexOf(sameNameRegistry)];
                //   - if same URL: add scopes
                if (sameNameRegistry.Url == registryInfo.Url) {
                    var arr = new JSONArray();
                    foreach (var newScope in sameNameRegistry.Scopes.Union(registryInfo.Scopes).Distinct())
                        arr.Add(new JSONString(newScope));
                    jsonReg["scopes"] = arr;
                    return true;
                }
                
                //   - else: change name and add (?) or throw error
                Logging.LogError("[Manifest Tools] Couldn't add scoped registry " + registryInfo.Name + " at " + registryInfo.Url + ": Registry with same name but different URL already exists.");
                return false;
            }
            
            // collect all existing scopes
            var existingScopes = registries.SelectMany(x => x.Scopes).ToList();
            foreach (var existing in existingScopes)
            {
                if (registryInfo.Scopes.Contains(existing))
                {
                    Logging.LogWarning("[Manifest Tools] The scope " + existing + " from " + registryInfo.Url + " already exists in the manifest. It won't be added again. Please make sure you're getting the package from the right place!");
                    registryInfo.Scopes.Remove(existing);
                }
            }
            
            // - other registry with same scope: forbidden
            registriesJson.Add(registryInfo.ToJson());
            return true;
        }

        public static void RemoveRegistry(this JSONObject manifest, string registryName)
        {
            if (!manifest.HasKey(ScopedRegistriesKey))
                return;

            JSONArray registries = manifest[ScopedRegistriesKey].AsArray;
            for (int i = registries.Count - 1; i >= 0; i--)
            {
                RegistryInfo reg = RegistryInfo.FromJson(registries[i].AsObject);
                if (reg.Name == registryName)
                {
                    registries.Remove(i);
                }
            }
        }

        public static IEnumerable<RegistryInfo> GetRegistries(this JSONObject manifest)
        {
            if (!manifest.HasKey(ScopedRegistriesKey))
                yield break;
            
            JSONArray registries = manifest[ScopedRegistriesKey].AsArray;
            for (int i = 0; i < registries.Count; i++)
            {
                RegistryInfo reg = RegistryInfo.FromJson(registries[i].AsObject);
                yield return reg;
            }
        }

        #endregion
        
        #region Package Handling
        
        #if INSIDE_UNITY
        internal static async Task<string> TryGetLatestVersionFromPackMan(string packageName, bool includePreview = false)
        {
            await Task.Delay(100);
            var req = Client.Search(packageName);
            while (!req.IsCompleted) await Task.Delay(100);
            if (req.Status == StatusCode.Success)
            {
                // assuming results are ordered by version
                foreach (var pack in req.Result)
                {
                    foreach (var ver in pack.versions.all.OrderByDescending(x => (SemVersion) x))
                    {
                        // if we include previews just take the first
                        if(includePreview)
                            return ver;
                        // otherwise take the first that is not a preview version
                        if (!ver.Contains("preview"))
                            return ver;
                    }
                }
            }

            if (!includePreview)
            {
                // if we didnt find a latest version try again including preview versions
                return await TryGetLatestVersionFromPackMan(packageName, true);
            }

            return null;
        }
        #endif

        internal static void PurgePackage(string manifestPath, string packageName, string registryName)
        {
            var manifest = ReadManifest(manifestPath);
            RemovePackage(manifest, packageName);
            manifest.WriteManifest(manifestPath);
            
            void PurgeFolder(string path, string folderSearchName)
            {
                if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
                {
                    // TODO this is a pretty broad search, should be more exact
                    // Library/PackageCache has format name@version
                    // other folders have format name only
                    var folders = Directory.GetDirectories(path)?.Where(x => x != null && x.Contains(folderSearchName));
                    foreach (var f in folders)
                    {
                        if (Directory.Exists(f))
                            Directory.Delete(f, true);
                    }
                }
            }

            // delete the package from all caches
            var libraryPath = Path.GetDirectoryName(Path.GetDirectoryName(manifestPath)) + "/Library/PackageCache/";
            PurgeFolder(libraryPath, packageName);
            PurgeFolder(Environment.GetEnvironmentVariable("LOCALAPPDATA") + "/Unity/cache/npm/" + registryName, packageName);
            PurgeFolder(Environment.GetEnvironmentVariable("LOCALAPPDATA") + "/Unity/cache/packages/" + registryName, packageName);
        }

        #endregion
        
        #if INSIDE_UNITY
        public static string ProjectManifestPath => Path.GetDirectoryName(Application.dataPath) + "/Packages/manifest.json";
        #endif

        public static JSONObject ReadManifest(string manifestPath = null)
        {
#if INSIDE_UNITY
            if (string.IsNullOrEmpty(manifestPath)) manifestPath = ProjectManifestPath;
#endif
            if (string.IsNullOrEmpty(manifestPath) || !File.Exists(manifestPath)) return null;
            
            var manifestString = File.ReadAllText(manifestPath);
            var manifestJson = JSONNode.Parse(manifestString).AsObject;

            return manifestJson;
        }

        public static void WriteManifest(this JSONObject newJson, string manifestPath = null)
        {
#if INSIDE_UNITY
            if (string.IsNullOrEmpty(manifestPath)) manifestPath = ProjectManifestPath;
#endif
            if (string.IsNullOrEmpty(manifestPath)) return;
            
            var jsonString = newJson.ToString(JsonIndentSpaceCount);
            File.WriteAllText(manifestPath, jsonString);
        }

        public static JSONObject GetDefaultManifest()
        {
            var manifest = new JSONObject
            {
                ["scopedRegistries"] = new JSONArray(),
                ["dependencies"] = new JSONObject()
            };
            
            return manifest;
        }
    }
    
    [Serializable]
    public struct RegistryInfo
    {
        public const string NAME_KEY = "name";
        public const string URL_KEY = "url";
        public const string SCOPES_KEY = "scopes";

        public string Name;
        public string Url;
        public List<string> Scopes;

        public bool IsValid => !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Url);

        public static explicit operator RegistryInfo(UnityEditor.PackageManager.RegistryInfo rhs)
        {
            if (rhs == null) throw new NullReferenceException("Registry Info is null, can't convert");
            
            return new RegistryInfo
            {
                Name = rhs.name, 
                Url = rhs.url, 
                Scopes = new List<string>()
            };
        }
        
        public static RegistryInfo FromJson(JSONObject json)
        {
            RegistryInfo info = new RegistryInfo
            {
                Name = json[NAME_KEY],
                Url = json[URL_KEY],
                Scopes = new List<string>(),
            };

            foreach (JSONNode node in json[SCOPES_KEY].AsArray)
            {
                info.Scopes.Add(node);
            }

            return info;
        }

        public static JSONObject ToJson(RegistryInfo info)
        {
            JSONObject json = new JSONObject();
            json[NAME_KEY] = info.Name;
            json[URL_KEY] = info.Url;

            JSONArray scopes = new JSONArray();
            foreach (string scope in info.Scopes)
            {
                scopes.Add(new JSONString(scope));
            }

            json[SCOPES_KEY] = scopes;

            return json;
        }

        public JSONObject ToJson()
        {
            return ToJson(this);
        }

        public bool Equals(RegistryInfo other)
        {
            return Name == other.Name && Url == other.Url && Scopes.SequenceEqual(other.Scopes);
        }

        public override bool Equals(object obj)
        {
            return obj is RegistryInfo other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Url != null ? Url.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Scopes != null ? Scopes.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
    
    public static class Logging
    {
        public static void LogWarning(object content)
        {
#if INSIDE_UNITY
            UnityEngine.Debug.LogWarning(content);
#else
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(content);
            Console.ResetColor();
#endif
        }
        
        public static void Log(object content)
        {
#if INSIDE_UNITY
            UnityEngine.Debug.Log(content);
#else
            Console.WriteLine(content);
#endif
        }

        public static void LogError(object content)
        {
#if INSIDE_UNITY
            UnityEngine.Debug.LogError(content);
#else
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(content);
            Console.ResetColor();
#endif
        }
    }
}