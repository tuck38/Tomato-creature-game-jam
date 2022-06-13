using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to hold all the lines of dialogue
/// </summary>
public class DialogueData : ScriptableObject
{
	public List<string> JokeQuestionLines = new ();
	public List<string> JokeAnswerLines = new ();
	public List<string> AdviceLines = new();
	public List<string> ExtraLines = new ();
}
