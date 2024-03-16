using UnityEngine;

public interface Holdable
{
    public void OnPickedUp() { }
    public void OnPlacedDown() { }
    public GameObject ItemPrefab { get; }
}