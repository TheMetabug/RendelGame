using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameScript : MonoBehaviour
{
	public GameManagerScript manager;
	public GameObject uiInterface;
	public PlayerScript playerObj;
	public Spawner enemySpawner;

	public EnemyBehavior[] enemies = new EnemyBehavior[10];
    public Text scoreText;
    public float score = 0;
	void Start ()
	{
        scoreText.text = "Score: " + score;
	}
	
	void Update ()
	{
		GameInput ();
		GetEnemies ();
        score += 1 * Time.deltaTime;
        scoreText.text = "Score: " + score;
	}

	private void GetEnemies()
	{
		for (int i = 0; i < enemySpawner.transform.childCount; i++) 
		{
			enemies[i] = enemySpawner.transform.GetChild(i).GetComponent<EnemyBehavior>();
		}
	}

	private void GameInput()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			manager.SwitchState (0);
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			GameOver();
		}
		if (Input.GetMouseButtonDown (0) && !playerObj.isDodging) 
		{   
			CheckIfHitDodgeButton();
			CheckIfHitEnemy();
		}
	}

	private void CheckIfHitDodgeButton()
	{
		/*
		if (Mathf.Abs(Input.mousePosition.x - Camera.main.WorldToScreenPoint (uiInterface.transform.GetChild (0).transform.position).x) < 35 &&
		    Mathf.Abs(Input.mousePosition.y - Camera.main.WorldToScreenPoint (uiInterface.transform.GetChild (0).transform.position).y) < 35) 
		{
			playerObj.Dodge (false);
		}
		if (Mathf.Abs(Input.mousePosition.x - Camera.main.WorldToScreenPoint (uiInterface.transform.GetChild (1).transform.position).x) < 35 &&
		    Mathf.Abs(Input.mousePosition.y - Camera.main.WorldToScreenPoint (uiInterface.transform.GetChild (1).transform.position).y) < 35) 
		{
			playerObj.Dodge (true);
		} */

		Bounds mouseBounds = new Bounds (Input.mousePosition, new Vector3 (65.2f, 65.2f, 65.2f));
		Bounds debugBounds_0 = new Bounds (Camera.main.WorldToScreenPoint (uiInterface.transform.GetChild (0).transform.localPosition), new Vector3 (65.2f, 65.2f, 65.2f));
		Bounds debugBounds_1 = new Bounds (Camera.main.WorldToScreenPoint (uiInterface.transform.GetChild (1).transform.localPosition), new Vector3 (65.2f, 65.2f, 65.2f));

		if (debugBounds_0.Intersects(mouseBounds))
		{
			playerObj.Dodge (false);
		}
		else if (debugBounds_1.Intersects(mouseBounds))
		{
			playerObj.Dodge (true);
		}
	}

	private void CheckIfHitEnemy()
	{
		for (int i = 0; i < enemies.Length; i++) 
		{	
			if(enemies[i] != null)
			{
				if (Mathf.Abs(Input.mousePosition.x - Camera.main.WorldToScreenPoint(enemies[i].transform.position).x) < 280 &&
				    enemies[i].inPosition) 
				{
					if(enemies[i].transform.position.x < 0)
					{
						playerObj.Punch(false, true);
					}
					else
					{
						playerObj.Punch(true, true);
					}
					StartCoroutine("makePlayerHitEnemyWhenDone", enemies[i]);
				}
				else
				{
					if(enemies[i].transform.position.x < 0)
					{
						playerObj.Punch(false, false);
					}
					else
					{
						playerObj.Punch(true, false);
					}
					StartCoroutine("checkIfPlayerHitsEnemyWhileMissing", enemies[i]);
				}
			}
		}
	}

	IEnumerator makePlayerHitEnemyWhenDone(EnemyBehavior enemy)
	{
		while (playerObj.isPunchingEnemy) {
			yield return null;
		}
		//enemy.Damage(1);
		HitEnemy (enemy);
		yield return null;
	}
	IEnumerator checkIfPlayerHitsEnemyWhileMissing(EnemyBehavior enemy)
	{
		while (playerObj.isPunchingAir) {
			yield return null;
		}
		if (enemy.inPosition) {
			//enemy.Damage (1);
			HitEnemy(enemy);
		}
		yield return null;
	}

	private void HitEnemy(EnemyBehavior enemy)
	{
		switch (enemy.enemytype)
		{
		case EnemyBehavior.type.basic:
			enemy.Damage(1);
			break;
		case EnemyBehavior.type.puukko:
			if(enemy.staggering) enemy.Damage(1);
			break;
		case EnemyBehavior.type.shield:
			if (enemy.staggering) enemy.Damage(1);
			else { enemy.staggering = true; }
			break;
		default:
			break;
		}
	}
	
	
	
	public void GameOver()
	{
		manager.SwitchState (2);
	}
}
