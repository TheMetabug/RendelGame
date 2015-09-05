using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {
    int hitPoints;
    float coolDown;
    public float defaultCooldown;
    int damage;
    public float speed;
    float step;
    float dodgeTimer;
    float defaultDodgeTimer;
    bool hitting;
    public GameObject enemy;
    public GameObject leftHand;
    public GameObject rightHand;
    Vector3 leftHandStartingPosition;
    Vector3 rightHandStartingPosition;
    public bool enemyHit = true;
    public bool ishit = false;
    public bool dodging = false;

	// Use this for initialization
	void Start () 
    {
        hitPoints = 3;
        damage = 1;
        coolDown = 2.0f;
        dodgeTimer = defaultDodgeTimer;
        leftHandStartingPosition = leftHand.transform.position;
        rightHandStartingPosition = rightHand.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (coolDown > 0)
        {
            coolDown -= Time.deltaTime;
        }
        if (coolDown <= 0)
        { 
            if(Input.tou)
        }
        if (enemyHit == true)
        {
            hitEnemy();
        }
        if(dodging)
        {
            dodgeTimer--;
            if(dodgeTimer <= 0)
            {
                dodging = false;
                dodgeTimer = defaultDodgeTimer;
            }
        }
	}
    void hitEnemy()
    {
        
        if (enemy)
        {
            step = speed * Time.deltaTime;
            if (enemy.transform.position.x - transform.position.x > 0)
            {

                if (ishit == false)
                {
                    leftHand.transform.position = Vector3.MoveTowards(leftHand.transform.position, enemy.transform.position,step);
                    if (Vector3.Distance(leftHand.transform.position, enemy.transform.position) < 0.2f)
                    {
                        ishit = true;
                    }
                }
                if (ishit)
                {
                    leftHand.transform.position = Vector3.MoveTowards(leftHand.transform.position, leftHandStartingPosition, step);
                    if (Vector3.Distance(leftHand.transform.position, leftHandStartingPosition) < 0.2f)
                    {
                        ishit = false;
                        enemyHit = false;
                    }
                }
            }
            else
            {
                if (ishit == false)
                {
                    rightHand.transform.position = Vector3.MoveTowards(rightHand.transform.position, enemy.transform.position, step);
                    if (Vector3.Distance(rightHand.transform.position, enemy.transform.position) < 0.2f)
                    {
                        ishit = true;
                    }
                }
                if (ishit)
                {
                    rightHand.transform.position = Vector3.MoveTowards(rightHand.transform.position, rightHandStartingPosition, step);
                    if (Vector3.Distance(rightHand.transform.position, rightHandStartingPosition) < 0.2f)
                    {
                        ishit = false;
                        enemyHit = false;
                    }
                }
            }
            coolDown = defaultCooldown;
        }
    }
    void dodge()
    {
        dodging = true;
    }
    void getHit()
    {
        coolDown = defaultCooldown;
        hitPoints--;
        if (hitPoints <= 0)
        {
            shinenzo();
        }
    }
    void shinenzo()
    {
        //show highscore

    }
}
