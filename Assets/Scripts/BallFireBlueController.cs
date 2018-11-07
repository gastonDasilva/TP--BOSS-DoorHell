using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFireBlueController : MonoBehaviour {

    public float speed;

    private Rigidbody2D rb2d;
    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>(); // detecta automaticamente el rigidbody
    }
	
	// Update is called once per frame
	void Update () {
        float dir = 1f;
        transform.position = new Vector3(transform.position.x, transform.position.y + (dir * speed) * Time.deltaTime, transform.position.z);
    }

    public void BallDestroy()
    {
        Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        BallDestroy();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Jugador")
        {
            Debug.Log("Ojito jugador");
            collision.gameObject.SendMessage("RecibirDanho", 3f);
        }
    }

    }
