using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : MonoBehaviour
{

    [SerializeField] Casting casting;
    bool isCast = false;
    bool fishOn = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
