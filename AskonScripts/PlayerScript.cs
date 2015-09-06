using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour 
{

	public bool isDodging;
	public int health;

	private Vector3 startPosition;
	private Vector3 dodgePosition;

	void Start () 
	{
		Reset ();
	}

	void Reset()
	{
		startPosition = transform.localPosition;
		isDodging = false;
		health = 3;
		GameVariables.PlayerHealth = 3;
	}

	void Update () 
	{
		
	}

	public void GetDamage()
	{
		StartCoroutine ("HurtAnimation");
	}

	public void Dodge(bool isRightSide)
	{
		if (!isDodging) 
		{
			StopCoroutine ("DodgeToSide");
			StartCoroutine ("DodgeToSide", isRightSide);
		}
	}

	IEnumerator HurtAnimation()
	{	
		var startColor = GetComponent<SpriteRenderer> ().color;
		float colorVar = 0.15f;
		
		while (GetComponent<SpriteRenderer> ().color.g < 1f)
		{
			colorVar += Time.deltaTime;
			GetComponent<SpriteRenderer> ().color = new Color(1f,colorVar,colorVar, startColor.a);
			yield return null;
		}
		
		yield return null;
	}

	IEnumerator DodgeToSide(bool isRightSide)
	{
		if (isRightSide) {
			dodgePosition = new Vector3(startPosition.x + 1.0f, startPosition.y - 1.0f, startPosition.z);
			print("right");
		} else {
			dodgePosition = new Vector3(startPosition.x - 1.0f, startPosition.y - 1.0f, startPosition.z);
			print("left");
		}

		isDodging = true;
		bool isGoingToDodgePosition = true;

		while (isDodging)
		{
			if(isGoingToDodgePosition)
			{
				transform.localPosition = Vector3.Lerp(transform.localPosition, dodgePosition, Time.deltaTime * 5f);
			}
			else
			{
				transform.localPosition = Vector3.Lerp(transform.localPosition, startPosition, Time.deltaTime * 5f);
			}

			if(Vector3.Distance(transform.localPosition, dodgePosition) < 0.1f && isGoingToDodgePosition)
			{
				isGoingToDodgePosition = false;
			}		
			if(Vector3.Distance(transform.localPosition, startPosition) < 0.1f && !isGoingToDodgePosition)
			{
				transform.localPosition = startPosition;
				isDodging = false;
			}
			yield return null;
		}

		isGoingToDodgePosition = false;
		yield return null;
	}
}
