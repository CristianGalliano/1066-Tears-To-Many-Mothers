using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    public Vector3 handPosition;

    private int widthOfHand = 700;

    // Use this for initialization
    void Start ()
    {
        handPosition = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
    
	}

    public void adjustCardPos()
    {
        Vector3 cardPos = new Vector3();
        int i = 1;
        foreach (Transform child in transform)
        {
            float q = ((widthOfHand / transform.childCount) * i) - widthOfHand / 2;
            if (gameObject.tag == "Norman")
            {               
                cardPos = new Vector3(q, transform.position.y, transform.position.z - (i * 5));
            }
            else if (gameObject.tag == "Saxon")
            {
                cardPos = new Vector3(-q, transform.position.y, transform.position.z + (i * 5));
            }
            child.transform.position = cardPos;
            i++;
        }
    }
}
