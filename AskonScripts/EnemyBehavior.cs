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

	public Vector3 offsetOfKillZone = new Vector3();

    public GameObject killingzone;
    public GameObject target;

	public Sprite[] sprites = new Sprite[0];
	public float timeBetweenSprites_walk = 0.5f; 
	private float walkTimer = 0;
	private int walkIndex = 0;

	void Start () {
        health = 1;
        hit = false;
        speed = 8.0f;
        hitspeed = 3.0f;
        delay = 0.5f;

        killingzone = GameObject.FindGameObjectWithTag("KZ");
        target = GameObject.FindGameObjectWithTag("Player");
    }

	void Update () 
	{
        if (inPosition) 
		{
            //foreach (Touch touch in Input.touches) {
           /* if(Input.GetMouseButtonDown(0)) 
			{    
				if (Mathf.Abs(Input.mousePosition.x - Camera.main.WorldToScreenPoint(transform.position).x) < 140) 
				{
                    Damage(1);
                }
            }*/

            delay -= Time.deltaTime;

            if (delay <= 0)
            {
                HitDammit();
                delay = 1.5f;
            }

        }
        else 
		{
			WalkAnimation();
            float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, killingzone.transform.position + offsetOfKillZone, step);
        }
        CheckCondition();
	}

    public void Damage(int dmg)
	{
		if (!target.GetComponent<PlayerScript> ().isDodging)
		{
			health -= dmg;
		}
    }

    void CheckCondition() {
        if (health <= 0) {
            Destroy(gameObject);
        }
    }

    void HitDammit() {
		if(!target.GetComponent<PlayerScript>().isDodging)
		{
			target.GetComponent<PlayerScript>().GetDamage();
		}
		StartCoroutine ("PunchAnim");
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Player") {
            hit = true;
        }
        else {
            hit = false;
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "KZ") {
            inPosition = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "KZ") {
            inPosition = false;
        }
    }
	void WalkAnimation()
	{
		walkTimer += Time.deltaTime;
		
		if (timeBetweenSprites_walk <= walkTimer) {
			walkIndex++;
			
			if(sprites.Length <= walkIndex)
			{
				walkIndex = 0;
			}
			
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[walkIndex];
			walkTimer = 0;
		}
	}

	IEnumerator PunchAnim()
	{
		var startColor = GetComponent<SpriteRenderer> ().color;
		float colorVar = 0.75f;
		GetComponent<SpriteRenderer> ().color = new Color(1f, colorVar, colorVar);

		while (GetComponent<SpriteRenderer> ().color.g < 1f)
		{
			colorVar += Time.deltaTime * 1.25f;
			GetComponent<SpriteRenderer> ().color = new Color(1f, colorVar, colorVar);
			yield return null;
		}

		yield return null;
	}
}