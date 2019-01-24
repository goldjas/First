using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimAttackScript : MonoBehaviour
{
    int damage;   //pass this in from the monster so we can use straight shot on anyone maybe?
    public float speed;
    public Transform player;
    Vector2 playpos;
    Rigidbody2D rb2d;
    bool isCollidedWithShield;

    // Start is called before the first frame update
    void Start()
    {
        damage = 10;
        speed = 5;
    }

    public void SetPlayer(Transform passedPlayer)
    {
        player = passedPlayer;

        speed = 5;
        playpos = player.transform.position;

        Vector2 enemyPos = gameObject.transform.position;
        Vector2 movedir = (enemyPos - playpos);

        GameObject go = GameObject.FindGameObjectWithTag("Player");
        //var target = go.transform;
        // rotate the projectile to aim the target:
        //transform.LookAt(target);
    }

    public void OnCollisionEnter2d(Collision collision)
    {
        isCollidedWithShield = false;
        if (collision.gameObject.CompareTag("Shield"))
        {
            isCollidedWithShield = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        isCollidedWithShield = false;
        rb2d = GetComponent<Rigidbody2D>();
        speed = 1;

        // distance moved since last frame:
        float amtToMove = 5 * Time.deltaTime;
        // translate projectile in its forward direction:
        transform.Translate(Vector2.up * amtToMove);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.gameObject.CompareTag("Attack"))
        //{
        //    Debug.Log("triggered " + other.gameObject.name);
        //    //    other.gameObject.SetActive(false);
        //    Hit();
        //    //SetHitText();
        //}
        if (other.gameObject.CompareTag("Shield"))
        {
            isCollidedWithShield = true;

        }

        if (other.gameObject.CompareTag("Player") && isCollidedWithShield)
        {
            //myObject.GetComponent<MyScript>().MyFunction()
            // Debug.Log("triggered " + other.gameObject.name);
            //    other.gameObject.SetActive(false);
            Debug.Log("shielded");
            other.gameObject.GetComponent<PlayerController>().TakeDamage(damage, GetComponent<Collider2D>(), true);
            isCollidedWithShield = false;
            // Debug.Log("shielded ");
            //SetHitText();
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().TakeDamage(damage, GetComponent<Collider2D>(), false);
        }

        //if (other.gameObject.CompareTag("Wall"))
        //{
        //    //myObject.GetComponent<MyScript>().MyFunction()
        //    // Debug.Log("triggered " + other.gameObject.name);
        //    //    other.gameObject.SetActive(false);
        //    Destroy(gameObject);
        //    //SetHitText();
        //}


        //        Vector2 moveDir = transform.position - player.transform.position;
        //Debug.Log("triggered " + moveDir.normalized);
        //if (distance < 2)
        //{
        //    rb2d.AddForce(moveDir * speed);

        //}
        //else
        //{
        //    rb2d.AddForce(gameObject.transform.up * speed);
        //}

    }
}
