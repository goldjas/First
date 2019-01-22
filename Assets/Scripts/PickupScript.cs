using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("triggered " + other.gameObject.name);
            //    other.gameObject.SetActive(false);
            //Hit();
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
}
