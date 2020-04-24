using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.Entities;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Collections;

public class MoveSystem : JobComponentSystem
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
                float3 heading = waypointPositions[wpMoveComp.currentWP] - position.Value;
                quaternion targetDirection = quaternion.LookRotation(heading, math.up());
                rotation.Value = math.slerp(rotation.Value, targetDirection, deltaTime * wpMoveComp.rotationSpeed);
                position.Value += deltaTime * wpMoveComp.speed * math.forward(rotation.Value);

                if (math.distance(position.Value, waypointPositions[wpMoveComp.currentWP]) < 2)
                {
                    if (!waitComp.waiting)
                    {
                        // at target but not waiting yet
                        waitComp.waiting = true;

                    } else if (waitComp.waiting && waitComp.curTime >= waitComp.maxTime)
                    {
                        // at target and done waiting
                        waitComp.waiting = false;
                        waitComp.curTime = 0;
                        wpMoveComp.currentWP++;

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
