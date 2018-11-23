using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    public Vector3 cardPosition;
    private int clickCount = 0;

	// Use this for initialization
	void Start ()
    {
        cardPosition = gameObject.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnMouseEnter()
    {
        if (clickCount == 0)
        {
            cardPosition.y += 200;
            gameObject.transform.localPosition = cardPosition;
        }
    }

    private void OnMouseExit()
    {
        if (clickCount == 0)
        {
            cardPosition.y -= 200;
            gameObject.transform.localPosition = cardPosition;
        }
    }

    private void OnMouseDown()
    {
        clickCount++;
        if (clickCount == 1)
        {
            cardPosition.y = 500;
            cardPosition.z = 100;
            gameObject.transform.localPosition = cardPosition;
        }
        else
        {
            cardPosition.y = 200;
            cardPosition.z = 0;
            gameObject.transform.localPosition = cardPosition;
            clickCount = 0;
        }
    }
}
