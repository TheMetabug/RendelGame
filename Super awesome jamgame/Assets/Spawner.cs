using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public float spawntime;
    float time;
    public GameObject enemy;

	void Start () {
        time = spawntime;
	}

    void Update() {
        if (time <= 0) {
            Instantiate(enemy);
            time = spawntime;
        }
        else {
            time -= Time.deltaTime;
        }
    }
}
