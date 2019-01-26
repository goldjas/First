﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy1Script : MonoBehaviour {
    //Is there a better way to differentiate different enemies then just a goofy string here?  HMMMMM
    public string EnemyName;
    public float speed;
    //transform is the position
    public Transform player;
    public Text HealthText;
    private float nextFire;
    public GameObject shot;
    public GameObject healthCrystal;
    public Transform shotSpawn;
    public GameObject WallToDestroy;
    public GameObject playerObject;
    public GameObject MainCanvas;
    public GameObject EnemyDamageText;
    public Text DamageTakenText;


    public float healthTimer;
    public float nextHealth;

    private Rigidbody2D rb2d;
    //private bool healthout;

    Animator anim;


    float health;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
        //playerObject = GameObject.FindGameObjectWithTag("Player");
        health = 10;
        rb2d = GetComponent<Rigidbody2D>();
        healthTimer = 1f;
        //NOTE:  TO TELEPORT to Y position 5
        // transform.position = new Vector3(transform.position.x, 5, transform.position.z);
    }

    public void Hit(int damage)
    {
        health = health - damage;
        //gameObject.in
        //var childCanvas = gameObject.transform.GetChild(0).gameObject;
        //var childText = EnemyDamageText;
        //childText.SetActive(true);
        EnemyDamageText.transform.position = gameObject.transform.position;

        //cloneShield = Instantiate(shield, shieldPos, transform.rotation);

        //healthout = true;
        var curvec = gameObject.transform.transform.up;
        curvec.y = curvec.y + 10;
        //childText.transform.LookAt(curvec);
        //damage health position
        //mousePos.z = transform.position.z;
        //var shieldPos = Vector3.MoveTowards(transform.position, mousePos, 10 * Time.deltaTime);
        //cloneShield = Instantiate(shield, shieldPos, transform.rotation);
        //origTrans = childText.transform;
        // childText.transform.Translate(childText.transform.up * 2,,);
        EnemyDamageText.GetComponent<Text>().text = "- " + damage.ToString();
       
        playerObject.GetComponent<PlayerController>().SetLastHitEnemyInfo(EnemyName, health);
        nextHealth = Time.time + healthTimer;
        //HealthText.text = "" + health.ToString();
        if (health<=0)
        {
            Death();
            
            //var destobj = DestroyWall1;
            //Destroy(destobj);
        }
    }

    void Death()
    {
        //NOTE:  Replace this with not awful code, make an enemy class, etc. etc.
        if (EnemyName == "Lesser Light Elemental")
        {
            //Debug.Log("triggered " + EnemyName);
            Instantiate(healthCrystal, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(WallToDestroy);
        }
        else if (EnemyName == "Lesser Dark Elemental")
        {
            Destroy(gameObject);
            var destroywall = GameObject.Find("DestroyWall2");
            Destroy(destroywall);
            var destroywall2 = GameObject.Find("DestroyWall3");
            Destroy(destroywall2);
            var destroywall3 = GameObject.Find("DestroyWall4");
            Destroy(destroywall3);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Attack"))
        {
            //Debug.Log("triggered " + other.gameObject.name);
            //    other.gameObject.SetActive(false);
            Hit(1);
            //SetHitText();
        }

        //if (other.gameObject.CompareTag("Player"))
        //{
        //    //myObject.GetComponent<MyScript>().MyFunction()
        //    //Debug.Log("triggered " + other.gameObject.name);
        //    //    other.gameObject.SetActive(false);
        //     other.gameObject.GetComponent<PlayerController>().TakeDamage(5, GetComponent<Collider2D>());
        //    //SetHitText();
        //}

        //if (other.gameObject.CompareTag("Wall"))
        //{

            //myObject.GetComponent<MyScript>().MyFunction()
            //Debug.Log("triggered " + other.gameObject.name);
            //    other.gameObject.SetActive(false);
            // other.gameObject.GetComponent<PlayerController>().TakeDamage(5, GetComponent<Collider2D>());
            //SetHitText();
        //}
    }



    void FixedUpdate()
    {
        //var childCanvas = gameObject.transform.GetChild(0).gameObject;
        //if(childCanvas !=null)
        //{
            //if(EnemyName == "Lesser Light Elemental")
            //{
                //var childText = childCanvas.gameObject.transform.GetChild(0).gameObject;
                //if(childText.activeInHierarchy)
                //{
                    //childText.transform.Translate(childText.transform.up  * Time.deltaTime);
                    if (Time.time > nextHealth)
                    {

                        //childText.transform.position = gameObject.transform.position;

                        EnemyDamageText.GetComponent<Text>().text = "" ;
                    }


                //}
            //}
        //}

        float z = Mathf.Atan2((player.transform.position.y - transform.position.y), (player.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;
        
        transform.eulerAngles = new Vector3(0, 0, z);

        speed = 5;


        DoAI();

        //myTransform.rigidbody.velocity = Vector3(lookDir.normalized moveSpeed Time.deltaTime);


        //shot.transform = player;
    }

    private void Attack()
    {
        
    }

    private void DoAI()
    {
        //NOTE:  Replace this with not awful code, make an enemy class, etc. etc.
        if(EnemyName == "Lesser Light Elemental")
        {
            //Debug.Log("triggered " + EnemyName);
            LesserLightEleAI();
        }
        else if (EnemyName == "Lesser Dark Elemental")
        {
           
            LesserDArkEleAI();
        }
    }

    private void LesserLightEleAI()
    {
        //make enemy get close to you, or run away if you get to close to it.
        float distance = Vector3.Distance(gameObject.transform.position, player.position);
        if (distance < 5)
        {
            var fireRate = 1.5f;
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Instantiate(shot, transform.position, transform.rotation);
                shot.GetComponent<ShotScript>().SetPlayer(player);
                shot.SetActive(true);
                
                rb2d.velocity = Vector3.zero;
            }
            else
            {
                Vector2 moveDir = transform.position - player.transform.position;

                if (distance < 2)
                {
                    rb2d.AddForce(moveDir * speed);

                }
                else
                {
                    rb2d.AddForce(gameObject.transform.up * speed);
                }
            }
        }
    }

    private void LesserDArkEleAI()
    {
        float distance = Vector3.Distance(gameObject.transform.position, player.position);
        if (distance < 5)
        {
            Vector2 moveDir = transform.position - player.transform.position;
            //rb2d.AddForce(gameObject.transform.up * speed);
            var fireRate = 2;
            if (Time.time > nextFire && distance < 0.5f && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack.attack"))
            {
                Debug.Log("is animating" + anim.GetCurrentAnimatorStateInfo(0).IsName("Attack.attack"));
                nextFire = Time.time + fireRate;
                anim.SetTrigger("Attack");
                //shot.GetComponent<ShotScript>().SetPlayer(player);
               

            }
            else if(anim.GetCurrentAnimatorStateInfo(0).IsName("Attack.attack"))
            {
                Debug.Log("is animating" + anim.GetCurrentAnimatorStateInfo(0).IsName("Attack.attack"));
                rb2d.velocity = Vector3.zero;
            }
            else
            {
              
                rb2d.AddForce(gameObject.transform.up * speed);
            }
        }
    }
}
