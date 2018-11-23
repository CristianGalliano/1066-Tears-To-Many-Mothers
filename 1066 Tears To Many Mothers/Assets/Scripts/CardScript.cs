using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
    public Vector3 cardPosition;
    private int clickCount = 0;
    public GameObject panel;
    public Text nameTraitType;
    public Text costZeal;
    public Text mightHealthResources;
    public Text abilities;
    public Text flavour;
    public Text cardNumber;
    public NormanCard card;

	// Use this for initialization
	void Start ()
    {
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
            if (panel != null)
            {
                panel.gameObject.SetActive(true);
                nameTraitType.text = card.name + ", " + card.traits + ", " + card.type;
                costZeal.text = "Cost: " + card.cost.ToString() + ", Zeal: " + card.zeal.ToString();
                mightHealthResources.text = "Might: " + card.might.ToString() + ", Health: " + card.health.ToString() + ", Resources: " + card.resources.ToString();
                abilities.text = card.abilities;
                flavour.text = card.flavour;
                cardNumber.text = "Card Number: " + card.cardNumber.ToString();
            }
        }
        else
        {
            if (panel != null)
            {
                panel.gameObject.SetActive(false);
            }
            cardPosition.y = 200;
            cardPosition.z = 0;
            gameObject.transform.localPosition = cardPosition;
            clickCount = 0;
        }
    }
}
