using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class TempMoveSystem : SystemBase
{
    protected override void OnCreate()
    {
        Entities.ForEach((ref TempMoveDemoTag tag) => { tag.direction = 1; }).Run();
    }

    protected override void OnUpdate()
    {
        float deltaTime = UnityEngine.Time.deltaTime;
        
        Entities.ForEach((ref Translation translation, ref TempMoveDemoTag tempMoveDemoTag) =>
        {

            if (translation.Value.y < 0)
            {
                tempMoveDemoTag.direction = 1;
            } else if (translation.Value.y > 2)
            {
                tempMoveDemoTag.direction = -1;
            }
            
            translation.Value += new float3(0, 1, 0) * deltaTime * 3f * tempMoveDemoTag.direction;
            
        }).ScheduleParallel();

    }
}
