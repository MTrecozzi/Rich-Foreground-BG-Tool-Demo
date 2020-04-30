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
    
    public GameObject bPrefab;
    public GameObject bgPrefab;
    private GameObjectConversionSettings settings;

    // Start is called before the first frame update
    void Start()
    {
        world = World.DefaultGameObjectInjectionWorld;
        manager = World.DefaultGameObjectInjectionWorld.EntityManager;
        settings = GameObjectConversionSettings.FromWorld(world, null);
    }

    public void SpawnForegroundButterFlies()
    {
        // Creates entities that spawn in a random spot, with a random move and rotation speed, and a random wait time.
        
        var rand = new System.Random();

        for (int i = 0; i < 10; i++)
        {
            var entity = manager.Instantiate(GameObjectConversionUtility.ConvertGameObjectHierarchy(bPrefab, settings));
            
            float posX = rand.NextDouble() > 0.5 ? -15 : 15;

            manager.SetComponentData(entity, new Translation { Value = new float3(posX, 
                UnityEngine.Random.Range(5f, 10f), UnityEngine.Random.Range(0, 0)) });
            manager.SetComponentData(entity, new Rotation { Value = quaternion.identity });
            manager.SetComponentData(entity, new WayPointMoveComponent { speed = UnityEngine.Random.Range(1.5f, 3f),
                currentWP = UnityEngine.Random.Range(0, GameDataManager.S.waypoints.Length)});
            manager.SetComponentData(entity, new WaitComponent { maxTime = UnityEngine.Random.Range(1f, 4f) });
            manager.SetComponentData(entity, new SineCurveComponent { frequency = UnityEngine.Random.Range(5, 10f),
                amp = UnityEngine.Random.Range(100, 200)});

        }
    }

    public void SpawnBackgroundButterflies()
    {
        var rand = new System.Random();


        var e = GameObjectConversionUtility.ConvertGameObjectHierarchy(bgPrefab, settings);

        bool leftSide = true;
        
        for (int i = 0; i < GameDataManager.S.bgWPS.Length; i++)
        {
            var entity = manager.Instantiate(e);
            
            leftSide = !leftSide;
            
            float posX = leftSide ? -15 : 15;

            manager.SetComponentData(entity, new Translation { Value = new float3(posX, 
                UnityEngine.Random.Range(5f, 10f), GameDataManager.S.bgWPS[0].z) });
            manager.SetComponentData(entity, new Rotation { Value = quaternion.identity });
            manager.SetComponentData(entity, new WayPointMoveComponent { speed = UnityEngine.Random.Range(1.5f, 3f),
                currentWP = UnityEngine.Random.Range(0, GameDataManager.S.waypoints.Length)});
            manager.SetComponentData(entity, new SineCurveComponent { frequency = UnityEngine.Random.Range(5, 10f),
                amp = UnityEngine.Random.Range(100, 200)});
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.B))
        {
            SpawnForegroundButterFlies();
        }
    }
}
