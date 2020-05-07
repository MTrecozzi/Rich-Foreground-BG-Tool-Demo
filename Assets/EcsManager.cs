using System;
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

    private Entity fgSource;
    private Entity bgSource;

    private const int NumFgButterflies = 10;

    private float3[] fgStartingPos = new float3[NumFgButterflies];


    private void Awake()
    {
        
        world = World.DefaultGameObjectInjectionWorld;
        manager = World.DefaultGameObjectInjectionWorld.EntityManager;
        settings = GameObjectConversionSettings.FromWorld(world, null);
        
        fgSource = GameObjectConversionUtility.ConvertGameObjectHierarchy(bPrefab, settings);
        bgSource = GameObjectConversionUtility.ConvertGameObjectHierarchy(bgPrefab, settings);

        for (int i = 0; i < NumFgButterflies; i++)
        {
            bool half = i < NumFgButterflies / 2;
            
            float posX = half ? -15 : 15;
            
            fgStartingPos[i] = new float3(posX, UnityEngine.Random.Range(5f, 10f), 0);
            
        }
    }

    public void SpawnForegroundButterFlies()
    {
        // Creates entities that spawn in a random spot, with a random move and rotation speed, and a random wait time.
        
        var rand = new System.Random();
        

        for (int i = 0; i < 10; i++)
        {
            var entity = manager.Instantiate(fgSource);

            manager.SetComponentData(entity, new Translation { Value = fgStartingPos[i] });
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
        

        bool leftSide = true;

        int length = GameDataManager.S.bgWPS.Length;

        float zPos = GameDataManager.S.bgWPS[0].z;

        for (int i = 0; i < length; i++)
        {
            var entity = manager.Instantiate(bgSource);
            
            leftSide = !leftSide;
            
            float posX = leftSide ? -15 : 15;

            manager.SetComponentData(entity, new Translation { Value = new float3(posX, 
                UnityEngine.Random.Range(5f, 10f), zPos) });
            manager.SetComponentData(entity, new Rotation { Value = quaternion.identity });
            manager.SetComponentData(entity, new WayPointMoveComponent { speed = UnityEngine.Random.Range(1.5f, 3f),
                currentWP = UnityEngine.Random.Range(0, GameDataManager.S.waypoints.Length)});
            manager.SetComponentData(entity, new SineCurveComponent { frequency = 7f,
                amp = 150});
            
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
