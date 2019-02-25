using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawScript : MonoBehaviour
{
    public int x;
    public GameObject card;
    public GameObject hand;
    private Vector3 location = new Vector3(0, 200, 1300);
    private Vector3 scale;
    private Vector3 oPosition;
    private Vector3 mPosition;
    Quaternion qrotation;
    private int cardsRemaining = 76;
    public float decreaseHeight;
    private HandScript handScript;

    // Use this for initialization
    void Start ()
    {
        Vector3 rotation = new Vector3(-40, x, 0);
        qrotation = Quaternion.Euler(rotation);
        decreaseHeight = (gameObject.transform.localScale.y) / 76 ;
        oPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        handScript = hand.GetComponent<HandScript>();
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    public void drawFunc(int i)
    {
        if (gameObject.transform.localScale.z >= 0)
        {
            for (int j = 0; j < i; j++)
            {
                scale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z - decreaseHeight);
                mPosition = new Vector3(oPosition.x, (scale.z/33) / 2, oPosition.z);
                GameObject Card = Instantiate(card, location, qrotation);
                Card.transform.parent = hand.transform;
                transform.localPosition = mPosition;
                transform.localScale = scale;
                cardsRemaining--;                
                //Debug.Log("number of cards in deck : " + cardsRemaining);
                handScript.adjustCardPos();
            }
        }
        else
        {
            Debug.Log("out of cards");
        }
    }

    public void drawLeader()
    {
        
    }
}
