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
    public GameObject WallToDestroy;
    public GameObject playerObject;
    private string enemyName;
    public Text DamageTakenText;

    public float healthTimer;
    public float nextHealth;

    private Rigidbody2D rb2d;
    private bool healthout;

    float health;

	// Use this for initialization
	void Start ()
    {
        //playerObject = GameObject.FindGameObjectWithTag("Player");
        enemyName = "Lesser Light Elemental";
        health = 10;
        rb2d = GetComponent<Rigidbody2D>();
        healthTimer = 0.5f;
    }

    void Hit()
    {
        var damage = 1;
        health = health - damage;
        //gameObject.in
        var childCanvas = gameObject.transform.GetChild(0).gameObject;
        var childText = childCanvas.gameObject.transform.GetChild(0).gameObject;
        childText.SetActive(true);
        healthout = true;
        var curvec = gameObject.transform.transform.up;
        curvec.y = curvec.y + 10;
        //childText.transform.LookAt(curvec);
        //damage health position
        //mousePos.z = transform.position.z;
        //var shieldPos = Vector3.MoveTowards(transform.position, mousePos, 10 * Time.deltaTime);
        //cloneShield = Instantiate(shield, shieldPos, transform.rotation);
        //origTrans = childText.transform;
       // childText.transform.Translate(childText.transform.up * 2,,);
        childText.GetComponent<Text>().text = "- " + damage.ToString();
        //  shot.GetComponent<ShotScript>().SetPlayer(player);
        playerObject.GetComponent<PlayerController>().SetLastHitEnemyInfo(enemyName, health);
        nextHealth = Time.time + healthTimer;
        //HealthText.text = "" + health.ToString();
        if (health<=0)
        {
            Destroy(gameObject);
            Destroy(WallToDestroy);
            
            //var destobj = DestroyWall1;
            //Destroy(destobj);
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
        var childCanvas = gameObject.transform.GetChild(0).gameObject;
        var childText = childCanvas.gameObject.transform.GetChild(0).gameObject;
        if(childText.activeInHierarchy)
        {
            //childText.transform.Translate(childText.transform.up  * Time.deltaTime);
            if (Time.time > nextHealth)
            {

                //childText.transform.position = gameObject.transform.position;
                childText.SetActive(false);
            }

        }
         


        float z = Mathf.Atan2((player.transform.position.y - transform.position.y), (player.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;
        speed = 5;
        transform.eulerAngles = new Vector3(0, 0, z);

        //make enemy get close to you, or run away if you get to close to it.

        
        
        float distance = Vector3.Distance(gameObject.transform.position, player.position);
        if(distance < 5)
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

            var fireRate = 2;
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Instantiate(shot, transform.position, transform.rotation);
                shot.SetActive(true);
                shot.GetComponent<ShotScript>().SetPlayer(player);

            }
        }


        //myTransform.rigidbody.velocity = Vector3(lookDir.normalized moveSpeed Time.deltaTime);


        //shot.transform = player;
    }
}
