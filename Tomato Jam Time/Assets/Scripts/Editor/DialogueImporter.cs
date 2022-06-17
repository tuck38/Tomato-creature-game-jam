using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

/// <summary>
/// Class to import dialogue into easy Lists. NOTE: does not support formatted text
/// </summary>
public class DialogueImporter : EditorWindow
{
	TextAsset file;
	Dialogue dialogue;

	enum DialogueType	
	{
		None,
		Joke,
		Advice,
		Other
	}

	// Add menu item named "My Window" to the Window menu
	[MenuItem("Fun Tools/Dialogue Importer")]
	public static void ShowWindow()
	{
		//Show existing window instance. If one doesn't exist, make one.
		EditorWindow.GetWindow(typeof(DialogueImporter));
	}

	void OnGUI()
	{
		EditorGUILayout.LabelField("Some rules for import formatting:\n\n" +
			"Each Joke should follow the the format of: \"   * Joke Question?\"\n\"Joke Answer!\"\n\n" +
			"Each Advice and Other should be on their own line as: \"   * The line!\"\n\n" +
			"Be sure to remove any unnecessary lines (like random comments or stuff, or it'll get imported!)",
			GUILayout.ExpandHeight(true));

		file = (TextAsset)EditorGUILayout.ObjectField("File:", file, typeof(TextAsset), allowSceneObjects: true);

		EditorGUILayout.BeginHorizontal();
		if (file != null && Application.isEditor && !Application.isPlaying)
		{
			dialogue = (Dialogue)EditorGUILayout.ObjectField(dialogue, typeof(Dialogue), allowSceneObjects: true);
			if (GUILayout.Button("Import"))
			{
				ImportDialogue(file);
			}
		}
		EditorGUILayout.EndHorizontal();
	}

	/// <summary>
	/// Import dialogue from given file
	/// </summary>
	void ImportDialogue(TextAsset textFile)
	{
		Debug.Log("Hello");

		var filePath = AssetDatabase.GetAssetPath(textFile);

		try
		{
			using (var reader = new StreamReader(filePath))
			{
				// create scriptable object with required data
				DialogueType dType = DialogueType.None;
				dialogue.JokeQuestionLines = new List<string>();
				dialogue.JokeAnswerLines = new List<string>();
				dialogue.AdviceLines = new List<string>();
				dialogue.ExtraLines = new List<string>();

				string line;
				// the flipper switch for joke questions and answers
				bool jokeQuestion = true;
				while ((line = reader.ReadLine()) != null)
				{
					line = line.Trim(' ', '*');

					// Tabs in Google Docs convert to 3 spaces in a .txt file
					if (line.StartsWith("Dad jokes"))
					{
						// get rid of those spaces and asterisks
						dType = DialogueType.Joke;
						continue;
					}
					else if (line.StartsWith("Fishing/Dad advice"))
					{
						dType = DialogueType.Advice;
						continue;
					}
					else if (line.StartsWith("Other things"))
					{
						dType = DialogueType.Other;
						continue;
					}

					if (!string.IsNullOrEmpty(line))
					{
						switch (dType)
						{
							case DialogueType.Joke:
								{
									if (jokeQuestion)
									{
										dialogue.JokeQuestionLines.Add(line);
									}
									else
									{
										dialogue.JokeAnswerLines.Add(line);
									}
									jokeQuestion = !jokeQuestion;
									break;
								}
							case DialogueType.Advice:
								{
									dialogue.AdviceLines.Add(line);
									break;
								}
							case DialogueType.Other:
								{
									dialogue.ExtraLines.Add(line);
									break;
								}
							default:
								break;
						}
					}
				}

				AssetDatabase.SaveAssets();
			}
		}
		catch (Exception e)
		{
			Debug.LogError($"Failed to read file:/n{e.Message}");
		}
	}
}
