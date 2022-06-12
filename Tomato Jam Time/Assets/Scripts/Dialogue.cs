using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    // GameObjects
    [SerializeField] GameObject speaker;
    [SerializeField] Canvas canvas;
    [SerializeField] Image textBubble;
    [SerializeField] Text textType;
    Image dialogueBubble;
    Text dialogueText;

    // Timers
    float time = 5;
    float dialogueTime = 45;

    // String
    string chosenDialogue = "";
    string dialogueByLetter = "";
    int currentLetter = 0;

    // Start is called before the first frame update
    void Start()
    {
        
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
            dialogueText.text = "";
        }

        // Get this tuned to a certain amount of time, like .25 seconds per letter
        if(dialogueText != null && currentLetter < chosenDialogue.Length)
        {
            dialogueText.text = TextSpeed();
        }

        // For x many seconds
        if(dialogueTime <= 0)
        {
            // Doesn't fully delete...
            Destroy(dialogueText);
            Destroy(dialogueBubble);
            Debug.Log("Deleted!");

            dialogueTime = 45;
            currentLetter = 0;
        }

        // Manage the two dialogue timers
        if (dialogueBubble != null && dialogueText != null)
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
        // Create bubble
        dialogueBubble = Instantiate<Image>(textBubble);
        dialogueText = Instantiate<Text>(textType);

        // Set positions
        Vector3 pos = new Vector3(speaker.transform.position.x + 30, speaker.transform.position.y, speaker.transform.position.z);
        Debug.Log(pos);

        Debug.Log(dialogueText.transform.position);
        dialogueText.transform.position = Vector3.zero;
        dialogueBubble.transform.position = Vector3.zero;

        // Add to canvas
        dialogueBubble.transform.SetParent(canvas.transform);
        dialogueText.transform.SetParent(canvas.transform);

        chosenDialogue = "This is some text!";

    }

    // Display text one letter at a time - STRETCH GOAL
    string TextSpeed()
    {
        if(currentLetter < chosenDialogue.Length)
        {
            dialogueByLetter += chosenDialogue[currentLetter];
            Debug.Log(chosenDialogue[currentLetter]);
        }

        currentLetter++;
        return dialogueByLetter;
    }
}
