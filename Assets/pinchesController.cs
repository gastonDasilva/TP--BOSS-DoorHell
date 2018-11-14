using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pinchesController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Jugador") {
            collision.gameObject.SendMessage("EnemyKnockBack",this.gameObject);
        }
        print("HOLAAAAAAAAaa");
    }


    // Update is called once per frame
    void Update () {
		
	}
}
