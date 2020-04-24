using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class FlyAwayEvent : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Attempt Archetype Change");
            ChangeArchetype();
        }
    }

    private void ChangeArchetype()
    {
        World world = World.DefaultGameObjectInjectionWorld;

        EntityManager entityManager = world.GetExistingSystem<FlutterMoveSystem>().EntityManager;
        
        var NativeArrayOfEntities = world.GetExistingSystem<FlutterMoveSystem>().EntityManager.GetAllEntities();

        for (int i = 0; i < NativeArrayOfEntities.Length; i++)
        {
            entityManager.RemoveComponent<WayPointMoveComponent>(NativeArrayOfEntities[i]);
            entityManager.AddComponentData(NativeArrayOfEntities[i], new FlutterAwayComponent { target = new float3(30, 30, 0)});
        }

        NativeArrayOfEntities.Dispose();

    }
}
