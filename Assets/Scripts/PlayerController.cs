using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {
    public bool grounded;
    public float maxSpeed;
    public float speed;
    public float jumpPower = 6.5f;
    public GameObject game;
    public GameObject healhtbar;
    public GameObject Manabar;
    public GameObject ballFire;
    public Camera mainCamera;
    public GameObject shoppigCanvas;
    public AudioClip dieClip;
    public AudioClip clipHurt1;
    public AudioClip clipHurt2;
    public AudioClip clipHurt3;
    public AudioClip clipHurt4;
    public AudioClip clipHurt5;
    public AudioClip clipHit;


    private ParticleSystem particleSys;
    private Rigidbody2D rb2d;
    private Animator anim;
    private bool jump;
    private bool poseeMana = true;
    private bool movement = true;
    private bool sigueEnvenenado = false;
    private bool puedeComprar;
    private bool estaComprando;
    private SpriteRenderer sprt;
    private AudioSource audioController;


    // Use this for initialization
    void Start() {
        rb2d = GetComponent<Rigidbody2D>(); // detecta automaticamente el rigidbody
        anim = GetComponent<Animator>();// detecta automaticamente el animator, para gestionar las animaciones
        sprt = GetComponent<SpriteRenderer>();
        audioController = GetComponent<AudioSource>();
        particleSys = GetComponentInChildren<ParticleSystem>();
        particleSys.Stop();
    }

    // Update is called once per frame
    void Update() {
        if (rb2d.bodyType != RigidbodyType2D.Static)
        {
            anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x)); // valor absolutod de la velocidad del eje x 
            anim.SetBool("Grounded", grounded);
            if (Input.GetKeyDown(KeyCode.UpArrow) && grounded) // para saltar con la flecha Arriba
            {
                jump = true;
            }
            EfectuarAtaqueDeFuegoSiDebe();
        }
    }

    void FixedUpdate()
    {
        if (rb2d.bodyType != RigidbodyType2D.Static)
        {
            CorreccionDeVelocidad();
            Movimientos();
            EfectuarSaltoSiDebe();
            EfectuarAtaqueSiDebe();


            EnvenenarJugadorSiDebe();
            ActivarCanvasCompraSiPuede();
            CurarVidaSiDebe();
            RegenerarManaSiDebe();
        }
    }

    public void CorreccionDeVelocidad()
    {
        Vector3 fixedVelocity = rb2d.velocity;// Para corregir la frision provista por el materials 2D
        fixedVelocity.x *= 0.75f;
        if (grounded)
        {
            rb2d.velocity = fixedVelocity;
        }
    }

    public void CurarVidaSiDebe(){
        if (Input.GetKeyDown(KeyCode.Q))
        {
            game.SendMessage("CurarPlayerSiDebe");
        }

    }

    public void RegenerarManaSiDebe()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            game.SendMessage("RegenerarMana");
        }
    }


    public void ActivarCanvasCompraSiPuede()
    {
        if (Input.GetKeyDown(KeyCode.E) && puedeComprar && shoppigCanvas != null && !estaComprando)
        {
            shoppigCanvas.SetActive(true);
            estaComprando = true;
        }
    }


    public void Movimientos()
    {
        float h = Input.GetAxis("Horizontal"); // para detectar la flechas que se aprientan, -1 para <- y 1 para ->
        if (!movement) h = 0; // gestiona que el personaje no pueda moverse si recibe daño


        rb2d.AddForce(Vector2.right * speed * h);

        float limitedSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
        rb2d.velocity = new Vector2(limitedSpeed, rb2d.velocity.y);

        cambiarDireccion(h);
    }

    public void EfectuarSaltoSiDebe()
    {
        if (jump)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse); // Fisica del SALTO
            jump = false;
        }
    }


    private bool PlayerEnModoAtaque()
    {/* Devuelvo un booleano indicando si el player esta en modo Ataque*/
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        bool ataca = stateInfo.IsName("Player_Attack");
        return ataca;
    }

    public void ActivarMuerteDelJugador()
    {
        rb2d.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("Die");
        Collider2D[] colParent= GetComponents<Collider2D>();
        Collider2D[] colChildren = GetComponentsInChildren<Collider2D>();
        print(colChildren.Length);
        print(colParent.Length);
        SetAllCollidersStatus(false, colParent);
        SetAllCollidersStatus(false, colChildren);
        PlayClip(dieClip);
        Invoke("ResetGame", 2f);
    }

    public void ResetGame()
    {
        game.SendMessage("ResetGame");
    }

    public void SetAllCollidersStatus(bool active, Collider2D[] colliders)
    {

        foreach (Collider2D c in colliders)
        {
            c.enabled = active;
        }
    }

    void EfectuarAtaqueSiDebe()
    {
        bool ataca = PlayerEnModoAtaque();
        if (Input.GetMouseButtonDown(0) && ataca != true && !estaComprando) // Detecta el click del boton derecho y efectua un ataque
        {
            movement = false;
            //ActivarDetectorParaAtaque();
            //UpdateState("Player_Attack");
            anim.SetTrigger("Attacking");
            PlayClip(clipHit);
            Invoke("EnableMovement", 0.46f);
            //Invoke("ActivarDetectorParaAtaque", 0.46f);
        }
    }

    public void EfectuarAtaqueDeFuegoSiDebe()
    {
        bool ataca = PlayerEnModoAtaque();
        if (Input.GetMouseButtonDown(1) && ataca != true && poseeMana && !estaComprando) // Detecta el click del boton derecho y efectua un ataque
        {
            movement = false;
            //ActivarDetectorParaAtaque();
            //UpdateState("Player_Attack");
            anim.SetTrigger("Attacking");
            CreationBallFire();
            Manabar.SendMessage("DisminuirMana", 25f);
            print("tiro");
            Invoke("EnableMovement", 0.46f);
            //Invoke("ActivarDetectorParaAtaque", 0.46f);
        }
    }

    public void NohayMasMana()
    {
        poseeMana = false;
    }

    public void TieneMana()
    {
        poseeMana = true;
    }

    void ActivarDetectorParaAtaque()
    {
        PolygonCollider2D colAttack = gameObject.GetComponentInChildren<PolygonCollider2D>();
        colAttack.enabled = !colAttack.enabled;


    }

    void cambiarDireccion(float h)
    {
        if (h > 0.1f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f); // Gestiona la direccion de la animacion
            if (rb2d.velocity.x < -0.1)
            {
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            }
        }

        if (h < -0.1f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            if (rb2d.velocity.x > 0.1)
            {
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            }
        }
    }

    public void EstoyAtacando(GameObject ememy)
    {
        bool ataca = PlayerEnModoAtaque();
        if (ataca)
        {
            Debug.Log("Player Esta Atacando");
            ememy.transform.parent.SendMessage("EnemigoMuerto");
        }
        else {
            /*EnemyKnockBack(ememy.transform.position.x);*/
            Debug.Log("Player NOOO Esta Atacando"); }

        //game.SendMessage("IncreasePoint", transform.position.y);
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Estatua")
        {
            Debug.Log("YA NO Puedes Comprar");
            puedeComprar = false;
            shoppigCanvas.SetActive(false);
            estaComprando = false;
            //healhtbar.SendMessage("RecibirDanho", 2f);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Wizard")
        {
            Debug.Log("Choco con Un Enemigo");
            this.EstoyAtacando(collision.gameObject);
            //healhtbar.SendMessage("RecibirDanho", 2f);
        }
        if (collision.gameObject.tag == "Coin")
        {
            collision.gameObject.transform.parent.SendMessage("CantidadDePuntosPorMonedas",game);
            collision.gameObject.transform.parent.SendMessage("GestionarRecoleccion");
            Debug.Log("Coin!!!");
        }

        if (collision.gameObject.tag == "BallAcid")
        {
            DestruirAtaqueEnemigoSiDebe(collision.gameObject);
        }

        if (collision.gameObject.tag == "Estatua")
        {
            Debug.Log("Puedes Comprar");
            puedeComprar = true;
            //healhtbar.SendMessage("RecibirDanho", 2f);
        }
        if (collision.gameObject.tag == "pinches")
        {
            EnemyKnockBackPinches(collision.gameObject.transform.position.x);
        }
    }

    public void EnemyKnockBackPinches(float enemyPosx)
    {
        jump = true;
        float side = Mathf.Sign(enemyPosx - transform.position.x);
        rb2d.AddForce(Vector2.left * side * jumpPower, ForceMode2D.Impulse); // Fisica del SALTO para atras 
        movement = false;
        Invoke("EnableMovement", 1f);
        Color colour = new Color(19f, 111f, 60f);
        sprt.color = Color.red;
        RecibirDanho(25f);
    }

    public void DestruirAtaqueEnemigoSiDebe(GameObject enemyAttack)
    { /* Por lo general el enemyAttack suele ser algun poder que el enemigo libera,
        por ahora solo existen las ballsAcid.
        */
        bool ataca = PlayerEnModoAtaque();
        if (ataca)
        {
            enemyAttack.SendMessage("BallDestroy");

        }
    }

    public void  EnemyKnockBack( GameObject enemy)
    {
        jump = true;
        float enemyPosx = enemy.transform.position.x;
        EnemyControler enemyctr = enemy.GetComponent<EnemyControler>();
        float side = Mathf.Sign(enemyPosx - transform.position.x);
        rb2d.AddForce(Vector2.left *side * jumpPower, ForceMode2D.Impulse); // Fisica del SALTO para atras 
        movement = false;
        Invoke("EnableMovement", 1f);
        Color colour = new Color(19f,111f,60f);
        sprt.color = Color.red;


        if (enemyctr.tipoDeEnemigo == 1)
        {
            // healhtbar.SendMessage("RecibirDanho", 4f);
            RecibirDanho(4f);
            EfecuarEnvenamientoPorUnTiempo(6f);
        }
        RecibirDanho(2f);
        //healhtbar.SendMessage("RecibirDanho", 2f);
    }

    public void EfecuarEnvenamientoPorUnTiempo(float tiempo)
    {
        InciarEnvenenamiento();
        Invoke("DejarDeEnvenenar", tiempo);
    }

    public void RecibirDanho(float danho)
    {
        PlayClip(ObtenerClipHurtRandom());
        healhtbar.SendMessage("RecibirDanho", danho);
        ProducirAturdimientoDeCamera();
    }


    public void ProducirAturdimientoDeCamera()
    {
        CameraShake camaraShake = mainCamera.GetComponent<CameraShake>();
        camaraShake.Shake(0.06f, 0.2f);
    }

    public void DejarDeEnvenenar()
    {/*Inicia la gestion de Parar el envenamiento del player*/
        sigueEnvenenado = false;
        healhtbar.SendMessage("CambiarColor", Color.white);
        particleSys.Stop();
    }

    public void InciarEnvenenamiento()
    {/*Inicia la gestion de envenamiento del player*/
        ProducirAturdimientoDeCamera();
        particleSys.Play();
        sigueEnvenenado = true;
        healhtbar.SendMessage("CambiarColor", Color.green);
    }

   public void EnvenenarJugadorSiDebe()
    {/*Si el envenamiento continua se le va sacando vida al player */
        if(sigueEnvenenado) healhtbar.SendMessage("RecibirDanho", 0.050f);
    }

    public void EnableMovement()
    {
        movement = true;
        sprt.color = Color.white;
    }


    void CreationBallFire()
    {// Objeto a Instanciar, posicion actual del gameObject, variable necesaria para el instantiate
        Vector3 transformUpdate = new Vector3(transform.position.x, transform.position.y, transform.position.z);


        GameObject instantiateBall =  Instantiate(ballFire, transformUpdate, Quaternion.identity);

        BallController ballObject = instantiateBall.GetComponent<BallController>();

        ballObject.mov = transform.localScale;

    }

    public void ComprarSiPuede(int idItem)
    {
        if (idItem == 1)
        {
            game.SendMessage("ObtenerPocionDeVidaSiPuede");
            Debug.Log("ComprandoUna Pocion De Vida");
        }
        if (idItem == 2)
        {
            game.SendMessage("ObtenerPocionDeManaSiPuede");
        }

        if (idItem == 3)
        {
            game.SendMessage("ObtenerMonedaACambioDeUnRubySiPuede");
            Debug.Log("Cambiando Un Ruby Por Monedas");
        }
    }

    public void PlayClip(AudioClip clip)
    {
        audioController.clip = clip;
        audioController.Play();
    }

    public AudioClip ObtenerClipHurtRandom()
    {
        System.Random rnd = new System.Random();
        int rInt = rnd.Next(0, 5);
        print(rInt);
        switch (rInt)
        {
            case 0:
                return clipHurt1;
            case 1:
                return clipHurt2;
            case 2:
                return clipHurt3;
            case 3:
                return clipHurt4;
            case 4:
                return clipHurt5;
            default:
                break;
        }

        return clipHurt1;
    }

    void OnBecameInvisible()
    {
        transform.position = new Vector3(-8f, 0f, 0f);
    }
}
