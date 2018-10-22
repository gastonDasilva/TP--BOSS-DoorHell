using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControler : MonoBehaviour {
    public float maxSpeed = 1f;
    public float speed = 1f;
    public GameObject CoinPreFab01 = null;
    public GameObject CoinPreFab02 = null;
    public GameObject CoinPreFab03 = null;
    public GameObject CoinPreFab04 = null;
    public GameObject CoinPreFab05 = null;


    private Rigidbody2D rb2d;
    private Animator anim;
    private bool precipicio = false;
    private PolygonCollider2D polcol;
    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>(); // detecta automaticamente el rigidbody
        anim = GetComponent<Animator>();// detecta automaticamente el animator, para gestionar las animaciones
        polcol = GetComponentInChildren<PolygonCollider2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        GestionDeMovimiento();
    }

    void GestionDeMovimiento()

    {
        if (rb2d.bodyType != RigidbodyType2D.Static)
        {
            rb2d.AddForce(Vector2.right * speed);
            float limitedSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
            rb2d.velocity = new Vector2(limitedSpeed, rb2d.velocity.y);
            GestionarMovimientoAlDetecatarUnPrecipicio();
        }

    }

    void GestionarMovimientoAlDetecatarUnPrecipicio()
    {
        if (precipicio) // si choca con un precipicio  Entra al if
        {
            speed = -speed;
            rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
            CambiarDireccionDependiendo(speed);
            precipicio = false;
        }
    }

    void CambiarDireccionDependiendo(float s)// Dependiendo de la speed
    {
        if (s > 0.1f && rb2d.bodyType != RigidbodyType2D.Static && precipicio == true)
        {
            transform.localScale = new Vector3(1f, 1f, 1f); // Gestiona la direccion de la animacion
        }

        if (s < -0.1f && rb2d.bodyType != RigidbodyType2D.Static && precipicio == true)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Precipicio")
        {
            precipicio = true;
        }

        if (collision.gameObject.tag == "Jugador")
        {
            Debug.Log("Choco con el player");
            collision.gameObject.SendMessage("EnemyKnockBack", this.gameObject.transform.position.x);
        }

        if (collision.gameObject.tag == "JugadorAtaque")
        {
            Debug.Log("Enemido en el punto para atacar");
            collision.gameObject.transform.parent.SendMessage("EstoyAtacando", this.gameObject);
        }
    }

    void EnemigoMuerto()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        rb2d.bodyType = RigidbodyType2D.Static;
        
        if (!(stateInfo.IsName("Enemy_Die")))
        {
            GeneratorCoins();
        }
        anim.SetTrigger("Died");
        //audioEnemy.clip = clipEnemyDie;
        //audioEnemy.Play();
        Invoke("DestroyEnemy", 1f);
        polcol.enabled = !polcol.enabled; // desactiva la colision contra el player 
    }

    void DestroyEnemy()
    {
        Destroy(this.gameObject);
    }

    void CreateCoin()
    {// Objeto a Instanciar, posicion actual del gameObject, variable necesaria para el instantiate
        System.Random rnd = new System.Random();
        int rInt = rnd.Next(0, 6);
        Debug.Log(rInt);
        Vector3 transformUpdate = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Instantiate(GenerarCoinAleatorio(rInt), transformUpdate, Quaternion.identity);
    }

    public GameObject GenerarCoinAleatorio(int randomInt){
     var  obj = CoinPreFab05;
     switch (randomInt)
        { 
            case 1:
                return CoinPreFab01;
            case 2:
                return CoinPreFab02;
            case 3:
                return CoinPreFab03;
            case 4:
                return CoinPreFab04;
            case 5:
                return CoinPreFab05;
            default:
                break;
        

      };
        return obj;
    }


    public void GeneratorCoins()
    {
        /// Este metodo propio de Unity realiza una invocacion cada
        /// determinado tiempo, el 0f es para la primera vez que lo invoca
        Invoke("CreateCoin", 0f);
    }
}
