using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    // GameObjects
    [SerializeField] GameObject speaker;
    [SerializeField] Image textBubble;
    [SerializeField] Text text;


    // Timers
    float time = 5;
    float dialogueTime = 45;
    float timeBetweenLetter = 0.2f;

    // String
    string chosenDialogue = "";
    string dialogueByLetter = "";
    int currentLetter = 0;
    bool joke= false;

    // Start is called before the first frame update
    void Start()
    {
        textBubble.enabled = false;
        text.enabled = false;

        textBubble.transform.position = new Vector3(Screen.width / 2, textBubble.rectTransform.rect.height / 2, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // If a certain amount of time passes
        if (time <= 0)
        {
            time = 15;

            // Display a speech bubble
            CreateBubble();

            // With random text
            text.text = "";

            // Give a little extra time if it's a joke
            if(joke)
            {
                dialogueTime = 75;
            }
        }

        // Get this tuned to a certain amount of time, like .25 seconds per letter
        if(textBubble.enabled && currentLetter < chosenDialogue.Length)
        {
            if(timeBetweenLetter <= 0)
            {
                text.text = TextSpeed();
            }

            timeBetweenLetter = timeBetweenLetter - Time.deltaTime;
        }

        // For x many seconds
        if(dialogueTime <= 0)
        {
            dialogueTime = 45;
            currentLetter = 0;
            timeBetweenLetter = 0.2f;

            textBubble.enabled = false;
            text.enabled = false;
        }

        // Manage the two dialogue timers
        if (textBubble.enabled && text.enabled)
        {
            dialogueTime = dialogueTime - Time.deltaTime;
        }
        else
        {
            time = time - Time.deltaTime;
        }
    }

    // Create speech bubble
    void CreateBubble()
    {
        // Display Bubble
        textBubble.enabled = true;
        text.enabled = true;

        chosenDialogue = "This is some text!";
    }

    // Display text one letter at a time
    string TextSpeed()
    {
        if(currentLetter < chosenDialogue.Length)
        {
            dialogueByLetter += chosenDialogue[currentLetter];
            Debug.Log(chosenDialogue[currentLetter]);
        }

        timeBetweenLetter = 0.2f;

        currentLetter++;
        return dialogueByLetter;
    }
}
