using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawScript : MonoBehaviour
{
    public GameObject card;
    public GameObject hand;
    private Vector3 location = new Vector3(0, 200, 1300);
    private Vector3 scale;
    private Vector3 oPosition;
    private Vector3 mPosition;
    Quaternion qrotation = Quaternion.Euler(-40, 0, 0);
    private int cardsRemaining = 76;
    float decreaseHeight;
    private HandScript handScript;

    // Use this for initialization
    void Start ()
    {
        decreaseHeight = (gameObject.transform.localScale.y) / 76 ;
        oPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        handScript = hand.GetComponent<HandScript>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnMouseDown()
    {
        if (cardsRemaining != 0)
        {
            scale = new Vector3(transform.localScale.x, transform.localScale.y - decreaseHeight, transform.localScale.z);
            mPosition = new Vector3(oPosition.x, scale.y / 2, oPosition.z);
            GameObject Card = Instantiate(card, location, qrotation);
            Card.transform.parent = hand.transform;
            transform.localPosition = mPosition;
            transform.localScale = scale;
            cardsRemaining--;
            Debug.Log("number of cards in deck : " + cardsRemaining);
            handScript.adjustCardPos();
        }
    }


}
