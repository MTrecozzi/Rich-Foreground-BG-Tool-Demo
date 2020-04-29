using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class CustomClip : PlayableAsset, ITimelineClipAsset
{

    [SerializeField] private CustomBehaviour customBehaviour;

    [SerializeField] private Color light;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        return ScriptPlayable<CustomBehaviour>.Create(graph, customBehaviour);
    }

    public ClipCaps clipCaps => ClipCaps.None;
}
