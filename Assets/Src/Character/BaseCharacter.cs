using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    public Humanoid Humanoid { get; private set; } = new Humanoid();
}
