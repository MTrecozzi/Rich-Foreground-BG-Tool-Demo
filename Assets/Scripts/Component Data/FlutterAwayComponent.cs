using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct FlutterAwayComponent : IComponentData
{
    public float3 target;
    public float speed;
    public bool disabled;

}
