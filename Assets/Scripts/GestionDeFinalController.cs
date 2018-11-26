using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionDeFinalController : MonoBehaviour {


    public GameObject columnaMovilConCofre;
    public GameObject cofre;
    public GameObject skull1;
    public GameObject skull2;
    public GameObject paredesInvisibles;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AbrirCofre()
    {
        columnaMovilConCofre.SendMessage("StartMovimiento");
        Invoke("StartOpenChest", 3.4f);
        skull1.SendMessage("EnableStart");
        skull2.SendMessage("EnableStart");
        EnabledColliders(paredesInvisibles.GetComponentsInChildren<EdgeCollider2D>(), false);
    }

    public void StartOpenChest()
    {
        cofre.SendMessage("StartOpenChest");
    }

    public void EnabledColliders(Collider2D[] colliders, bool b)
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = b;
        }
    }
}
