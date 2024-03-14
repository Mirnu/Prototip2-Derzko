using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

public class JumpState : MovementState
{
    private float _force = 5;
    private bool isCanJump = true;

    private const string EarthLayer = "Earth";

    public JumpState(CharacterStateMachine characterStateMachine) :
        base(characterStateMachine) { }

    public override bool Enter()
    {
        if (!isCanJump) return false;
        Character.CollisionEnter += OnCollisionEnter;
        isCanJump = false;
        Rigidbody?.AddRelativeForce( Character.transform.up *  _force, ForceMode2D.Impulse);
        
        return true;
    }

    private void OnCollisionEnter(Collision2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer(EarthLayer)) return;
        isCanJump = true;
        Character.CollisionEnter -= OnCollisionEnter;
    }
}