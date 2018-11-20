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

    private int points = 0;
    private int pointsRuby = 0;
    private int cantspocionVida = 0;
    private int cantspocionMana = 0;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       

    }
    void FixedUpdate()
    {
        Parallax();
    }

    /*void Parallax()
    {
        float finalSpeed = parallaxVelocidad * Time.deltaTime;
        mountains.uvRect = new Rect(mountains.uvRect.x + finalSpeed, 0f, 1f, 1f);
        float finalSpeedGraves = (parallaxVelocidad +0.05f)* Time.deltaTime;
        graves.uvRect = new Rect(graves.uvRect.x + finalSpeedGraves, 0f, 1f, 1f);

    }*/

    Transform GetCameraTransform()
    {
        return this.GetComponentInParent<Transform>();
    }

    void Parallax()
    {
        mountains.uvRect = new Rect(this.GetCameraTransform().position.x * 0.025f, 0f, 1f, 1f);
        graves.uvRect = new Rect(this.GetCameraTransform().position.x * 0.04f, 0f, 1f, 1f);
    }


    public void IncreasePoint(int i)
    {
        if (i == 6)
        {
            pointsRuby += 1;
            pointTextRuby.text = pointsRuby.ToString();
        }
        else {
            points += i;
            pointText.text = points.ToString();
        }
        

        //SaveScore(points);
    }

    public void ObtenerPocionDeVidaSiPuede()
    {
        if (points >= 15)
        {
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
            Manabar.SendMessage("Curar");
            cantspocionMana -= 1;
            cantTextPocionMana.text = cantspocionMana.ToString();
        }
        else
        {
            Debug.Log("No tenes Pociones de Mana, Wacho");
        }
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("Principal");
    }
}
