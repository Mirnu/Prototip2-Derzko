using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelState : MonoBehaviour 
{
    public bool abortRepo = false;

    protected List<StateObject> _stateObjects = new List<StateObject>();
    public Dictionary<int, Dictionary<string, object>> _savedStateObjects = new Dictionary<int, Dictionary<string, object>>();

    protected void StartMonitor()
    {
        if (abortRepo)
            PlayerPrefs.DeleteAll();
        else
            initLevelState();

        handleChangeLevelState();
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
        SaveCurrentLevelState();
    }

    public void SaveCurrentLevelState()
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
        LevelData levelData = new LevelData { stateObjects = _savedStateObjects };
        Repository.SetData(levelData);
        Repository.SaveState();
    }
}

public struct LevelData
{
    public Dictionary<int, Dictionary<string, object>> stateObjects;
}