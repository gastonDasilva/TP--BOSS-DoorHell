using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoDeColumna : MonoBehaviour {

    public Transform target;
    public float speed = 0.5f;

    private Vector3 start;
    private Vector3 end;
    private bool inicioDeMovimiento;
    // Use this for initialization
    void Start () {
        if (target != null)
        {
            start = transform.position;
            end = target.transform.position;
        }

    }
	
	// Update is called once per frame
	void Update () {
        if (target != null && inicioDeMovimiento)
        {
            float fixedSpeedDelta = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, fixedSpeedDelta);
        }
    }

    public void StartMovimiento()
    {
        inicioDeMovimiento = true;
    }
}
