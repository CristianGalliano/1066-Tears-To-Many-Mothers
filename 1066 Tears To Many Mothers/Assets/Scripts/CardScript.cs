﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardScript : MonoBehaviour
{
    Vector3[] dropPoints = new Vector3[] { new Vector3(300,0,326), new Vector3(0,0,326), new Vector3(-300,0,326), new Vector3(300,0,663), new Vector3(0,0,663), new Vector3(-300,0,663), new Vector3(300,0,1000), new Vector3(0,0,1000), new Vector3(-300,0,1000)};
    public Vector3 cardPosition;
    public Vector3 originalCardPosition;
    public Vector3 lastPosition;
    private int clickCount = 0;
    public GameObject panel;
    public Image sprite;
    public Text nameTraitType;
    public Text costZeal;
    public Text mightHealthResources;
    public Text abilities;
    public Text flavour;
    public Text cardNumber;
    public NormanCard card;
    private bool dragging = false;
    bool placed = false;

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

    private void OnMouseDown()
    {
        if (placed)
        {
            clickCount++;
            if (clickCount == 1)
            {
                if (panel != null)
                {
                    panel.gameObject.SetActive(true);
                    sprite.sprite = card.image;
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
                clickCount = 0;
            }
        }
    }

    public void OnMouseDrag()
    {
        if (!placed)
        {
            dragging = true;
            float distance = 1100 + Input.mousePosition.y;
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = objPosition;
        }
    }

    public void OnMouseUp()
    {
        int Count = 0;
        foreach (Vector3 point in dropPoints)
        {
            if (transform.position.x < point.x + 100 && transform.position.x > point.x - 100 && transform.position.z < point.z + 168 && transform.position.z > point.z - 168)
            {
                if (!placed)
                {
                    Vector3 dropPosition = new Vector3(point.x, point.y + 2, point.z);
                    transform.position = dropPosition;
                    lastPosition = transform.position;
                    Vector3 Rotation = new Vector3(-50, 0, 0);
                    transform.Rotate(Rotation);
                    placed = true;                    
                }
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
