using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Zenject;

public interface Holdable
{
    public void OnPickedUp() { }
    public void OnPlacedDown() { }
    public GameObject ItemPrefab { get; }
}