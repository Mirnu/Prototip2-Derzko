using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelState : MonoBehaviour 
{
    public bool abortRepo = false;

    protected List<StateObject> _stateObjects = new List<StateObject>();
    public Dictionary<int, Dictionary<string, object>> _savedStateObjects = new Dictionary<int, Dictionary<string, object>>();

    [SerializeField] protected List<CheckPointBundle> checkPointBundles;
    [SerializeField] protected Transform spawnPoint;

    protected Character _character;
    protected Maid maid = new Maid();

    [Inject]
    public void Construct(Character character)
    {
        _character = character;
    }

    protected void StartMonitor()
    {
        if (abortRepo)
            PlayerPrefs.DeleteAll();
        else
            initLevelState();

        handleChangeLevelState();
        handleCheckPoints();
    }

    private void handleCheckPoints()
    {
        foreach (var checkPoint in checkPointBundles)
        {
            maid.GiveTask(
            checkPoint.Broadcaster.Subscribe("isActive", (newState, Prev) =>
            {
                if (!(bool)newState) return;
                spawnPoint = checkPoint.SpawnPoint;
                SaveCurrentLevelState();
            }));
        }
    }

    private void initLevelState()
    {
        Repository.LoadState();
        bool isGet = Repository.TryGetData(out LevelData data);
        if (!isGet) return;
        foreach (var state in data.stateObjects)
        {
            StateObject stateObject = _stateObjects.Find(x => x.ID == state.Key);
            if (stateObject == null) throw new Exception("StateObject is null");
            foreach (var _state in state.Value)
            {
                stateObject.ChangeObjectState(_state.Key, _state.Value); 
            }
        }
        _character.transform.position = data.spawnPoint.y != 0 && data.spawnPoint.x != 0  ? 
            new Vector2 (data.spawnPoint.x, data.spawnPoint.y) : 
            spawnPoint.position;
        SaveCurrentLevelState();
    }

    private void SaveCurrentLevelState()
    {
        foreach (var stateObject in _stateObjects)
        {
            _savedStateObjects[stateObject.ID] = stateObject.State;
        }
    }

    private void handleChangeLevelState()
    {
        foreach (var stateObject in _stateObjects)
        {
            stateObject.ObjectStateChanged += OnObjectStateChanged;
        }
    }

    private void OnObjectStateChanged(StateObject state)
    {
        _savedStateObjects[state.ID] = state.PrevState;
    }

    private void OnApplicationQuit()
    {
        maid.Clean();
        LevelData levelData = new LevelData { 
            stateObjects = _savedStateObjects,
            spawnPoint = new Victor { x = spawnPoint.position.x, y = spawnPoint.position.y }
        };
        Repository.SetData(levelData);
        Repository.SaveState();
    }
}

public struct LevelData
{
    public Dictionary<int, Dictionary<string, object>> stateObjects;
    public Victor spawnPoint; 
}
public struct Victor
{
    public float x, y;
}

[Serializable]
public struct CheckPointBundle
{
    public StateObject Broadcaster;
    public Transform SpawnPoint;
}