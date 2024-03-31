using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro.EditorUtilities;
using UnityEngine;

public class HiddenPlatform : Platform
{
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _collider;
    [SerializeField]
    private string platfomShowState;
    public string PlatfomShowState { get => platfomShowState; private set { platfomShowState = value; } }
    private void Awake()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _collider = gameObject.GetComponent<BoxCollider2D>();
        _spriteRenderer.enabled = false;
        _collider.isTrigger = true;
    }

    public void Show()
    {
        _spriteRenderer.enabled = true;
        _collider.isTrigger = false;      
    }

    public void Hide()
    {
        _spriteRenderer.enabled = false;
        _collider.isTrigger = true;
    }
}
