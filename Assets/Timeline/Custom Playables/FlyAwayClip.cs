using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class FlyAwayClip : PlayableAsset, ITimelineClipAsset
{

    [SerializeField]
    private FlyAwayPlayableBehaviour flyAwayPlayableBehaviour;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        return ScriptPlayable<FlyAwayPlayableBehaviour>.Create(graph, flyAwayPlayableBehaviour);
    }

    public ClipCaps clipCaps => ClipCaps.None;
}
