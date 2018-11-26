using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    private GameObject[] cardsInHand;

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

    //private void OnMouseEnter()
    //{
    //    handPosition.z -= 200;
    //    gameObject.transform.position = handPosition;
    //}

    //private void OnMouseExit()
    //{
    //    handPosition.z += 200;
    //    gameObject.transform.position = handPosition;
    //}

    public void adjustCardPos()
    {
        int i = 1;
        foreach (Transform child in transform)
        {
            float q = ((widthOfHand / transform.childCount) * i) - widthOfHand/2;
            Vector3 cardPos = new Vector3(q, transform.position.y, transform.position.z - (i*5));
            child.transform.position = cardPos;
            i++;
        }
    }
}
