using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratoFireController : MonoBehaviour {

    public GameObject PrefabBallFire;

    private bool attacking;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (!attacking) StartCoroutine(CreateBallOFAcid(1.5f));
  
    }

    IEnumerator CreateBallOFAcid(float seconds)
    {
        attacking = true;
        if (PrefabBallFire != null)
        {
            CreateInstanceBallFire();
            yield return new WaitForSeconds(seconds);
        }
        attacking = false;
    }

    public void CreateInstanceBallFire ()
    {
        Vector3 transformUpdate;
        transformUpdate = new Vector3((transform.position.x), (transform.position.y), transform.position.z);
        Quaternion TransformRotation = PrefabBallFire.transform.rotation;
        GameObject instantiateBall = Instantiate(PrefabBallFire, transformUpdate, TransformRotation);
        BallController ballObject = instantiateBall.GetComponent<BallController>();
    }
}
