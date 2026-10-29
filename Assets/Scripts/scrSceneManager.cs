using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scrSceneManager : MonoBehaviour
{

    public string levelToLoad;
    public Animator anim;

    public float autoLoadDelay;


    #region LOAD LEVEL ON DELAY==================================
   //to load a level on delay from a button, add both of the methods below to OnClick to set both level name and delay time
    public void SetDelayedLoad(string levelName)
    {
        levelToLoad = levelName;
    }
    public void LoadOnDelay(float delay)
    {
        StartCoroutine(LoadDelayedLevel(levelToLoad, delay));
    }

    IEnumerator LoadDelayedLevel(string levelName, float delay)
    {
        //if there's an animation to play, play it first
        if (anim != null)
        {
            anim.SetTrigger("trigPlay");
        }

        //wait then load scene
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(levelToLoad);

    }
    #endregion

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void Exit()
    {
        Application.Quit();
    }


}
