using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullsController : MonoBehaviour {


    private bool start; 
    private SpriteRenderer imageRenderer;
    private Color originalColour;
    private Color flashColourEnd = Color.green;
    private float flashSpeed = 2f;
    // Use this for initialization
    void Start () {
        imageRenderer = GetComponent<SpriteRenderer>();
        originalColour = imageRenderer.color;

    }
	
	// Update is called once per frame
	void Update () {
        StartSkull();

    }

    public void StartSkull()
    {

        if (start)
        {
            imageRenderer.color = Color.Lerp(imageRenderer.color, flashColourEnd, flashSpeed * Time.deltaTime);
            if (imageRenderer.color == Color.green) flashColourEnd = originalColour;
            if (imageRenderer.color == originalColour) flashColourEnd = Color.green;
        }
    }

    public void EnableStart()
    {
        start = true;
    }

}
