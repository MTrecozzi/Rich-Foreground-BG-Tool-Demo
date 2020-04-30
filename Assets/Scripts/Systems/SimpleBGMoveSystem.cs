using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Unity.Mathematics;
using Unity.Entities;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Collections;
using Random = System.Random;

/**
 * Makes entities flutter...sort of.
 * 
 **/
public class SimpleBGMoveSystem : JobComponentSystem
{

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;
        NativeArray<float3> waypointPositions = new NativeArray<float3>(GameDataManager.S.bgWPS,
            Allocator.TempJob);
        var rnd = new Unity.Mathematics.Random((uint)UnityEngine.Random.Range(1, 100000));

        float pointOffset = 0.3f;

        var jobHandle = Entities
            .WithName("FlutterMoveSystem")
            .ForEach((ref Translation position, ref WayPointMoveComponent wpMoveComp, ref SineCurveComponent sinComp, ref BGMoveComponent bgMoveComponent) =>
            {
                float3 heading = waypointPositions[wpMoveComp.currentWP] + new float3(0, 0, -pointOffset) - position.Value;
                quaternion targetDirection = quaternion.LookRotation(heading, math.up());


                    position.Value += deltaTime * (wpMoveComp.speed * math.normalize(heading));

                    // We've reached a waypoint!
                if (math.distance(position.Value, waypointPositions[wpMoveComp.currentWP] + new float3(0, 0, -pointOffset)) < 0.1f)
                {
                    wpMoveComp.currentWP = rnd.NextInt(0, waypointPositions.Length);
                }
            })
            .Schedule(inputDeps);

        waypointPositions.Dispose(jobHandle);

        return jobHandle;
    }

}