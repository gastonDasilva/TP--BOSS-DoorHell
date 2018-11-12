using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeVidaController : MonoBehaviour {

    public Image health;
    public float hp, maxHp = 100f;

    private Vector2 localScaleOrigin;
    // Use this for initialization
    void Start () {
        hp = maxHp;
        localScaleOrigin = health.transform.localScale;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RecibirDanho(float danho)
    {
        hp = Mathf.Clamp(hp - danho, 0f, maxHp);
        health.transform.localScale = new Vector2(hp / maxHp, 1);
    }

    public void CambiarColor(Color colorH )
    {
        health.color = colorH;
    }

    public void Curar()
    {
        hp = maxHp;
        health.transform.localScale = localScaleOrigin;
    }
}
