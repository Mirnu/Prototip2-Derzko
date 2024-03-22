using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Rope : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Character character))
        {
            character._characterStateMachine.RopeState.canGrab = true;
            character.ropeBit = GetComponentsInChildren<HingeJoint2D>().Last();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Character character))
        {
            character._characterStateMachine.RopeState.canGrab = false;
            character.ropeBit = null;
        }
    }
}