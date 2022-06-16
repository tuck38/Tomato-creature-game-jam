using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fishing : MonoBehaviour
{
    // Fishing
    [SerializeField] Casting casting;
    bool isCast = false;
    bool fishOn = false;

    // Scoring nonsense
    [SerializeField] Text scoring;
    [SerializeField] Text timeOnScreen;
    int score = 0;
    float time = 1200;
    float fishTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0)
        {
            // if fishTime has passed 
            if (fishTime <= 0)
            {
                // & your rod has been cast
                if (isCast == true)
                {
                    // Change the score
                    score++;
                }

                // Pick a random time
                fishTime = Random.Range(15, 30);
                Debug.Log(fishTime);
            }

            fishTime -= Time.deltaTime;
            time -= Time.deltaTime;
        }
        else
        {
            SceneManager.LoadScene(2);
        }

        scoring.text = score.ToString();
        timeOnScreen.text = ((int)time).ToString();
    }

    //check if the user has pressed on the rod ;)
    private void OnMouseDown()
    {
        if(isCast == false)
        {
            casting.castRod();
            isCast = true;
        }
        else
        {
            casting.reelInRod();
            isCast = false;
        }
    }
}
