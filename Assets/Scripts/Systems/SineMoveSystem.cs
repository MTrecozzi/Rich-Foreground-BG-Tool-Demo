using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.Entities;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Collections;

public class SineMoveSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;
        float age = UnityEngine.Time.time - deltaTime;

        var jobHandle = Entities
            .WithName("SineMoveSystem")
            .ForEach((ref Translation position, ref SineCurveComponent sinComp) =>
            {
                sinComp.y0 = position.Value.y;
                
                float sin = Mathf.Sin(Mathf.PI * .5f * age / sinComp.amp);
                position.Value.y = sinComp.y0 + sinComp.frequency * sin;


            })
            .Schedule(inputDeps);

        return jobHandle;
    }
}
