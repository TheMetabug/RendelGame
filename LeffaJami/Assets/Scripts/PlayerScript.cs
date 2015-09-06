using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour 
{

	public bool isDodging;
	public int health;
	public bool isPunchingEnemy;
	public bool isPunchingAir;
	public bool isPunching;
	public bool isDead;
	public bool isDying;

	private Vector3 startPosition;
	private Vector3 dodgePosition;
	private bool hitEnemy;

	void Start () 
	{
		Reset ();
	}

	public void Reset()
	{
		startPosition = transform.localPosition;
		isDodging = false;
		health = 3;
		isPunchingEnemy = false;
		isPunchingAir = false;
		isPunching = false;
		hitEnemy = false;
		isDead = false;
		isDying = false;

		GameVariables.PlayerHealth = 3;
	}

	void Update () 
	{
		GameVariables.PlayerHealth = health;
	}

	public void GetDamage()
	{
		health--;
		if (health <= 0) {
			Death();
		}
		StartCoroutine ("HurtAnimation");
	}

	public void Death()
	{
		if (!isDying && !isDead) {
			isDying = true;
			StartCoroutine("DeathAnimation");

		}
	}

	public void Dodge(bool isRightSide)
	{
		if (!isDodging && !isPunching) 
		{
			StopCoroutine ("DodgeToSide");
			StartCoroutine ("DodgeToSide", isRightSide);
		}
	}

	public void Punch(bool isRightSide, bool _hitEnemy)
	{
		if (!isDodging && !isPunching) 
		{
			isPunching = true;
			hitEnemy = _hitEnemy;

			StopCoroutine ("PunchAnimation");

			StartCoroutine ("PunchAnimation", isRightSide);
		}
	}

	IEnumerator HurtAnimation()
	{	
		var startColor = GetComponent<SpriteRenderer> ().color;
		float colorVar = 0.15f;
		GetComponent<SpriteRenderer> ().color = new Color(1f,colorVar,colorVar, startColor.a);
		
		while (GetComponent<SpriteRenderer> ().color.g < 1f)
		{
			colorVar += Time.deltaTime;
			GetComponent<SpriteRenderer> ().color = new Color(1f,colorVar,colorVar, startColor.a);
			yield return null;
		}
		
		yield return null;
	}
	IEnumerator DeathAnimation()
	{	
		while (isDying) {
			if (transform.localPosition.y > startPosition.y - 4f) {
				transform.localPosition = Vector3.Lerp (transform.localPosition, new Vector3 (0, -8f, 0), Time.deltaTime * 2f);
			} else {
				isDying = false;
			}
		}

		isDead = true;
		
		yield return null;
	}

	IEnumerator PunchAnimation(bool isRightSide)
	{	
		Vector3 punchPos;
		Vector3 buildUpPos;
		HandScript hand;

		if (isRightSide) {
			punchPos = new Vector3(startPosition.x + 0.2f, startPosition.y + 0.2f, startPosition.z);
			buildUpPos = new Vector3(startPosition.x - 0.2f, startPosition.y - 0.2f, startPosition.z);
			hand = transform.GetChild(0).GetComponent<HandScript>();
			print("right punch");
		} else {
			punchPos = new Vector3(startPosition.x - 0.2f, startPosition.y + 0.2f, startPosition.z);
			buildUpPos = new Vector3(startPosition.x + 0.2f, startPosition.y - 0.2f, startPosition.z);
			hand = transform.GetChild(1).GetComponent<HandScript>();
			print("left punch");
		}

		if (hitEnemy) {
			isPunchingEnemy = true;
			isPunchingAir = false;
		} else {
			isPunchingAir = true;
			isPunchingEnemy = false;
		}

		bool isGoingToPunchPosition = true;
		bool isNotOnStartPos = true;

		hand.changeHandState (HandScript.handStates.build);
		
		while (isPunchingEnemy) {
			if (isGoingToPunchPosition) {
				transform.localPosition = Vector3.Lerp (transform.localPosition, buildUpPos, Time.deltaTime * 5f);
			} else {
				transform.localPosition = Vector3.Lerp (transform.localPosition, punchPos, Time.deltaTime * 5f);
			}
			
			if (Vector3.Distance (transform.localPosition, buildUpPos) < 0.1f && isGoingToPunchPosition) {
				isGoingToPunchPosition = false;
				isPunchingEnemy = false;
				hand.changeHandState (HandScript.handStates.punch);
			}		
			if (Vector3.Distance (transform.localPosition, punchPos) < 0.1f && !isGoingToPunchPosition) {
				//isPunchingEnemy = false;
			}
			yield return null;
		}

		while (isPunchingAir) {
			if (isGoingToPunchPosition) {
				transform.localPosition = Vector3.Lerp (transform.localPosition, buildUpPos, Time.deltaTime * 5f);
			} else {
				transform.localPosition = Vector3.Lerp (transform.localPosition, punchPos, Time.deltaTime * 5f);
			}
			
			if (Vector3.Distance (transform.localPosition, buildUpPos) < 0.1f && isGoingToPunchPosition) {
				isGoingToPunchPosition = false;
				isPunchingAir = false;
				hand.changeHandState (HandScript.handStates.punch);
			}		
			if (Vector3.Distance (transform.localPosition, punchPos) < 0.1f && !isGoingToPunchPosition) {
				//isPunchingAir = false;
			}
			yield return null;
		}


		// Return to start position
		while (isNotOnStartPos) {
			transform.localPosition = Vector3.Lerp (transform.localPosition, startPosition, Time.deltaTime * 5f);
			if (Vector3.Distance (transform.localPosition, startPosition) < 0.1f) {
				transform.localPosition = startPosition;
				isNotOnStartPos = false;
			}

			yield return null;
		}
		
		hand.changeHandState (HandScript.handStates.ready);
		isPunching = false;
		hitEnemy = false;

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
