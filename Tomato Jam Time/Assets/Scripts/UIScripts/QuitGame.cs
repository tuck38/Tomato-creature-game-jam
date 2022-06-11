using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // Just quit the game! easy :)
    public void Quit()
    {
        Application.Quit();
        Debug.Log("QUIT!");
    }
}
