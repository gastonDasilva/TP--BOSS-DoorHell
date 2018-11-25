using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour {

    public int CoinType;

    private Rigidbody2D rb2d;
    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>(); // detecta automaticamente el rigidbody
    }
	
	// Update is called once per frame
	void Update () {
        //this.gameObject.name

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            rb2d.bodyType = RigidbodyType2D.Static;
        }
    }

    public int CantidadDePuntosPorMonedas(GameObject game)
    {
        switch (CoinType)
        {
            case 1:
                game.SendMessage("IncreasePoint", 1);
                return 1;
            case 2:
                game.SendMessage("IncreasePoint", 2);
                return 2;
            case 3:
                game.SendMessage("IncreasePoint", 3);
                return 3;
            case 4:
                game.SendMessage("IncreasePoint", 4);
                return 4;
            case 5:
                game.SendMessage("IncreasePoint", 5);
                return 5;
            case 6:
                GestionDeObtencionDeCoinRuby(game,6);
                return 6;
            default:
                break;
        }
        game.SendMessage("IncreasePoint", 5);
        return 5;
    }

    public void GestionDeObtencionDeCoinRuby(GameObject game, int coinType)
    {
        game.SendMessage("IncreasePoint", coinType);
    }

    public void GestionarRecoleccion()
    {
        //game.SendMessage("IncreasePoint");
        /*CircleCollider2D col = this.gameObject.GetComponent<CircleCollider2D>();
        col.enabled = true;*/

        //Invoke("DestroyCoin", 0f);
        DestroyCoin();
    }

    public void ImpulseCoin()
    {
        System.Random rnd = new System.Random();
        int rInt = rnd.Next(0, 2);
        int dirRInt = rnd.Next(0, 2);
        rb2d = GetComponent<Rigidbody2D>();
        //float side = Mathf.Sign((transform.position.x +2) - transform.position.x);
        rb2d.AddForce(DireccionDeImpulsoRandom(dirRInt) * rInt * 5f, ForceMode2D.Impulse); // Fisica del SALTO para atras
    }

    public Vector2 DireccionDeImpulsoRandom(int n)
    {
        switch (n)
        {
            case 0:
                return Vector2.left;
            case 1:
                return Vector2.right;
            default:
                break;
        }

        return Vector2.left;
    }

    void DestroyCoin()
    {
        Destroy(this.gameObject);
    }
}
