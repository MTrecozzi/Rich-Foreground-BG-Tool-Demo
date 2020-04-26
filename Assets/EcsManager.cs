using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;

public class EcsManager : MonoBehaviour
{
    private EntityManager manager;
    private World world;
    

    public GameObject prefab;
    public GameObject bPrefab;
    private GameObjectConversionSettings settings;

    // Start is called before the first frame update
    void Start()
    {
        world = World.DefaultGameObjectInjectionWorld;
        manager = World.DefaultGameObjectInjectionWorld.EntityManager;
        settings = GameObjectConversionSettings.FromWorld(world, null);
    }

    void SpawnRandom()
    {

        for (int i = 0; i < 20; i++)
        {
            Entity entity = GameObjectConversionUtility.ConvertGameObjectHierarchy(prefab, settings);
        
            manager.SetComponentData(entity, new Translation { Value = new float3(UnityEngine.Random.Range(-15f, 15f), UnityEngine.Random.Range(-3f, 10f) , UnityEngine.Random.Range(5, 20))});
            manager.SetComponentData(entity, new TempMoveDemoTag { direction = 1});
            manager.Instantiate(entity);
        }
    }

    void SpawnTestButterfly()
    {
        // Creates entities that spawn in a random spot, with a random move and rotation speed, and a random wait time.
        for (int i = 0; i < 1; i++)
        {
            var entity = manager.Instantiate(GameObjectConversionUtility.ConvertGameObjectHierarchy(bPrefab, settings));

            manager.SetComponentData(entity, new Translation { Value = new float3(UnityEngine.Random.Range(-15f, 15f), 
                UnityEngine.Random.Range(0, 10f), UnityEngine.Random.Range(0, 0)) });
            manager.SetComponentData(entity, new Rotation { Value = quaternion.identity });
            manager.SetComponentData(entity, new WayPointMoveComponent { speed = UnityEngine.Random.Range(2, 8),
                currentWP = UnityEngine.Random.Range(0, GameDataManager.S.waypoints.Length)});
            manager.SetComponentData(entity, new WaitComponent { maxTime = UnityEngine.Random.Range(1f, 4f) });
            manager.SetComponentData(entity, new SineCurveComponent { frequency = UnityEngine.Random.Range(1, 2f),
                amp = UnityEngine.Random.Range(100, 200), startTime = Time.time });

        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.B))
        {
            SpawnTestButterfly();
        }
    }
}
