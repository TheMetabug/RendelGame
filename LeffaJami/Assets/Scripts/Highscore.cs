using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Highscore : MonoBehaviour
{
    public List<int> highscores;
    public List<string> names;
    public GameObject mainMenu;

	private GUIStyle styleForList;

	void Start ()
    {
        Load();
		styleForList = new GUIStyle ();
		styleForList.fontSize = 50;
	}
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            mainMenu.SetActive(true);
            gameObject.SetActive(false);
        }
	}

    void Save()
    {
        for (int i = 0; i < 15; i++)
		{
            PlayerPrefs.SetInt(400 + i.ToString(), highscores[i]);
            PlayerPrefs.SetString(100 + i.ToString(), names[i]);
        }
        PlayerPrefs.Save();
    }

    void Load()
    {
        if (highscores != null)
        {
            for (int i = 0; i < 15; i++)
            {
                highscores.Add(PlayerPrefs.GetInt(400 + i.ToString(), 0));
                names.Add(PlayerPrefs.GetString(100 + i.ToString(), "default"));
            }
        }
    }

    public void CheckScore(int score, string playername)
    {
        int i;
        for (i = 0; i < highscores.Count; i++)
        {
            if (highscores[i] > score)
            { }
            else
            {
                break;
            }
        }
        highscores.Insert(i, score);
        names.Insert(i, playername);
        highscores.RemoveAt(15);
        names.RemoveAt(15);
        Save();
    }

    void OnGUI()
    {
        for (int i = 0; i < 15; i++)
        {
			if(names[i] != "default")
			{
				GUI.Label(new Rect(10, 100 + 55 * i, 200, 20), (i+1) + ". " + names[i], styleForList);
				GUI.Label(new Rect(250, 100 + 55 * i, 100, 20), highscores[i].ToString(), styleForList);
			}
        }
    }

	void OnEnable()
	{
		if (GameVariables.score > 0) {

			CheckScore((int)GameVariables.score, PlayerPrefs.GetString("Player"));

			GameVariables.score = 0;
		}
	}

    void ResetScore()
    {
        highscores.Clear();
        names.Clear();
    }
}