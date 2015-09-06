using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class MenuScript : MonoBehaviour {

	public GameManagerScript manager;
    public bool startScreen;
    public Text playerName;
    public string name;
    void setName()
    {
        
        PlayerPrefs.SetString("Player", playerName.text);
        PlayerPrefs.Save();
        print(playerName.text);
    }
}
