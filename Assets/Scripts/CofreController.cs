using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CofreController : MonoBehaviour {

    public GameObject ItemToDrop;

    private Animator anim;
    private ParticleSystem ps;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        ps = GetComponentInChildren<ParticleSystem>();
        ps.Pause();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartOpenChest()
    {
        anim.SetTrigger("Start");
        ps.Play();
        Invoke("CreateCoin", 0.5f);
        Invoke("CreateCoin", 0.6f);
        Invoke("CreateCoin", 0.7f);
        Invoke("CreateCoin", 0.6f);
        Invoke("CreateCoin", 0.5f);
        Invoke("CreateCoin", 0.7f);
        Invoke("CreateCoin", 0.8f);
    }

    void CreateCoin()
    {// Objeto a Instanciar, posicion actual del gameObject, variable necesaria para el instantiate
        Vector3 transformUpdate = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        GameObject coin = Instantiate(ItemToDrop, transformUpdate, Quaternion.identity);
        CoinController coinC = coin.GetComponent<CoinController>();
        coinC.SendMessage("ImpulseCoin");
    }
}
