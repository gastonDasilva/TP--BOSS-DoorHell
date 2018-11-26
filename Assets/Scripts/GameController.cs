using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public RawImage mountains;
    public RawImage graves;
    public float parallaxVelocidad = 0.02f;
    public Text pointText;
    public Text pointTextRuby;
    public Text cantTextPocionVida;
    public Text cantTextPocionMana;
    public GameObject healhtbar;
    public GameObject Manabar;
    public GameObject bossbar;
    public AudioClip clipPosion1;
    public AudioClip clipPosion2;
    public AudioClip clipPosion3;
    public AudioClip clipCoin;
    public AudioClip clipRuby;
    public AudioClip clipClickPosion;
    public AudioClip clipClikCoinsXRuby;
    public GameObject horseBoss;

    private int points = 0;
    private int pointsRuby = 0;
    private int cantspocionVida = 0;
    private int cantspocionMana = 0;
    private AudioSource audioController;
    private MusicController musicController;
    private HorseBossController horseController;
    // Use this for initialization
    void Start () {

        audioController = GetComponent<AudioSource>();
        musicController = GetComponentInChildren<MusicController>();
        horseController = horseBoss.GetComponent<HorseBossController>();

    }
	
	// Update is called once per frame
	void Update () {
       

    }
    void FixedUpdate()
    {
        Parallax();
    }


    Transform GetCameraTransform()
    {
        return this.GetComponentInParent<Transform>();
    }

    void Parallax()
    {
        mountains.uvRect = new Rect(this.GetCameraTransform().position.x * 0.025f, 0f, 1f, 1f);
        graves.uvRect = new Rect(this.GetCameraTransform().position.x * 0.04f, 0f, 1f, 1f);
    }

    public void PlayClip(AudioClip clip)
    {
        audioController.clip = clip;
        audioController.Play();
    }

    public AudioClip ObtenerClipPosionRandom()
    {
        System.Random rnd = new System.Random();
        int rInt = rnd.Next(0, 3);
        print(rInt);
        switch (rInt)
        {
            case 0:
                return clipPosion1;
            case 1:
                return clipPosion2;
            case 2:
                return clipPosion3;
            default:
                break;
        }

        return clipPosion1;
    }

    public void IncreasePoint(int i)
    {
        if (i == 6)
        {
            PlayClip(clipRuby);
            pointsRuby += 1;
            pointTextRuby.text = pointsRuby.ToString();
        }
        else {
            PlayClip(clipCoin);
            points += i;
            pointText.text = points.ToString();
        }
        

        //SaveScore(points);
    }

    public void ObtenerPocionDeVidaSiPuede()
    {
        if (points >= 15)
        {
            PlayClip(clipClickPosion);
            cantspocionVida += 1;
            cantTextPocionVida.text = cantspocionVida.ToString();
            Debug.Log("Puntos:" + points);
            points = points - 15;
            pointText.text = points.ToString();
            Debug.Log("PuntosDespues:" + points);

        }
        else { Debug.Log("No tienes suficiente Dinero Para comprar ese Item"); }
    }

    public void ObtenerMonedaACambioDeUnRubySiPuede()
    {
        if (pointsRuby >= 1)
        {
            PlayClip(clipClikCoinsXRuby);
            pointsRuby -= 1;
            pointTextRuby.text = pointsRuby.ToString();
            points += 20;
            pointText.text = points.ToString();
        }
        else
        {
            Debug.Log("No tienes suficientes Rubys Para comprar ese Item");
        }
    }

    public void ObtenerPocionDeManaSiPuede()
    {
        if (points >= 10)
        {
            PlayClip(clipClickPosion);
            cantspocionMana += 1;
            cantTextPocionMana.text = cantspocionMana.ToString();
            points = points - 10;
            pointText.text = points.ToString();

        }
        else { Debug.Log("No tienes suficiente Dinero Para comprar ese Item"); 
        }
    }

    public void CurarPlayerSiDebe()
    {
        if (cantspocionVida >= 1)
        {
            PlayClip(ObtenerClipPosionRandom());
            healhtbar.SendMessage("Curar");
            cantspocionVida -= 1;
            cantTextPocionVida.text = cantspocionVida.ToString();

        }
        else
        {
            Debug.Log("No tenes Pociones de Vida, Pedazo de tontin");
        }
    }

    public void RegenerarMana()
    {
        if (cantspocionMana >=1)
        {
            PlayClip(ObtenerClipPosionRandom());
            Manabar.SendMessage("Curar");
            cantspocionMana -= 1;
            cantTextPocionMana.text = cantspocionMana.ToString();
        }
        else
        {
            Debug.Log("No tenes Pociones de Mana, Wacho");
        }
    }

    public void EnabledBossBar()
    {
        bossbar.SetActive(true);
        musicController.StartMusicBoss();
        horseController.StartBoss();

    }

    public void ResetGame()
    {
        SceneManager.LoadScene("Principal");
    }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    