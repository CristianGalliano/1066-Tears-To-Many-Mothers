using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardScript : MonoBehaviour
{
    public GameObject[] dropPoints;
    public Vector3 cardPosition;
    public Vector3 originalCardPosition;
    private int clickCount = 0;
    public GameObject panel;
    public Text nameTraitType;
    public Text costZeal;
    public Text mightHealthResources;
    public Text abilities;
    public Text flavour;
    public Text cardNumber;
    public NormanCard card;
    private bool dragging = false;

	// Use this for initialization
	void Start ()
    {
        originalCardPosition = transform.position;
        cardPosition = gameObject.transform.localPosition;
        if (panel != null)
        {
            panel.gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnMouseEnter()
    {
        if (clickCount == 0 && dragging == false)
        {
            cardPosition.y = 300;
            cardPosition.z = 50;
            gameObject.transform.localPosition = cardPosition;
        }
    }

    private void OnMouseExit()
    {
        if (clickCount == 0 && dragging == false)
        {
            transform.position = originalCardPosition;
        }
    }

    //private void OnMouseDown()
    //{
    //    clickCount++;
    //    if (clickCount == 1)
    //    {
    //        cardPosition.y = 500;
    //        cardPosition.z = 100;
    //        gameObject.transform.localPosition = cardPosition;
    //        if (panel != null)
    //        {
    //            panel.gameObject.SetActive(true);
    //            nameTraitType.text = card.name + ", " + card.traits + ", " + card.type;
    //            costZeal.text = "Cost: " + card.cost.ToString() + ", Zeal: " + card.zeal.ToString();
    //            mightHealthResources.text = "Might: " + card.might.ToString() + ", Health: " + card.health.ToString() + ", Resources: " + card.resources.ToString();
    //            abilities.text = card.abilities;
    //            flavour.text = card.flavour;
    //            cardNumber.text = "Card Number: " + card.cardNumber.ToString();
    //        }
    //    }
    //    else
    //    {
    //        if (panel != null)
    //        {
    //            panel.gameObject.SetActive(false);
    //        }
    //        cardPosition.y = 200;
    //        cardPosition.z = 0;
    //        gameObject.transform.localPosition = cardPosition;
    //        clickCount = 0;
    //    }
    //}

    public void OnMouseDrag()
    {
        dragging = true;
        float distance = Input.mousePosition.x + Input.mousePosition.y;
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = objPosition;
    }

    public void OnMouseUp()
    {
        int Count = 0;
        foreach (GameObject point in dropPoints)
        {
            if (transform.position.x < point.transform.position.x + 100 && transform.position.x > point.transform.position.x - 100 && transform.position.z < point.transform.position.z + 100 && transform.position.z > point.transform.position.z - 100)
            {
                transform.position = point.transform.position;
            }
            else
            {
                Count++;
            }
        }
        if (Count == dropPoints.Length)
        {
            transform.position = originalCardPosition;
            dragging = false;
        }
    }

}
