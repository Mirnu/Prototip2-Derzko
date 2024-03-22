using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public List<HingeJoint2D> ropeParts = new List<HingeJoint2D>();

    private void Awake() {
        ropeParts = GetComponentsInChildren<HingeJoint2D>().ToList();
        Debug.Log("lr: " + ropeParts.Last().name);
    }
}
