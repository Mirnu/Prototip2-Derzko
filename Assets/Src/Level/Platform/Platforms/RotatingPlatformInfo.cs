using Unity;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

[CreateAssetMenu(fileName = "RotatingPlatform", menuName = "Gameplay/Rotating platform")]
public class RotatingPlatformInfo : ScriptableObject
{
    [Header("������������ ���� ��������")]
    [Range(-360, 360)]
    [SerializeField] private float _maxRotationAngle;
    public float MaxRotationAngle
    {
        get => _maxRotationAngle; 
        protected set { 
            if (-360 <= value && value <= 360)
            {
                _maxRotationAngle = value;
            }
        }
    }

    [Header("����������� ���� ��������")]
    [Range(-360, 360)]
    [SerializeField] private float _minRotationAngle;
    public float MinRotationAngle
    {
        get => _minRotationAngle;
        protected set
        {
            if (-360 <= value && value <= 360)
            {
                _minRotationAngle = value;
            }
        }
    }

    [Header("����� �� ����������� �� �����")]
    [SerializeField] private bool _isReturnToStart;
    public bool IsReturnToStart
    {
        get => _isReturnToStart;
        protected set { _isReturnToStart = value; }
    }

    [Header("���������� ����� ��������� ������� � �������")]
    [SerializeField] private Vector2 _anchorPos;
    public Vector2 AnchorPos { 
        get => _anchorPos; 
        protected set { _anchorPos = value; } 
    }

    [Header("���������� ����� ��������� ������� � ����� (���������� ����������)")]
    [SerializeField] private Vector2 _connectedAnchorPos;
    public Vector2 ConnectedAnchorPos
    {
        get => _connectedAnchorPos;
        protected set { _connectedAnchorPos = value; }
    }

    [Header("Ƹ������� �������")]
    [SerializeField] private float _springFrecuency;
    public float SpringFrecuency
    {
        get => _springFrecuency;
        protected set {
            if (value >= 0)
                _springFrecuency = value;
            else
                _springFrecuency = 0;
        }
    }
}