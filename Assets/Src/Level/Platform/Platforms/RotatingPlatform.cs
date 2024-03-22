using Unity;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(HingeJoint2D))]
public class RotatingPlatform : Platform {

    [SerializeField] private RotatingPlatformInfo rotatingPlatformInfo;
    private HingeJoint2D _hingeJoint;
    private SpringJoint2D _springJoint;

    private void Awake()
    {
        _hingeJoint = gameObject.GetComponent<HingeJoint2D>();
        JointAngleLimits2D limits = new() { 
            max = rotatingPlatformInfo.MaxRotationAngle, 
            min = rotatingPlatformInfo.MinRotationAngle 
        };
        _hingeJoint.limits = limits;
        _hingeJoint.useLimits = true;
        if (rotatingPlatformInfo.IsReturnToStart)
        {
            _springJoint = gameObject.GetComponent<SpringJoint2D>();
            _springJoint.frequency = rotatingPlatformInfo.SpringFrecuency;
            _springJoint.anchor = rotatingPlatformInfo.AnchorPos;
            _springJoint.connectedAnchor = rotatingPlatformInfo.ConnectedAnchorPos;
        }
    }

}