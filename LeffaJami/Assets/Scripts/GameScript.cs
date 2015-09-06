using UnityEngine;
using System.Collections;

public class GameScript : MonoBehaviour
{
	public GameManagerScript manager;
	public GameObject uiInterface;
	public PlayerScript playerObj;
	public Spawner enemySpawner;

	public EnemyBehavior[] enemies = new EnemyBehavior[10];
	
	void Start ()
	{
		
	}
	
	void Update ()
	{
		GameInput ();
	}

	private void GetEnemies()
	{
		for (int i = 0; i < enemySpawner.transform.childCount; i++) 
		{
			enemies[i] = enemySpawner.gameObject.transform.GetChild(i);
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
		if (Mathf.Abs(Input.mousePosition.x - Camera.main.WorldToScreenPoint (uiInterface.transform.GetChild (0).transform.position).x) < 35 &&
		    Mathf.Abs(Input.mousePosition.y - Camera.main.WorldToScreenPoint (uiInterface.transform.GetChild (0).transform.position).y) < 35) 
		{
			playerObj.Dodge (false);
		}
		if (Mathf.Abs(Input.mousePosition.x - Camera.main.WorldToScreenPoint (uiInterface.transform.GetChild (1).transform.position).x) < 35 &&
		    Mathf.Abs(Input.mousePosition.y - Camera.main.WorldToScreenPoint (uiInterface.transform.GetChild (1).transform.position).y) < 35) 
		{
			playerObj.Dodge (true);
		} 
	}

	private void CheckIfHitEnemy()
	{
		/*
		for (int i = 0; i < enemies.Length; i++) 
		{	
			if (Mathf.Abs(Input.mousePosition.x - Camera.main.WorldToScreenPoint(transform.position).x) < 140) 
			{
				Damage(1);
			}
		}*/

	}

	public void GameOver()
	{
		manager.SwitchState (2);
	}
}
