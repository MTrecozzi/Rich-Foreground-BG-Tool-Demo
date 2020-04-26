using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.Entities;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Collections;

/**
 * Makes entities flutter...sort of.
 * 
 **/
public class FlutterMoveSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;
        NativeArray<float3> waypointPositions = new NativeArray<float3>(GameDataManager.S.wps,
            Allocator.TempJob);
        var rnd = new Unity.Mathematics.Random((uint)UnityEngine.Random.Range(1, 100000));

        var jobHandle = Entities
            .WithName("MoveSystem")
            .ForEach((ref Translation position, ref WayPointMoveComponent wpMoveComp, ref WaitComponent waitComp, ref SineCurveComponent sinComp) =>
            {
                
                float3 heading = waypointPositions[wpMoveComp.currentWP] + new float3(0, 0, -0.5f) - position.Value;
                quaternion targetDirection = quaternion.LookRotation(heading, math.up());
                position.Value += deltaTime * (wpMoveComp.speed * math.normalize(heading));

                // We've reached a waypoint!
                if (math.distance(position.Value, waypointPositions[wpMoveComp.currentWP]) < 1)
                {
                    if (!waitComp.waiting)
                    {
                        // just arrived at target
                        waitComp.waiting = true;

                    } else if (waitComp.waiting && waitComp.curTime >= waitComp.maxTime)
                    {
                        // at target and done waiting
                        waitComp.waiting = false;
                        waitComp.curTime = 0;
                        sinComp.disabled = false;

                        waitComp.maxTime = rnd.NextFloat(.5f, 5);
                        wpMoveComp.currentWP = rnd.NextInt(0, waypointPositions.Length);

                    } else
                    {
                        // at target and waiting
                        waitComp.curTime += deltaTime;
                        sinComp.disabled = true;
                        
                    }
                }
            })
            .Schedule(inputDeps);

        waypointPositions.Dispose(jobHandle);

        return jobHandle;
    }

}
