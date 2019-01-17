using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour {

    public float spriteBlinkingTimer = 0.0f;
    public float spriteBlinkingMiniDuration = 0.1f;
    public float spriteBlinkingTotalTimer = 0.0f;
    public float spriteBlinkingTotalDuration = 0.2f;
    public bool startBlinking = false;
    public GameObject shield;
    private GameObject cloneShield;

    public float tilt;
    //public Done_Boundary boundary;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    public float dashTimer;
    private bool invincible;

    public float speed;            

    public Text HealthText;       
    public float health;

    public Text EnergyText;
    public float energy;

    public Text PointsText;
    public float points;

    public Text CurrentEnemyNameText;

    public Text CurrentEnemyHealthText;

    private Rigidbody2D rb2d;       
    //private int count;
    Animator anim;

    //Fire3(shift) for Dash

    private float nextFire;
    private float nextDash;
    private bool ShieldUp;
    private bool ShieldOut;
    private bool Shielded;

    // Use this for initialization
    void Start()
    {
        Shielded = false;
        ShieldUp = false;
        invincible = false;
        health = 100;
        energy = 50;
        points = 0;
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
        // count = 0;
        SetHitText();
        SetEnergyText();
        SetPointText();

        anim = GetComponent<Animator>();
    }

    void Update()
    {

        if (startBlinking == true)
        {
            invincible = true;
            SpriteBlinkingEffect();
        }

        dashTimer = 3;
        if (Input.GetButton("Fire1") && Time.time > nextFire  && !ShieldUp)
        {
            nextFire = Time.time + fireRate;
            anim.SetTrigger("Attack");

            //			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            //GetComponent<AudioSource>().Play();

        }

        if (Input.GetButtonDown("Fire2") && Time.time > nextFire )
        {
            
            //getbutton down and getbutton up
            //Shield
            //nextFire = Time.time + fireRate;
            // anim.SetTrigger("Attack");
            ShieldUp = true;
            if(!ShieldOut)
            {
                //   var cloneBomb=Instantiate(BombPrefab,bombPos,Quaternion.identity);
                var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                mousePos.z = transform.position.z;
                var shieldPos = Vector3.MoveTowards(transform.position, mousePos, 10 * Time.deltaTime);
                cloneShield = Instantiate(shield, shieldPos, transform.rotation);
               
                ShieldOut = true;

                //stop moving when shield is out
                rb2d.velocity = Vector3.zero;
                //rb2d.angularVelocity = Vector3.zero;

            }
        }

        if (Input.GetButtonUp("Fire2"))
        {
            ShieldUp = false;
            Destroy(cloneShield);
            ShieldOut = false;
            Shielded = false;
        }


            if (Input.GetButton("Fire3") && Time.time > nextDash)
        {
            nextDash = Time.time + dashTimer;
            //Store the current horizontal input in the float moveHorizontal.
            float moveHorizontal = Input.GetAxis("Horizontal");

            //Store the current vertical input in the float moveVertical.
            float moveVertical = Input.GetAxis("Vertical");
            Vector2 movement = new Vector2(moveHorizontal, moveVertical);
            startBlinking = true;
            rb2d.AddForce(movement * (speed * 150));
            

            //			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            //GetComponent<AudioSource>().Play();

        }
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        //follow mouse thing
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Quaternion rot = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward);
        transform.rotation = rot;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
        //rigidbody2D.angularVelocity = 0;



        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");

        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis("Vertical");

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        //Vector2 stopMovement = new Vector2(0, 0);
        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        //rb2d.

        //this is what actually moves the thing
        if(!ShieldUp)
        {
            rb2d.AddForce(movement * speed);
        }
        
    }
    
    
    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.CompareTag("EnemyAttack"))
    //    {
    //        //if()
    //        //{

    //            health = health - 5;
    //            SetHitText();
    //            //knock away
    //            rb2d.AddForce(other.gameObject.transform.up * (speed * 50));
    //        //}

    //    }
    //}

    public void TakeDamage(int damage, Collider2D other, bool shielded)
    {
        //check if shield is out and then x and y coordinates on shield instead?
        
        //Debug.Log("triggered start" );
        if (Shielded)
        {
            //   // Debug.Log("triggered " + other.gameObject.name);
            damage = damage / 2;
            Shielded = false;

            //Debug.Log("triggered " + damage.ToString());
        }

        if(!invincible)
        {
            health = health - damage;
            SetHitText();
            //knock away
            if(!ShieldOut)
            {
                rb2d.AddForce(other.gameObject.transform.up * (speed * 50));
            }
            
            startBlinking = true;
        }

    }

    public void SetShieldedToTrue()
    {
        Shielded = true;
    }

    void SetHitText()
    {
        HealthText.text = "Health: " + health.ToString();
    }



    void SetEnergyText()
    {
        EnergyText.text = "Energy: " + energy.ToString();
    }

    void SetPointText()
    {
        PointsText.text = "XP: " + points.ToString();
    }


    private void SpriteBlinkingEffect()
    {
        spriteBlinkingTotalTimer += Time.deltaTime;
        if (spriteBlinkingTotalTimer >= spriteBlinkingTotalDuration)
        {
            startBlinking = false;
            invincible = false;
            spriteBlinkingTotalTimer = 0.0f;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;   // according to 
                                                                             //your sprite
            return;
        }

        spriteBlinkingTimer += Time.deltaTime;
        if (spriteBlinkingTimer >= spriteBlinkingMiniDuration)
        {
            spriteBlinkingTimer = 0.0f;
            if (this.gameObject.GetComponent<SpriteRenderer>().enabled == true)
            {
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false;  //make changes
            }
            else
            {
                this.gameObject.GetComponent<SpriteRenderer>().enabled = true;   //make changes
            }
        }
    }

    public void SetLastHitEnemyInfo(string currentEnemyName, float currentEnemyHealth)
    {
        CurrentEnemyNameText.text = currentEnemyName;
        CurrentEnemyHealthText.text = currentEnemyHealth.ToString();
    }

}
