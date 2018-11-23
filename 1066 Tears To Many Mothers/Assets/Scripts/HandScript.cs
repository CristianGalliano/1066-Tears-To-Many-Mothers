using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    public Vector3 handPosition;
	// Use this for initialization
	void Start ()
    {
        handPosition = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnMouseEnter()
    {
        handPosition.z -= 200;
        gameObject.transform.position = handPosition;
    }

    private void OnMouseExit()
    {
        handPosition.z += 200;
        gameObject.transform.position = handPosition;
    }
}
