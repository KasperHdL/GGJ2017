using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO; 
using UnityEngine;
using UnityEngine.UI;
public class HighscoreManager : MonoBehaviour 
{
	public Text highscoresText;
	[SerializeField]
	private string highscoresFile;
	private List<Highscore> highscores;
	void Start() 
	{
		highscores = LoadHighscores(highscoresFile);
		SortHighscores();
		UpdateHighscores();
	}

	private List<Highscore> LoadHighscores(string fileName)
	{
		List<Highscore> highscores = new List<Highscore>();
		try
		{
			string line;
			StreamReader theReader = new StreamReader(fileName, Encoding.Default);
			using (theReader)
			{
				do
				{
					line = theReader.ReadLine();
						
					if (line != null)
					{
						string[] score = line.Split(',');
						if (score.Length > 0)
						{
							int scoreOut;
							if (int.TryParse(score[1], out scoreOut)) 
							{
								Highscore highscore = new Highscore(scoreOut, score[0]);
								highscores.Add(highscore);
							}
						}
					}
				} while (line != null);

			theReader.Close();
			}
		}
		catch
		{
			print("Error loading highscores - check if file exists.");
		}
		return highscores;
	}

	private void SortHighscores() 
	{
		highscores.Sort((y, x) => x.score.CompareTo(y.score));
	}

	private void UpdateHighscores() 
	{
		string highscoresString;
		highscoresString = "LOCAL\nHIGHSCORES\n\n";
		int position = 1;
		foreach (Highscore highscore in highscores) 
		{
			highscoresString += "<b>" + position + "\t";
			highscoresString += highscore.name + "</b>:\t";
			highscoresString += highscore.score + "\n";
			position++;
		}
		highscoresText.text = highscoresString;
	}
}
