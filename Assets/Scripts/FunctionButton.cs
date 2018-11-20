using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionButton : MonoBehaviour {

    public GameObject player;



    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ComprarPocionDeVidaSiPuede()
    {
        player.SendMessage("ComprarSiPuede",1);
    }

    public void ComprarPocionDeManaSiPuede()
    {
        player.SendMessage("ComprarSiPuede", 2);
    }

    public void ComprarMonedasXRubySiPuede()
    {
        player.SendMessage("ComprarSiPuede", 3);
    }
}
