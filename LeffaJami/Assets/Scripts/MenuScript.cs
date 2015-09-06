using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

	public GameManagerScript manager;

	void Start ()
	{
	
	}

	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) 
		{   
			if (Input.mousePosition.x - Camera.main.WorldToScreenPoint (transform.position).x < 2500) 
			{
				manager.SwitchState (1);
			}
		}
	}
}
