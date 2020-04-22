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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnRandom();
        }
    }
}
