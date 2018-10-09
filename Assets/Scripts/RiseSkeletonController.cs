using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiseSkeletonController : MonoBehaviour {
    private Rigidbody2D rb2d;
    private Animator anim;
    // Use this for initialization
    void Start () {
        rb2d = GetComponentInParent<Rigidbody2D>(); //detecta automaticamente el rigidbody
        anim = GetComponentInParent<Animator>();// detecta automaticamente el animator, para gestionar las animaciones
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnTriggerEnter2D(Collider2D collision)
    {
        /*if(collision.gameObject.tag == "Jugador")
        {
            Debug.Log("Skeleton tiene que SURGIR");
        }*/
    }

     void OnBecameVisible()
    {
        Debug.Log("Skeleton tiene que SURGIR");
    }
}
