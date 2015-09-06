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
    public bool staggering;

    public Vector3 offsetOfKillZone = new Vector3();

    public GameObject killingzone;
    public GameObject target;

    public Sprite[] sprites = new Sprite[0];
    public float timeBetweenSprites_walk = 0.5f;
    private float walkTimer = 0;
    private int walkIndex = 0;

    public enum type
    {
        basic = 0,
        puukko = 1,
        shield = 2,
    }

    public type enemytype;

	void Start () {
        health = 1;
        hit = false;
        speed = 8.0f;
        hitspeed = 3.0f;
        delay = 0.5f;
        staggering = false;

        killingzone = GameObject.FindGameObjectWithTag("KZ");
        target = GameObject.FindGameObjectWithTag("Player");
    }

	void Update () {
        if (inPosition) {
            //foreach (Touch touch in Input.touches) {
            if(Input.GetMouseButtonDown(0)) {    
                if (Vector2.Distance(/*touch.position*/Input.mousePosition , Camera.main.WorldToScreenPoint(transform.position)) < 250) {
                    switch (enemytype)
                    {
                        case type.basic:
                            Damage(1);
                            break;
                        case type.puukko:
                            if(staggering) Damage(1);
                            break;
                        case type.shield:
                            //something is happening
                            break;
                        default:
                            break;
                    }
                }
            }
            delay -= Time.deltaTime;
            
            switch (enemytype)
            {
                case type.basic:
                    if (delay <= 0)
                    {
                        HitDammit();
                        delay = 0.5f;
                    }
                    break;
                case type.puukko:
                    if (delay <= 0.15f)
                    {
                        staggering = false;
                        if (delay <= 0)
                        {
                            HitDammit();
                            delay = 0.5f;
                        }
                    }
                    break;
                case type.shield:
                    //shield is evolving
                    break;
                default:
                    break;
            }
        }
        else {
            WalkAnimation();
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, killingzone.transform.position + offsetOfKillZone, step);
        }
        CheckCondition();
	}

    public void Damage(int dmg)
    {
        if (!target.GetComponent<PlayerScript>().isDodging)
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
        switch (enemytype)
        {
            case type.basic:
                if(!target.GetComponent<PlayerScript>().isDodging)
                {
                    target.GetComponent<PlayerScript>().GetDamage();
                    Debug.Log("Now, that's just embarrassing.");
                }
                StartCoroutine ("PunchAnim");
                break;
            case type.puukko:
                if (!target.GetComponent<PlayerScript>().isDodging)
                {
                    staggering = false;
                    target.GetComponent<PlayerScript>().GetDamage();
                    Debug.Log("Now, that's just embarrassing.");
                }
                else { staggering = true; }
                StartCoroutine ("PunchAnim");
                break;
            case type.shield:
                //Shield evolved into metapod
                break;
            default:
                break;
        }
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

        if (timeBetweenSprites_walk <= walkTimer)
        {
            walkIndex++;

            if (sprites.Length <= walkIndex)
            {
                walkIndex = 0;
            }

            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[walkIndex];
            walkTimer = 0;
        }
    }

    IEnumerator PunchAnim()
    {
        var startColor = GetComponent<SpriteRenderer>().color;
        float colorVar = 0.75f;
        GetComponent<SpriteRenderer>().color = new Color(1f, colorVar, colorVar);

        while (GetComponent<SpriteRenderer>().color.g < 1f)
        {
            colorVar += Time.deltaTime * 1.25f;
            GetComponent<SpriteRenderer>().color = new Color(1f, colorVar, colorVar);
            yield return null;
        }

        yield return null;
    }
}