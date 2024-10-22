﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct SineCurveComponent : IComponentData
{
    public float frequency;
    public float amp;
    public bool disabled;
    public float elapsedTime;
}