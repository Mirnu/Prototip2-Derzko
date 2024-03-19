using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightMovePlatform : Platform
{
    private const string playerLayer = "Player";
    private const string holdableLayer = "Holdable";

    private bool hasMoved = false;

    private void OnCollisionEnter2D(Collision2D other) {
        if((other.collider.gameObject.layer != LayerMask.NameToLayer(playerLayer) && other.collider.gameObject.layer != LayerMask.NameToLayer(holdableLayer))
         || hasMoved) return;
        Move();
        hasMoved = true;
    }
}
