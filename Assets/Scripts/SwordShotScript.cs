﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordShotScript : MonoBehaviour
{
    private int damage;  
    public float speed;
    public Transform player;
    Vector2 playpos;
    Rigidbody2D rb2d;
    bool isCollidedWithShield;
    // Start is called before the first frame update
    void Start()
    {
        damage = 2;
        speed = 10;
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
