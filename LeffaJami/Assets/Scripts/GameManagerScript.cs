using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour 
{
	public GameObject[] gameStates = new GameObject[3];
	
	void Start () 
	{
		SwitchState (0);
	}
	
	void Update () 
	{

	}

	public void SwitchState(int index)
	{
		for (int i = 0; i < gameStates.Length; i++) 
		{
			if(index == i)
			{
				gameStates[i].gameObject.SetActive(true);
			}
			else
			{
				gameStates[i].gameObject.SetActive(false);
			}
		}
	}
}
