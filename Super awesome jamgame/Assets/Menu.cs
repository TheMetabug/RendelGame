using UnityEngine;
using System.Collections;
public class Menu : MonoBehaviour {

    public string textField;
    public Transform startGame;
    public Transform scoreBoard;
    public bool startScreen;
    public bool mainMenu;
    public string playerName;
    public PlayerPrefs playerPrefs;
    public GameObject game;
    public GameObject menu;
	// Use this for initialization
	void Start () 
    {
        PlayerPrefs.SetString("Player", "null");
        PlayerPrefs.Save();
        startScreen = true;
        startGame = transform;
	    //open login screen
	}
    void OnGUI()
    {
        if (startScreen)
        {
            if(Event.current.keyCode == KeyCode.Return)
            {
                PlayerPrefs.SetString("Player", textField);
                playerName = PlayerPrefs.GetString("Player");
                PlayerPrefs.Save();
            }
            textField = GUI.TextField(new Rect(10, 10, 200, 20), textField, 25);
            if (playerName != "null" && playerName != "" && !playerName.Equals(null) && playerName != null)
            {
                startScreen = false;
                mainMenu = true;
            }
        }
        if (mainMenu)
        {
            if (GUI.Button(new Rect(100, 100, 200, 50), "Start Game"))
            {
                print("Start Game Pressed");
                game.SetActive(true);
                menu.SetActive(false);
                mainMenu = false;
                //switchState(1);
            }
            if (GUI.Button(new Rect(100, 300, 200, 50), "High Score"))
            {
                print("High Score Pressed");
                //switchState(2);
            }
            if (GUI.Button(new Rect(100, 500, 200, 50), "Credits"))
            {
                print("Credits Pressed");
                //switchState(3);
            }
        }
    }
	
	// Update is called once per frame
	void Update () 
    {

	}

    //start game, view highscore,press back to runaway
}
