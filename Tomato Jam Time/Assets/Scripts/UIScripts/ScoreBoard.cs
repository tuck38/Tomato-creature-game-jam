using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] Image fishUI;
    [SerializeField] Text score;
    [SerializeField] Text timer;

    // Start is called before the first frame update
    void Start()
    {
        fishUI.transform.position = new Vector3(35, Screen.height - 20, 0);
        timer.transform.position = new Vector3(Screen.width + 30, Screen.height - 20, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
