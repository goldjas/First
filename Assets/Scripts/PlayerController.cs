using Assets.Scripts.Class;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PlayerController : MonoBehaviour {

    public float spriteBlinkingTimer = 0.0f;
    public float spriteBlinkingMiniDuration = 0.1f;
    public float spriteBlinkingTotalTimer = 0.0f;
    public float spriteBlinkingTotalDuration = 0.5f;
    public bool startBlinking = false;
    public GameObject shield;
    public GameObject SwordLaser;
    public GameObject ThrowDagger;
    private GameObject cloneShield;
    private GameObject cloneSwordLaser;
    private GameObject cloneThrowDagger;
    public AudioClip PickupClip;
    public AudioClip UseClip;
    public Text ItemGetText;
    public Collider2D CurrentCollidingWith;
    public float DisplayTimer;

    public bool CollidedWithStaticPickup;

    private float Heals;
    private float MaxHeals;
    public Image CrystalPicture;
    public Text HealsText;

    public bool HealVisible;

    public PlayerCharacter thePlayerCharacter { get; set; }
    private float HealingValue;

    public GameObject escapeMenus;
    public GameObject inventoryMenus;
    public GameObject itemsMenu;
    public bool isPaus;

    public float tilt;
    //public Done_Boundary boundary;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    public float dashTimer;
    public float healTimer;
    private bool invincible;

    public float speed;            

    public Text HealthText;       
    private float health;
    private float MaxHealth;

    public Text EnergyText;
    private float energy;
    private float MaxEnergy;

    public Text PointsText;
    private float points;

    public Text CurrentEnemyNameText;

    public Text CurrentEnemyHealthText;

    public Text Spell1Text;

    private bool CrystalAvailable;

    private Rigidbody2D rb2d;       
    //private int count;
    Animator anim;

    //Fire3(shift) for Dash

    private float nextFire;
    private float nextDash;
    private bool ShieldUp;
    private bool ShieldOut;
    private bool Shielded;

    //TODO:  Make more smaller classes and stuff so this dumb file isn't so ginormous.


    // Use this for initialization
    void Start()
    {
        healTimer = 0;
        ItemGetText.text = "";
        thePlayerCharacter = new PlayerCharacter();
        Shielded = false;
        ShieldUp = false;
        invincible = false;
        health = 100;
        MaxHealth = 100;
        HealingValue = 50;
        Heals = 2;
        MaxHeals = 2;
        CrystalAvailable = false;

        HealsText.text = "";

        energy = 50;
        MaxEnergy = 50;
        points = 0;
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
        // count = 0;
        SetHitText();
        SetEnergyText();
        SetPointText();

        anim = GetComponent<Animator>();

        escapeMenus.SetActive(false);
        thePlayerCharacter.Health = health;
        thePlayerCharacter.Energy = energy;
        thePlayerCharacter.XP = points;
        thePlayerCharacter.Weapons = new List<PlayerWeapon>
        {
            new PlayerWeapon("Rusty Sword")
        };

        thePlayerCharacter.Shields = new List<Shield>
        {
            new Shield("Wooden Shield")
        };

        foreach(var weapon in thePlayerCharacter.Weapons)
        {
            if(weapon.Name == "Rusty Sword")
            {
                weapon.Equipped = true;
                Spell1Text.text = weapon.Skill.Name;
            }
        }

        foreach (var shield in thePlayerCharacter.Shields)
        {
            if (shield.Name == "Wooden Shield")
            {
                shield.Equipped = true;
                // Spell1Text.text = weapon.Skill.Name;
            }
        }
    }

    void Update()
    {

        if(CrystalAvailable)
        {
            CrystalPicture.color = new Color(255, 255, 255, 255);
            HealsText.text = "x" + Heals + "/" + MaxHeals;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetPause();

            //SetCountText();
        }

        if(!isPaus)
        {
            if (startBlinking == true)
            {
                invincible = true;
                SpriteBlinkingEffect();
            }

            dashTimer = 2;
            if (Input.GetButton("Fire1") && Time.time > nextFire && !ShieldUp)
            {
                nextFire = Time.time + fireRate;
                if(thePlayerCharacter.Weapons.Where(z=>z.Equipped).Select(x=>x.Name).FirstOrDefault() == "Dagger")
                {
                    anim.SetTrigger("DaggerAttack");
                }
                else
                {
                    anim.SetTrigger("Attack");
                }
                

                //			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
                //GetComponent<AudioSource>().Play();

            }

            if (Input.GetButtonDown("Fire2") && Time.time > nextFire)
            {

                //getbutton down and getbutton up
                //Shield
                //nextFire = Time.time + fireRate;
                // anim.SetTrigger("Attack");
                ShieldUp = true;
                if (!ShieldOut)
                {
                    //   var cloneBomb=Instantiate(BombPrefab,bombPos,Quaternion.identity);
                    var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    mousePos.z = transform.position.z;
                    var shieldPos = Vector3.MoveTowards(transform.position, mousePos, 10 * Time.deltaTime);
                    cloneShield = Instantiate(shield, shieldPos, transform.rotation);

                    ShieldOut = true;
                    rb2d.velocity = Vector3.zero;
                }
            }

            if (Input.GetButtonUp("Fire2"))
            {
                ShieldUp = false;
                Destroy(cloneShield);
                ShieldOut = false;
                Shielded = false;
            }

            if (Input.GetButtonUp("Use"))
            {
                UseItem();
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

            }
        }

        if(Input.GetButtonUp("Heal"))
        {
            if(Heals > 0)
            {

                if(health < MaxHealth)
                {
                    health = health + HealingValue;
                    if(health > MaxHealth)
                    {
                        health = MaxHealth;
                    }
                    SetHitText();
                    AudioSource.PlayClipAtPoint(PickupClip, gameObject.transform.position);
                    Heals = Heals - 1;
                    rb2d.velocity = Vector3.zero;
                    healTimer = Time.time + 2;
                }
                
            }
        }

        if (Input.GetButton("Spell1") && Time.time > nextFire && !ShieldUp && energy > 0)
        {
           
             nextFire = Time.time + (fireRate * 2);
            //mousePos.z = transform.position.z;
            switch(thePlayerCharacter.Weapons.Where(z => z.Equipped).FirstOrDefault().Name )
            {
                case "Rusty Sword":
                    UseSwordBeam();
                    break;
                case "Sword":
                    UseSwordBeam();
                    break;
                case "Dagger":
                    UseDaggerStorm();
                    break;
            }
        }

        if(Time.time > DisplayTimer)
        {
            ItemGetText.text = "";
        }
    }

    void Heal()
    {

    }

    //This sucks
    void UseItem()
    {
        AudioSource.PlayClipAtPoint(UseClip, gameObject.transform.position);
        //var anotherCollider = GameObject.FindGameObjectWithTag("StaticPickup").GetComponent<Collider2D>();
        if (CollidedWithStaticPickup)
        {
            var curGameObj = CurrentCollidingWith.gameObject;
            if (!curGameObj.GetComponent<SetPickupScript>().PickedUp)
            {
                if (curGameObj.name == "ChestDagger")
                {
                    ItemGetText.text = "Get Dagger";
                    var curAnim = curGameObj.GetComponent<Animator>();
                    curAnim.SetTrigger("Opened");
                    DisplayTimer = Time.time + 3;
                    curGameObj.GetComponent<SetPickupScript>().PickedUp = true;
                    thePlayerCharacter.Weapons.Add(new PlayerWeapon("Dagger"));
                }

                if (curGameObj.name == "ChestBow")
                {
                    ItemGetText.text = "Get Bow";
                    var curAnim = curGameObj.GetComponent<Animator>();
                    curAnim.SetTrigger("Opened");
                    DisplayTimer = Time.time + 3;
                    curGameObj.GetComponent<SetPickupScript>().PickedUp = true;
                    thePlayerCharacter.Weapons.Add(new PlayerWeapon("Bow"));
                }

                if (curGameObj.name == "ChestWand")
                {
                    ItemGetText.text = "Get Wand";
                    var curAnim = curGameObj.GetComponent<Animator>();
                    curAnim.SetTrigger("Opened");
                    DisplayTimer = Time.time + 3;
                    curGameObj.GetComponent<SetPickupScript>().PickedUp = true;
                    thePlayerCharacter.Weapons.Add(new PlayerWeapon("Wand"));
                }

                if (curGameObj.name == "ChestSword")
                {
                    ItemGetText.text = "Get Sword";
                    var curAnim = curGameObj.GetComponent<Animator>();
                    curAnim.SetTrigger("Opened");
                    DisplayTimer = Time.time + 3;
                    curGameObj.GetComponent<SetPickupScript>().PickedUp = true;
                    thePlayerCharacter.Weapons.Add(new PlayerWeapon("Sword"));
                }
                if(curGameObj.name == "DarkSwitch")
                {
                    curGameObj.GetComponent<LightDarkSwitchController>().FlipSwitch();
                }
            }

        }
    }

    void UseSwordBeam()
    {
        cloneSwordLaser = Instantiate(SwordLaser, gameObject.transform.position, transform.rotation);
        var cloneswordscript = cloneSwordLaser.GetComponent<SwordShotScript>();
        cloneswordscript.thePlayerCharacter = thePlayerCharacter;
        energy = energy - 10;
        SetEnergyText();
    }

    void UseDaggerStorm()
    {
        var cloneThrowDagger1 = Instantiate(ThrowDagger, gameObject.transform.position, transform.rotation);
        var cloneswordscript = cloneThrowDagger1.GetComponent<SwordShotScript>();
        cloneswordscript.thePlayerCharacter = thePlayerCharacter;
        var numDags = 8;
        for(var dag = 1; dag < numDags;  dag ++)
        {
            GameObject rotatedObject = new GameObject();
            rotatedObject.transform.Rotate(gameObject.transform.position.x , gameObject.transform.position.y, gameObject.transform.position.z + 45 * dag);
            var cloneThrowDaggerx = Instantiate(ThrowDagger, gameObject.transform.position, rotatedObject.transform.rotation);
            var cloneswordscriptx = cloneThrowDaggerx.GetComponent<SwordShotScript>();
            cloneswordscriptx.thePlayerCharacter = thePlayerCharacter;
        }
        energy = energy - 10;
    }

    void UseNova()
    {

    }

    void UseMultiShot()
    {

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("StaticPickup") | other.gameObject.CompareTag("SavePoint"))
        {
           
            var itsSprite = other.gameObject.GetComponent<SpriteRenderer>();
            itsSprite.color = new Color(255, 255, 255);
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
        if(!ShieldUp && Time.time > healTimer)
        {
            rb2d.AddForce(movement * speed);
        }

        SetAbilityText();

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {

            if (other.gameObject.name == "HealthCrystal(Clone)")
            {
                CrystalPicture.color = new Color(255, 255, 255, 255);
                HealsText.text = "x" + Heals + "/" + MaxHeals;
                CrystalAvailable = true;
            }
            AudioSource.PlayClipAtPoint(PickupClip, gameObject.transform.position);
            Destroy(other.gameObject);

        }

        if (other.gameObject.CompareTag("StaticPickup") | other.gameObject.CompareTag("SavePoint"))
        {
            var itsSprite = other.gameObject.GetComponent<SpriteRenderer>();
            CurrentCollidingWith = other.gameObject.GetComponent<Collider2D>();
            var curGameObj = CurrentCollidingWith.gameObject;
            if (!curGameObj.GetComponent<SetPickupScript>().PickedUp)
            {
                itsSprite.color = new Color(255, 0, 0);
            }
                
            CollidedWithStaticPickup = true;
            
        }
        else
        {
            CollidedWithStaticPickup = false;
        }

        //if (other.gameObject.CompareTag("EnemyAttack"))
        //{
        //    TakeDamage(10, other, true);
        //    //other.gameObject.GetComponent<PlayerController>().TakeDamage(10, GetComponent<Collider2D>(), false);
        //}


    }


    public void SetPause()
    {
        if (!isPaus)
        {
            Time.timeScale = 0;

        }
        else
        {
            Time.timeScale = 1;
        }
        isPaus = !isPaus;
        escapeMenus.SetActive(isPaus);
        inventoryMenus.SetActive(false);
        itemsMenu.SetActive(false);
    }

    public void TakeDamage(int damage, Collider2D other, bool shielded)
    {
        //check if shield is out and then x and y coordinates on shield instead?
       
        if (Shielded)
        {
            damage = damage / 2;
            Shielded = false;
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

        if(health <= 0)
        {
            PlayerDeath();
        }

    }

    private void PlayerDeath()
    {
        Destroy(gameObject);

    }

    public void SetShieldedToTrue()
    {
        Shielded = true;
    }

    void SetHitText()
    {
        HealthText.text = "Health: " + health.ToString() + "/" + MaxHealth.ToString();
    }



    void SetEnergyText()
    {
        EnergyText.text = "Energy: " + energy.ToString() + "/" + MaxEnergy.ToString();

    }

    void SetPointText()
    {
        PointsText.text = "EE: " + points.ToString();
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
            if (gameObject.GetComponent<SpriteRenderer>().enabled == true)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;  //make changes
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;   //make changes
            }
        }
    }

    public void SetLastHitEnemyInfo(string currentEnemyName, float currentEnemyHealth)
    {
        CurrentEnemyNameText.text = currentEnemyName;
        CurrentEnemyHealthText.text = currentEnemyHealth.ToString();
    }

    public void SetAbilityText()
    {
        foreach (var weapon in thePlayerCharacter.Weapons)
        {
            if (weapon.Equipped)
            {
                Spell1Text.text = weapon.Skill.Name;
            }
        }
    }

}
