using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

[GenerateAuthoringComponent]
public struct WaitComponent : IComponentData
{
    public float curTime;
    public float maxTime;
    public bool waiting;
}
