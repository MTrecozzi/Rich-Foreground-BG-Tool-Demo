using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
public class FlyAwayPlayableBehaviour : PlayableBehaviour
{
    private bool firstFrameHappened = false;

    [SerializeField] private FlyAwayEvent _event;
    
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        var flyAway = playerData as FlyAwayEvent;

        if (flyAway == null)
        {
            Debug.Log("FLy Away Cast Fail");
            
            return;
        }

        if (!firstFrameHappened && EditorApplication.isPlaying)
        {
            flyAway.FlyAway();
        
        
            firstFrameHappened = true;
        }
    }
    

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        base.OnBehaviourPause(playable, info);
    }
    
}
