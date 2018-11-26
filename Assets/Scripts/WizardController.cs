using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardController : MonoBehaviour {

    public float radiusVision;
    public float speed;
    public float attackRadius;
    public GameObject PrefabBallAcid;
    public GameObject coinPreFab05;
    public GameObject coinPreFab04;
    public GameObject rubyPreFab;
    public AudioClip clipDie;
    public AudioClip clipHurt;
    public AudioClip clipShoot;



    private GameObject playerFollow;
    private Vector3 initialPosition;
    private bool attacking = false;
    private bool puedeCrearInstancias;
    Animator anim;
    Rigidbody2D r2bd;
    private PolygonCollider2D polcol;
    private int cantLife = 2;
    private AudioSource audioController;

    // Use this for initialization
    void Start () {
        playerFollow = GameObject.FindGameObjectWithTag("Jugador");
        initialPosition = transform.position;
        polcol = GetComponentInChildren<PolygonCollider2D>();
        anim = GetComponent<Animator>();
        r2bd = GetComponent<Rigidbody2D>();
        audioController = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {

        if (r2bd.bodyType != RigidbodyType2D.Static) {
            Vector3 target = initialPosition;
            float distancia = Vector3.Distance(playerFollow.transform.position, transform.position);

            if (distancia < radiusVision)
            {
                target = playerFollow.transform.position;
                anim.SetBool("Flying", true);
                anim.SetBool("Attack", false);
            }

            float fixedSpeed = speed * Time.deltaTime;

            Vector3 dir = (target - transform.position).normalized;

            if (target != initialPosition && distancia < attackRadius)
            {
                if (!attacking)
                {
                    StartCoroutine(CreateBallOFAcid(1f, dir));
                }

                anim.SetBool("Flying", false);
                anim.SetBool("Attack", true);
                cambiarDireccion(dir.x);
            }
            else {
                // mueve el personaje en un determinada dir, esta "dir" depende de cual sea el target.
                //anim.SetBool("Attack", false);
                cambiarDireccion(dir.x);
                r2bd.MovePosition(transform.position + dir * speed * Time.deltaTime);
                puedeCrearInstancias = false;
            }

            float distanciaDeVuelta = Vector3.Distance(target, transform.position);
            if (target == initialPosition && distanciaDeVuelta < 0.05f)
            {
                anim.SetBool("Flying", false);
                // anim.SetBool("Attack", false);
                transform.position = initialPosition;
            }

            Debug.DrawLine(transform.position, target, Color.green);
        }
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radiusVision);
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BallFire")
        {
            EnemigoMuerto();
            //WizardHurt();
        }

    }

    public void WizardHurt()
    {
        anim.SetBool("Hurt", true);
        cantLife -= 1;
        r2bd.bodyType = RigidbodyType2D.Static;
        PlayClip(clipHurt);
        Invoke("WizardHurtStop",0.40f);
    }

    public void WizardHurtStop()
    {
        anim.SetBool("Hurt", false);
        r2bd.bodyType = RigidbodyType2D.Kinematic;
    }

    public void EnemigoMuerto()
    {

        if (cantLife == 0)
        {
            GestionarProcesoDeMuerte();
        }
        else
        {
            WizardHurt();
        }
    }

    public void GestionarProcesoDeMuerte()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        r2bd.bodyType = RigidbodyType2D.Static;

        if (!(stateInfo.IsName("Wizard_Diee")))
        {
            GeneratorCoins();
        }
        anim.SetTrigger("Die");
        PlayClip(clipDie);
        Invoke("DestroyEnemy", 1.35f);
        polcol.enabled = !polcol.enabled; // desactiva la colision contra el player }
    }


    public void CreateInstanceBallAcid(Vector3 direccion)
    {
        if (puedeCrearInstancias)
        {
            Vector3 transformUpdate;
            transformUpdate = new Vector3((transform.position.x) - 1.2f, (transform.position.y) - 0.4f, transform.position.z);
            if (direccion.x > 0.1f)
            {
                transformUpdate = new Vector3((transform.position.x) + 1.2f, (transform.position.y) - 0.4f, transform.position.z);
            }

            GameObject instantiateBall = Instantiate(PrefabBallAcid, transformUpdate, Quaternion.identity);
            PlayClip(clipShoot);
            BallController ballObject = instantiateBall.GetComponent<BallController>();
            ballObject.mov = direccion;
        }
    }

    IEnumerator CreateBallOFAcid(float seconds, Vector3 direccion)
    {
        attacking = true;
        if (PrefabBallAcid != null)
        {
            
            CreateInstanceBallAcid(direccion);
            puedeCrearInstancias = true;
            yield return new WaitForSeconds(seconds);
        }
        attacking = false;
    }

    void DestroyEnemy()
    {
        Destroy(this.gameObject);
    }

    void CreateCoin()
    {// Objeto a Instanciar, posicion actual del gameObject, variable necesaria para el instantiate
        System.Random rnd = new System.Random();
        int rInt = rnd.Next(0, 6);
        Vector3 transformUpdate = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        if(rInt <=2) Instantiate(GenerarCoinAleatorio(rInt), transformUpdate, Quaternion.identity);

    }

    public GameObject GenerarCoinAleatorio(int randomInt)
    {
        var obj = coinPreFab04;
        switch (randomInt)
        {
            case 0:
                return coinPreFab05;
            case 1:
                return coinPreFab04;
            case 2:
                return rubyPreFab;
            default:
                break;


        };
        return obj;
    }
    public void PlayClip(AudioClip clip)
    {
        audioController.clip = clip;
        audioController.Play();
    }


    public void GeneratorCoins()
    {
        /// Este metodo propio de Unity realiza una invocacion cada
        /// determinado tiempo, el 0f es para la primera vez que lo invoca
        Invoke("CreateCoin", 0f);
    }
}
