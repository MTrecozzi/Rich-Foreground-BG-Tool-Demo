using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

/**
 * This struct can be applied to any entity that moves from one point to another.
 * Added a disable bool so if we want the butterflies to restart fluttering after being disturbed
 * we can just toggle that. (I have absolutely no idea if it would be easier to remove and readd the
 * component as needed)
 **/
[GenerateAuthoringComponent]
public struct WayPointMoveComponent : IComponentData
{
    public float speed;
    public int currentWP;
}
