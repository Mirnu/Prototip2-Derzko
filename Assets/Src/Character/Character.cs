using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class Character : MonoBehaviour
{
    public CharacterStateMachine _characterStateMachine { get; private set; }

    public Rigidbody2D Rigidbody { get; private set; }
    public event Action<Collision2D> CollisionEnter;

    public BoxCollider2D Collider { get; private set; }
    private IHandler _handler;
    
    public RaycastHit2D hit;

    public float Speed = 1.5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CollisionEnter?.Invoke(collision);
    }

    [Inject]
    public void Construct(IHandler handler)
    {
        _handler = handler;
        _characterStateMachine = new CharacterStateMachine(this, _handler);
        _characterStateMachine.Initialize();
    }

    private void Awake()
    {
        Collider = GetComponent<BoxCollider2D>();
        Rigidbody = GetComponent<Rigidbody2D>();
        _handler.PressedKey += KeyDown;
    }

    private void OnDestroy()
    {
        _handler.PressedKey -= KeyDown;
    }

    private void KeyDown(KeyCode keyCode)
    {
        if (Input.GetAxisRaw("Vertical") != 0)
            _characterStateMachine.ChangeState(_characterStateMachine.StairState);
        if (keyCode == KeyCode.Space)
        {
            _characterStateMachine.ChangeState(_characterStateMachine.JumpState);
            _characterStateMachine.ChangeState(_characterStateMachine.IdleState);
        }
        else if (keyCode == KeyCode.LeftControl)
        {
            _characterStateMachine.ChangeState(_characterStateMachine.DuckState);
        }

        Rigidbody.velocity = new Vector2(Input.GetAxis("Horizontal") * Speed, Rigidbody.velocity.y);
    }

    private void Update()
    {
        _characterStateMachine.Tick();
    }
    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, 0.5f), 0.5f);
    }
}
