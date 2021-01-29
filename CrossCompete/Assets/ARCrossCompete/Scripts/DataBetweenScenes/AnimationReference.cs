using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationReference : MonoBehaviour
{
    public void AnimationID(string animID)
    {
        HoldAnimationReferenceBetweenScenes.instance.AnimationID(animID);
    }
}
