using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour {

    //public GameObject hit;

    //void OnTriggerEnter2D(Collider2D other)
    //{
        //other.Trigger to call hit?
      //  Instantiate(hit, other.transform.position, Quaternion.identity);
       // other.BroadcastMessage("Hit");
        
        //Destroy(other.gameObject);
    //}

	// Use this for initialization
	void Start () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.gameObject.CompareTag("EnemyAttack"))
        //{
        //    if ()
        //    {
        //        Debug.Log("triggered " + gameObject.name);
        //        health = health - 5;
        //        SetHitText();
        //        //knock away
        //        rb2d.AddForce(other.gameObject.transform.up * (speed * 50));
        //    }

        //}
    }

    // Update is called once per frame
    void Update () {
		
	}
}
