using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {


   // public float waitBeforeDestroy;
    public Vector3 mov;
    public float speed;
    public bool soyBolaDeAcido = false;

    private Rigidbody2D rb2d;
    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>(); // detecta automaticamente el rigidbody

    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = new Vector3(mov.x, mov.y, 0) * speed * Time.deltaTime;
        float dir = mov.x;
        rb2d.MovePosition(new Vector3(transform.position.x + (dir * speed) * Time.deltaTime, transform.position.y, transform.position.z));
        cambiarDireccion(dir);

        //transform.position + (dir * speed) * Time.deltaTime);
    }

     void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Wizard" && !soyBolaDeAcido)
        {
            BallDestroy();
        }

        if (collision.gameObject.tag == "Jugador" && soyBolaDeAcido)
        {
            Debug.Log("LLEGO AL PLAYER");
            collision.gameObject.SendMessage("EfecuarEnvenamientoPorUnTiempo",8f);
            BallDestroy();
        }
    }

    void OnBecameInvisible()
    {
        BallDestroy();
    }
    void cambiarDireccion(float h)
    {
        if (h > 0.1f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f); // Gestiona la direccion de la animacion
        }

        if (h < -0.1f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void BallDestroy()
    {
        Destroy(gameObject);
    }
}
