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
        for (int i = 0; i < 3; i++)
        {
            var entity = manager.Instantiate(GameObjectConversionUtility.ConvertGameObjectHierarchy(bPrefab, settings));

            manager.SetComponentData(entity, new Translation { Value = new float3(UnityEngine.Random.Range(-15f, 15f), 
                UnityEngine.Random.Range(2f, 10f), UnityEngine.Random.Range(5, 20)) });
            manager.SetComponentData(entity, new Rotation { Value = quaternion.identity });
            manager.SetComponentData(entity, new WayPointMoveComponent { speed = UnityEngine.Random.Range(2, 8),
                rotationSpeed = UnityEngine.Random.Range(1, 10)});
            manager.SetComponentData(entity, new WaitComponent { maxTime = UnityEngine.Random.Range(.5f, 2f) });
        
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnRandom();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            SpawnTestButterfly();
        }
    }
}
