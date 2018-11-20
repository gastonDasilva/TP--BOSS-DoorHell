using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tiendaScript : MonoBehaviour {

    private bool showMessage;

    public GameObject text;

	// Use this for initialization
	void Start () {
        
	}

    /*void OnGUI()
    {
        if (showMessage) {
            GUI.Label(new Rect(230, 145, 300, 300), "Press E");
        }
    }*/

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Jugador") {
            //showMessage = true;
            text.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Jugador")
        {
            //showMessage = false;
            text.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update () {


    }
}
