using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSceneButton : MonoBehaviour
{
    [SerializeField] int sceneIndex = 0; // Just going to put -1 so that it can switch back to the main menu easily

    // Just load the next scene based on what I toss in :)
    public void SwitchScene()
    {
        //SceneManager.LoadScene(sceneIndex);
        Debug.Log("Clicked!" + sceneIndex);
    }
}