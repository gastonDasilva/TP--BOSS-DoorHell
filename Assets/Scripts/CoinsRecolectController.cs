using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsRecolectController : MonoBehaviour {

    public  GameObject game;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            collision.gameObject.transform.parent.SendMessage("CantidadDePuntosPorMonedas", game);
            collision.gameObject.transform.parent.SendMessage("GestionarRecoleccion");
            Debug.Log("Coin!!!");
            Destroy(collision.gameObject);
        }

    }
}
