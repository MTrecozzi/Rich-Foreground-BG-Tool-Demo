using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;


public class GameMoveSystem : JobComponentSystem
{
    

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        
     float deltaTime = UnityEngine.Time.deltaTime;
        
        var jobHandle = Entities.ForEach((ref Translation translation, ref ParrallaxTag parrallaxTag) =>
        {
            
            translation.Value += new float3(-1, 0, 0) * deltaTime * 0.5f;
            
        }).Schedule(inputDeps);


        return jobHandle;
    }
}
