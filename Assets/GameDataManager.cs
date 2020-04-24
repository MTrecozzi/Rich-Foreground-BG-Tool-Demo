using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager S;
    public Transform[] waypoints;
    public float3[] wps;

    private void Awake()
    {
        if (S != null && S != this)
        {
            Destroy(gameObject);
        }
        else
        {
            S = this;
        }

        wps = new float3[waypoints.Length];
        for (int i = 0; i < waypoints.Length; i++)
        {
            wps[i] = waypoints[i].position;
        }
    }
}
