using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public RawImage mountains;
    public RawImage graves;
    public float parallaxVelocidad = 0.02f;
    public Text pointText;

    private int points = 0;
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
        points += i;
        pointText.text = points.ToString();
        //SaveScore(points);
    }
}
