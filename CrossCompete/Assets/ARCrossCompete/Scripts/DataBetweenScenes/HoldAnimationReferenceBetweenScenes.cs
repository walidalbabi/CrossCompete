using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class HoldAnimationReferenceBetweenScenes : MonoBehaviour
{
   public static HoldAnimationReferenceBetweenScenes instance;

    public string _AnimationID;
    [SerializeField]
    private GameObject LoadingPanel;
    void Awake()
    {
        if (instance == null)
        {
            instance = this; // In first scene, make us the singleton.
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
            Destroy(gameObject); // On reload, singleton already set, so destroy duplicate.
    }

    public void AnimationID(string animID)
    {
        StartCoroutine(Loading());

        _AnimationID = animID;
        SceneManager.LoadScene(2);
    }

    private IEnumerator Loading()
    {
        LoadingPanel.SetActive(true);
        yield return new WaitForSeconds(1f);
        LoadingPanel.SetActive(false);
    }
}
