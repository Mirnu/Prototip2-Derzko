using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelState : MonoBehaviour 
{
    protected List<StateObject> _stateObjects = new List<StateObject>();
    public Dictionary<int, Dictionary<string, object>> _savedStateObjects = new Dictionary<int, Dictionary<string, object>>();

    protected void StartMonitor()
    {
        initLevelState();
        handleChangeLevelState();
    }


    private void initLevelState()
    {
        var data = Repository.GetData<LevelData>();
        foreach (var state in data.stateObjects)
        {
            GameObject instance = EditorUtility.InstanceIDToObject(state.Key) as GameObject;
            StateObject stateObject = instance.GetComponent<StateObject>();
            foreach (var _state in state.Value)
            {
                stateObject.ChangeObjectState(_state.Key, _state.Value);
            }
        }
    }

    private void handleChangeLevelState()
    {
        foreach (var stateObject in _stateObjects)
        {
            stateObject.ObjectStateChanged += OnObjectStateChanged;
        }
    }

    private void OnObjectStateChanged(int id, Dictionary<string, object> prevState)
    {
        _savedStateObjects[id] = prevState;
    }

    private void OnApplicationQuit()
    {
        LevelData levelData = new LevelData { stateObjects = _savedStateObjects };
        Repository.SetData(levelData);
    }
}

public struct LevelData
{
    public Dictionary<int, Dictionary<string, object>> stateObjects;
}