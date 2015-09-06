using UnityEngine;
using System.Collections;

public class HandScript : MonoBehaviour {

	public Sprite[] handSprites = new Sprite[3];

	public enum handStates
	{
		ready,
		punch,
		build
	};

	void Start () 
	{
	
	}


	void Update () 
	{
	}

	public void changeHandState(handStates state)
	{
		switch (state) {
		case handStates.ready:
			GetComponent<SpriteRenderer>().sprite = handSprites[0];
			break;
		case handStates.punch:
			GetComponent<SpriteRenderer>().sprite = handSprites[1];
			break;
		case handStates.build:
			GetComponent<SpriteRenderer>().sprite = handSprites[2];
			break;
		}
	}
}
