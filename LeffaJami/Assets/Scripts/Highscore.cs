using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Highscore : MonoBehaviour
{
    public List<int> highscores;
    public List<string> names;

	void Start ()
    {
        Load();
	}
	
	void Update ()
    {
	    if (Input.GetKey(KeyCode.Escape))
        {
            //activate main menu and disable highscore
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
            GUI.Label(new Rect(10, 200+25 * i, 200, 20), (i+1) + ". " + names[i]);
            GUI.Label(new Rect(250, 200+25 * i, 100, 20), highscores[i].ToString());
        }
    }


    void ResetScore()
    {
        highscores.Clear();
        names.Clear();
    }
}