using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class KeyboardHandler : IHandler, ITickable
{
    public event Action<KeyCode> PressedKeyDown;
    public event Action<KeyCode> PressedKeyUp;
    public event Action<KeyCode> PressedKey;

    private readonly List<KeyCode> _allowedKeys = new List<KeyCode>() { 
        KeyCode.D, 
        KeyCode.W, 
        KeyCode.A, 
        KeyCode.S,
        KeyCode.LeftShift,
        KeyCode.Space,
        KeyCode.LeftControl,
        KeyCode.E,
        KeyCode.F,
        KeyCode.Q
    };

    public void Tick()
    {
        foreach (var key in _allowedKeys)
        {
            handleUpKeys(key);
            handleDownKeys(key);
            handleKeys(key);
        }
    }

    private void handleUpKeys(KeyCode keyCode)
    {
        if (Input.GetKeyUp(keyCode))
            PressedKeyUp?.Invoke(keyCode);
        
    }

    private void handleDownKeys(KeyCode keyCode)
    {
        if (Input.GetKeyDown(keyCode))
            PressedKeyDown?.Invoke(keyCode);
    }

    private void handleKeys(KeyCode keyCode)
    {
        if (Input.GetKey(keyCode))
            PressedKey?.Invoke(keyCode);
    }
}

