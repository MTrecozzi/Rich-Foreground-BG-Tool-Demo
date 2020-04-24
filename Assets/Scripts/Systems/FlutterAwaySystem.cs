using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.Entities;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Collections;

public class FlutterAwaySystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;

        var jobHandle = Entities
           .WithName("FlutterAwaySystem")
           .ForEach((ref Translation position, ref FlutterAwayComponent fAwayComp) =>
           {
               position.Value += math.normalize(fAwayComp.target - position.Value) * deltaTime * 10;
               
               if(math.distance(position.Value, fAwayComp.target) < 1)
               {
                   // destroy entity - not sure how to do this
               }
           })
            .Schedule(inputDeps);

        return jobHandle;

    }
}
