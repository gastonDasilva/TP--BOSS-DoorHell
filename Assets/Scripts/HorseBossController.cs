using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseBossController : MonoBehaviour {

    public float maxSpeed;
    public float speed;
    public GameObject healhtbarBoss;
    public GameObject gestionadorDelFinal;

    private Color flashColour = new Color(1f, 0f, 0f, 255f); //0.1f
    private Color flashColourEnd = new Color(1f, 0f, 0f, 0.2f); //0.1f
    private float flashSpeed = 40f; //25
    private float countFlash = 0f;
    private SpriteRenderer damageImage;
    private bool damaged = false;
    private bool enProcesoDeFlash;
    private bool precipicio;
    private bool deadFlash;
    private Color origionalColor;
    private Rigidbody2D rb2d;
    private Animator anim;

    // Use this for initialization
    void Start () {
        damageImage = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        origionalColor = damageImage.color;
        transform.localScale = new Vector3(-1f, 1f, 1f);
        anim.SetTrigger("Die");
        rb2d.bodyType = RigidbodyType2D.Static;


    }
	
	// Update is called once per frame
	void Update () {

        RealizarProcesoDeDanhoSiDebe();
        GestionDeMovimiento();
        ActivarFlashOfDead();


    }

    public void StartBoss()
    {
        rb2d.bodyType = RigidbodyType2D.Dynamic;
        anim.SetTrigger("Start");
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
            CambiarDireccionDependiendo(transform.localScale.x);
            precipicio = false;
        }
    }

    void CambiarDireccionDependiendo(float s)// Dependiendo de la speed
    {
        if (s > 0.1f && rb2d.bodyType != RigidbodyType2D.Static && precipicio == true)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f); // Gestiona la direccion de la animacion
        }

        if (s < -0.1f && rb2d.bodyType != RigidbodyType2D.Static && precipicio == true)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void RealizarProcesoDeDanhoSiDebe()
    {
        if (damaged)
        {
            // ... set the colour of the damageImage to the flash colour.
            damageImage.color = flashColour;
            enProcesoDeFlash = true;
            countFlash = 3f;
        }
        // Otherwise...
        if (enProcesoDeFlash && !deadFlash)
        {
            // ... transition the colour back to clear.       /*Color.clear*/
            damageImage.color = Color.Lerp(damageImage.color, flashColourEnd, flashSpeed * Time.deltaTime);
            if (damageImage.color == flashColourEnd) {
                damageImage.color = flashColour;
                countFlash -= 1;
                if (countFlash == 0)
                {
                    enProcesoDeFlash = false;
                    ResetColor();
                }
            }
        }
        // Reset the damaged flag.
        damaged = false;
    }


    public void takeDamage()
    {
        damaged = true;
        healhtbarBoss.SendMessage("RecibirDanho",17f); //17f
    }

    void ResetColor()
    {
        damageImage.color = origionalColor;
    }

    public void ActivarMuerteDelJugador()
    {
        anim.SetTrigger("Die");
        rb2d.bodyType = RigidbodyType2D.Static;
        PolygonCollider2D polcol = GetComponentInChildren<PolygonCollider2D>();
        polcol.enabled = false;
        deadFlash = true;       
        print("El Horse Se murio");
        Invoke("BarEnabled",5f);
        Invoke("inicioDeFinal", 5f);
        
    }

    public void inicioDeFinal()
    {
        gestionadorDelFinal.SendMessage("AbrirCofre");

    }

    public void BarEnabled()
    {
        healhtbarBoss.SetActive(false);
    }

    public void ActivarFlashOfDead()
    {
        if (deadFlash)
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, 2f * Time.deltaTime);
        }

        if(damageImage.color == Color.clear)
        {
            deadFlash = false;
            Destroy(gameObject);
            
        }
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "BallFire")
        {
            takeDamage();
        }

        if (collision.gameObject.tag == "Precipicio")
        {
            precipicio = true;
        }
    }
}
