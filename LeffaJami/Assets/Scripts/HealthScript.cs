using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {

	public Sprite[] hpsprites = new Sprite[2];

	void Start () 
	{
	
	}

	void Update () 
	{
		if (GameVariables.PlayerHealth == 3) 
		{
			transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = hpsprites[0];
			transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = hpsprites[0];
			transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = hpsprites[0];
		}
		else if(GameVariables.PlayerHealth == 2)
		{
			transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = hpsprites[0];
			transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = hpsprites[0];
			transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = hpsprites[1];
		}
		else if(GameVariables.PlayerHealth == 1)
		{
			transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = hpsprites[0];
			transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = hpsprites[1];
			transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = hpsprites[1];
		}
		else if(GameVariables.PlayerHealth == 0)
		{
			transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = hpsprites[1];
			transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = hpsprites[1];
			transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = hpsprites[1];
		}
	}
}
