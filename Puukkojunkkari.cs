using UnityEngine;
using System.Collections;

public class Puukkojunkkari : MonoBehaviour {

    public int health;
    public float speed;
    public float hitspeed;
    public float delay;
    public bool hit;
    public bool inPosition;
    public bool flipflop;
    public bool staggering;

    public GameObject killingzone;
    public GameObject target;

    void Start()
    {
        health = 1;
        hit = false;
        speed = 8.0f;
        hitspeed = 3.0f;
        delay = 0.5f;
        staggering = false;

        killingzone = GameObject.FindGameObjectWithTag("KZ");
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (inPosition)
        {
            //foreach (Touch touch in Input.touches) {
            if (Input.GetMouseButtonDown(0))
            {
                if (Vector2.Distance(/*touch.position*/Input.mousePosition, Camera.main.WorldToScreenPoint(transform.position)) < 250 && staggering)
                {
                    Damage(1);
                }
            }
            delay -= Time.deltaTime;
            if (delay <= 0.1f)
            {
                staggering = false;
                if (delay <= 0)
                {
                    HitDammit();
                    delay = 0.5f;
                }
            }
        }
        else
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, killingzone.transform.position, step);
        }
        CheckCondition();
    }

    void Damage(int dmg)
    {
        health -= dmg;
    }

    void CheckCondition()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void HitDammit()
    {
        staggering = true;
        Debug.Log("you are kill!");
        //maek animotien
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            hit = true;
        }
        else
        {
            hit = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "KZ")
        {
            inPosition = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "KZ")
        {
            inPosition = false;
        }
    }
}
