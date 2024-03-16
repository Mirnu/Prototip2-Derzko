using System;
using UnityEngine;

interface IOnFlashlightAction
{
    Action OnFlashlightAction { get; protected set; }
}
