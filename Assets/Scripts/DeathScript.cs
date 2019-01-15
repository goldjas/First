using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        Invoke("Die", 1f);
    }
		
	void Die()
        {

        }
	
	// Update is called once per frame
	void Update () {
		
	}
}
