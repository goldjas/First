using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Assets.Scripts.Class;

public class SwordShotScript : MonoBehaviour
{
    private int damage;  
    public float speed;
    public GameObject player;
    Vector2 playpos;
    Rigidbody2D rb2d;
    bool isCollidedWithShield;
    public PlayerCharacter thePlayerCharacter;
    // Start is called before the first frame update
    //public SwordShotScript(GameObject player)
    //{
    //    player = player;
    //}

    void Start()
    {
        //damage = 2;
        //    var test = _player.GetComponent<PlayerController>().thePlayerCharacter;

        
        damage = thePlayerCharacter.Weapons.Where(x=>x.Equipped).FirstOrDefault().Skill.Damage;
        //speed = 10;
    }

    // Update is called once per frame
    void Update()
    {
        rb2d = GetComponent<Rigidbody2D>();
        speed = 2;

        // distance moved since last frame:
        float amtToMove = 5 * Time.deltaTime;
        // translate projectile in its forward direction:
        transform.Translate(Vector2.up * amtToMove);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy1Script>().Hit(damage);
            Destroy(gameObject);
        }
    }
}
