using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

    public int health;
    public float speed;
    public float hitspeed;
    public float delay;
    public float stagdelay;
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
        switch (enemytype)
        {
            case type.basic:
                health = 1;
                hit = false;
                speed = 8.0f;
                hitspeed = 3.0f; // Ei käytetä tällä hetkellä missään
                delay = 0.5f;
                staggering = false;
                break;
            case type.puukko:
                health = 1;
                hit = false;
                speed = 7.0f;
                hitspeed = 3.0f; // Vois olla hyvä ottaa käyttöön
                delay = 0.75f;
                stagdelay = 0.25f;
                staggering = false;
                break;
            case type.shield:
                health = 1;
                hit = false;
                speed = 4.0f;
                hitspeed = 3.0f; // Sais eri vihollisille erimittaiset lyöntivälit
                delay = 1.0f;
                stagdelay = 0.5f;
                staggering = false;
                break;
            default:
                break;
        }


        killingzone = GameObject.FindGameObjectWithTag("KZ");
        target = GameObject.FindGameObjectWithTag("Player");
    }

	void Update () {
        if (inPosition) {
            //foreach (Touch touch in Input.touches) {
            /*
			if(Input.GetMouseButtonDown(0)) {    
                if (Vector2.Distance(Input.mousePosition , Camera.main.WorldToScreenPoint(transform.position)) < 250) {
                    switch (enemytype)
                    {
                        case type.basic:
                            Damage(1);
                            break;
                        case type.puukko:
                            if(staggering) Damage(1);
                            break;
                        case type.shield:
                            if (staggering) Damage(1);
                            else { staggering = true; }
                            break;
                        default:
                            break;
                    }
                }
            }
			*/

            switch (enemytype)
            {
                case type.basic:
                    if (staggering)
                    {
                        stagdelay -= Time.deltaTime;
                        if (stagdelay <= 0)
                        {
                            staggering = false;
                            stagdelay = 0;
                        }
                    }
                    else { delay -= Time.deltaTime; }

                    if (delay <= 0)
                    {
                        HitDammit();
                        delay = 0.5f;
                    }
                    break;
                case type.puukko:
                    if (staggering)
                    {
                        stagdelay -= Time.deltaTime;
                        if (stagdelay <= 0)
                        {
                            staggering = false;
                            stagdelay = 0.25f;
                        }
                    }
                    else { delay -= Time.deltaTime; }

                    if (delay <= 0)
                    {
                        HitDammit();
                        delay = 0.5f;
                    }
                    break;
                case type.shield:
                    if (staggering)
                    {
                        stagdelay -= Time.deltaTime;
                        if (stagdelay <= 0)
                        {
                            staggering = false;
                            stagdelay = 0.5f;
                        }
                    }
                    else { delay -= Time.deltaTime; }

                    if (delay <= 0)
                    {
                        HitDammit();
                        delay = 1.0f;
                    }
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
        switch (enemytype)
        {
            case type.basic:
                if (!target.GetComponent<PlayerScript>().isDodging)
                {
                    health -= dmg;
                }
                break;
            case type.puukko:
                if (!target.GetComponent<PlayerScript>().isDodging)
                {
                    health -= dmg;
                }
                break;
            case type.shield:
                if (!target.GetComponent<PlayerScript>().isDodging)
                {
                    health -= dmg;
                }
                break;
            default:
                break;
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
                    Debug.Log("Now, that's just embarrassing. NORMIE");
                }
                StartCoroutine ("PunchAnim");
                break;
            case type.puukko:
                if (!target.GetComponent<PlayerScript>().isDodging)
                {
                    staggering = false;
                    target.GetComponent<PlayerScript>().GetDamage();
                    Debug.Log("Now, that's just embarrassing. PUUKKO");
                }
                else { staggering = true; }
                StartCoroutine ("PunchAnim");
                break;
            case type.shield:
                if (!target.GetComponent<PlayerScript>().isDodging)
                {
                    staggering = true;
                    target.GetComponent<PlayerScript>().GetDamage();
                    Debug.Log("Now, that's just embarrassing. SHIELD");
                }
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

    public void setEnum(int integer)
    {
        switch (integer)
        {
            case type.basic:
                enemytype = type.basic;
                break;
            case type.puukko:
                enemytype = type.puukko;
                break;
            case tpe.shield:
                enemytype = type.shield;
                break;
            default:
                break;
        }
    }
}