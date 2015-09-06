using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public float spawntime;
    float time;
    public GameObject enemy;
	public int maxEnemies;

	void Start () 
	{
        time = spawntime;
	}

    void Update() 
	{
        if (time <= 0 && transform.childCount < maxEnemies) 
		{
            GameObject newEnemy = Instantiate(enemy, gameObject.transform.localPosition, Quaternion.identity) as GameObject;
            //random in range(0-2)
            //newEnemy.setEnum(rand);
			newEnemy.transform.parent = transform;
			newEnemy.GetComponent<EnemyBehavior>().offsetOfKillZone = new Vector3(Random.Range(-1.0f,1.0f),0,0);
            time = spawntime;
        }
        else 
		{
            time -= Time.deltaTime;
        }
    }
}
