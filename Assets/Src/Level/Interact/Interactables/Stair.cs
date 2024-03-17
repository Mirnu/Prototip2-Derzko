using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Character character))
        {
            character._characterStateMachine.StairState.canRise = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Character character))
        {
            character._characterStateMachine.StairState.canRise = false;
        }
    }
}
