using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

    public int health;
    public float speed;
    public float hitspeed;
    public float delay;
    public bool hit;
    public bool inPosition;
    public bool flipflop;

    public GameObject killingzone;
    public GameObject target;
    public GameObject RightFist;
    public GameObject LeftFist;

	void Start () {
        health = 3;
        hit = false;
        speed = 8.0f;
        hitspeed = 3.0f;
        delay = 2.0f;

        killingzone = GameObject.FindGameObjectWithTag("KZ");
        target = GameObject.FindGameObjectWithTag("Player");
        RightFist = GameObject.FindGameObjectWithTag("RFist");
        LeftFist = GameObject.FindGameObjectWithTag("LFist");
    }

	void Update () {
        if (inPosition) {
            if (delay <= 0) {
                HitDammit();
                delay = 2.0f;
            }
            else {
                delay -= Time.deltaTime;
            }
        }
        else {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, killingzone.transform.position, step);
        }
        CheckCondition();
	}

    void Damage(int dmg) {
        health -= dmg;
    }

    void CheckCondition() {
        if (health <= 0) {
            Destroy(gameObject);
        }
    }

    void HitDammit() {
        Debug.Log("hurr");
        //maek animotien
        Debug.Log("durr");
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Player") {
            hit = true;
        }
        else {
            hit = false;
        }
    }

    void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "KZ") {
            inPosition = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "KZ") {
            inPosition = false;
        }
    }
}