using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeManaController : MonoBehaviour {

    public Image mana;
    public float hp, maxHp = 100f;

    public GameObject player;
    // Use this for initialization
    void Start () {
        hp = maxHp;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DisminuirMana(float danho)
    {
        hp = Mathf.Clamp(hp - danho, 0f, maxHp);
        mana.transform.localScale = new Vector2(hp / maxHp, 1);
        if (hp <= 0) player.SendMessage("NohayMasMana");
    }
}
