using System.Collections;
using System.Collections.Generic;

using Unity.Mathematics;
using Unity.Entities;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Collections;
using UnityEngine;

/**
 * Makes an entity's y position move in a sine curve.
 */
public class SineMoveSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        
        float deltaTime = Time.DeltaTime;

        var jobHandle = Entities
            .WithName("SineMoveSystem")
            .ForEach((ref TransformOffsetComponent offsetPos, ref SineCurveComponent sinComp) =>
            {
                if (!sinComp.disabled)
                {
                    sinComp.elapsedTime += deltaTime;
                    // frequency = speed of the offsetting, amp = length of our curve
                    offsetPos.Value.y = -math.sin(sinComp.elapsedTime * sinComp.frequency) * deltaTime * sinComp.amp;
                }

            })
            .Schedule(inputDeps);

        return jobHandle;
    }
}
