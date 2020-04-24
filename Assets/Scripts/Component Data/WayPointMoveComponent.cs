using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

[GenerateAuthoringComponent]
public struct WayPointMoveComponent : IComponentData
{
    public float speed;
    public float rotationSpeed;
    public int currentWP;
    public bool disable;
}
