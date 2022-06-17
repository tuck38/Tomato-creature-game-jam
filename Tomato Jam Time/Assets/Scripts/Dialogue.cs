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
	public List<string> JokeQuestionLines = new();
	public List<string> JokeAnswerLines = new();
	public List<string> AdviceLines = new();
	public List<string> ExtraLines = new();
	[SerializeField] AudioSource sound;
    [SerializeField] float volume = 0.4f;

    // Timers
    float time = 45;
    float dialogueTime = 30;
    float timeBetweenLetter = 0.2f;

    // String
    string chosenDialogue = "";
    string dialogueByLetter = "";
    int currentLetter = 0;
    // Types
    List<string> advice = new List<string>();
    List<string> extra = new List<string>();
    List<string> jokes = new List<string>();
    List<string> jokeEnds = new List<string>();
    int ranType = 1;
    int ranDialogue = 0;
    bool jokeFinished = false;


    // Start is called before the first frame update
    void Start()
    {
        textBubble.enabled = false;
        text.enabled = false;

        textBubble.transform.position = new Vector3(Screen.width / 2, textBubble.rectTransform.rect.height / 2 - 1, 0);
        advice = AdviceLines;
        extra = ExtraLines;
        jokes = JokeQuestionLines;
        jokeEnds = JokeAnswerLines;
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

            // Determine dialogue type w/ random line
            ranType = (int)Random.Range(0, 2);

            switch (ranType)
            {
                // Advice
                case (0):
                {
                    ranDialogue = (int)Random.Range(0, advice.Count - 1);
                    chosenDialogue = advice[ranDialogue];
                    break; 
                }

                // Joke
                case (1):
                {
                    ranDialogue = (int)Random.Range(0, jokes.Count - 1);
                    chosenDialogue = jokes[ranDialogue];
                    // Give extra time for joke
                    dialogueTime = 20;
                    break;
                }

                // Extra
                case (2):
                {
                    ranDialogue = (int)Random.Range(0, extra.Count - 1);
                    chosenDialogue = extra[ranDialogue];
                    break;
                }
            }
        }

        // Get this tuned to a certain amount of time, like .25 seconds per letter
        if(textBubble.enabled)
        {
                if (timeBetweenLetter <= 0)
                {
                    text.text = TextSpeed();
                }

            timeBetweenLetter = timeBetweenLetter - Time.deltaTime;
        }

        // Start end of joke
        if (dialogueTime <= 0 && !jokeFinished && ranType == 1)
        {
            dialogueTime = 45;
            currentLetter = 0;
            timeBetweenLetter = 0.2f;

            chosenDialogue = jokeEnds[ranDialogue];
            dialogueByLetter = "";

            jokeFinished = true;
            dialogueByLetter = "";
        }
        // For x many seconds
        else if (dialogueTime <= 0)
        {
            dialogueTime = 45;
            currentLetter = 0;
            timeBetweenLetter = 0.2f;

            textBubble.enabled = false;
            text.enabled = false;
            jokeFinished = false;
            dialogueByLetter = "";
        }

        // Manage the dialogue timers
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
    }

    // Display text one letter at a time
    string TextSpeed()
    {
        if (currentLetter < chosenDialogue.Length)
        {
            dialogueByLetter += chosenDialogue[currentLetter];
        }

        timeBetweenLetter = 0.2f;

        currentLetter++;
        return dialogueByLetter;
    }
}
