using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARManager : MonoBehaviour
{
  

    private HoldAnimationReferenceBetweenScenes HARBS;

    private Animator anim;


private void OnEnable()
    {
        HARBS = GameObject.Find("LoadingUI").GetComponent<HoldAnimationReferenceBetweenScenes>();
        anim = GetComponent<Animator>();

        anim.SetInteger("Move", int.Parse(HARBS._AnimationID));
    
    }

}
