using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tiendaScript : MonoBehaviour {

    private bool showMessage;

	// Use this for initialization
	void Start () {
        
        print("dasilva puto");
	}

    void OnGUI()
    {
        if (showMessage) {
            GUI.Label(new Rect(230, 145, 300, 300), "press E");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Jugador") {
            showMessage = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Jugador")
        {
            showMessage = false;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
