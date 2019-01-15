using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy1Script : MonoBehaviour {

    public float speed;
    //transform is the position
    public Transform player;
    public Text HealthText;
    private float nextFire;
    public GameObject shot;
    public Transform shotSpawn;

    private Rigidbody2D rb2d;

    float health;

	// Use this for initialization
	void Start ()
    {
        health = 10;
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Hit()
    {
        health--;
        HealthText.text = "" + health.ToString();
        if(health<=0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Attack"))
        {
            //Debug.Log("triggered " + other.gameObject.name);
            //    other.gameObject.SetActive(false);
            Hit();
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
        float z = Mathf.Atan2((player.transform.position.y - transform.position.y), (player.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;
        speed = 5;
        transform.eulerAngles = new Vector3(0, 0, z);

        //make enemy get close to you, or run away if you get to close to it.
        
        float distance = Vector3.Distance(gameObject.transform.position, player.position);
        Vector2 moveDir = transform.position - player.transform.position;
   
        if (distance < 2)
        {
            rb2d.AddForce(moveDir * speed);

        }
        else
        {
            rb2d.AddForce(gameObject.transform.up * speed);
        }

        var fireRate = 2;
        if ( Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, transform.position, transform.rotation);
            shot.SetActive(true);
            shot.GetComponent<ShotScript>().SetPlayer(player);

        }

        //myTransform.rigidbody.velocity = Vector3(lookDir.normalized moveSpeed Time.deltaTime);


        //shot.transform = player;
    }
}
