using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

/**
 * This struct can be used for anything that needs to chill for a bit.
 **/
[GenerateAuthoringComponent]
public struct WaitComponent : IComponentData
{
    public float curTime;
    public float maxTime;
    public bool waiting;
}
