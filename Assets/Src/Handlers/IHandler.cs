using System;
using UnityEngine;
using Zenject;

public interface IHandler
{
    public event Action<KeyCode> PressedKeyDown;
    public event Action<KeyCode> PressedKeyUp;
    public event Action<KeyCode> PressedKey;
}
