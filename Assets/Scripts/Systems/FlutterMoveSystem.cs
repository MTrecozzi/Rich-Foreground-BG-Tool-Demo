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

        var jobHandle = Entities
            .WithName("MoveSystem")
            .ForEach((ref Translation position, ref Rotation rotation, ref WayPointMoveComponent wpMoveComp, ref WaitComponent waitComp) =>
            {
                /*
                 * I figure using a sine curve for the butterflies as they go from waypoint to waypoint
                 * would look the most butterfly-esque. Didn't implement that yet because math is hard 
                 * and this is a first draft.
                 */
                float3 heading = waypointPositions[wpMoveComp.currentWP] - position.Value;
                // the rotation and the z axis makes things a bit funky after a while... to be fixed
                quaternion targetDirection = quaternion.LookRotation(heading, math.up());
                rotation.Value = math.slerp(rotation.Value, targetDirection, deltaTime * wpMoveComp.rotationSpeed);
                position.Value += deltaTime * wpMoveComp.speed * math.forward(rotation.Value);

                // We've reached a waypoint!
                if (math.distance(position.Value, waypointPositions[wpMoveComp.currentWP]) < 3)
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
                        wpMoveComp.currentWP++; // Should probably have it pick a random index to head to next

                        if(wpMoveComp.currentWP >= waypointPositions.Length)
                        {
                            wpMoveComp.currentWP = 0;
                        }

                    } else
                    {
                        // at target and waiting
                        waitComp.curTime += deltaTime;
                    }
                }
            })
            .Schedule(inputDeps);

        waypointPositions.Dispose(jobHandle);

        return jobHandle;
    }

}
