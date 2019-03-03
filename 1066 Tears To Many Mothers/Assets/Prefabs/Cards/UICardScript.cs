﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICardScript : MonoBehaviour
{
    CardFucntionScript functScript;

    public GameObject cross;
    private Image image;
    private int cardNum;
    private bool discarded;

    // Start is called before the first frame update
    void Start()
    {
        functScript = GameObject.Find("GameController").GetComponent<CardFucntionScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (discarded)
            cross.SetActive(true);
        else if (!discarded)
            cross.SetActive(false);
    }

    public void DiscardFromHand()
    {
        if(!discarded && functScript.DiscardCount < 2)
        {
            GameObject hand = GameObject.Find("saxonHand");
            image = gameObject.GetComponentInChildren<Image>();
            cardNum = int.Parse(image.sprite.name);

            foreach (Transform child in hand.transform)
            {
                if (child.gameObject.GetComponent<CardScript>().saxonCard.cardNumber == cardNum)
                {
                    Destroy(child.gameObject);
                    discarded = true;
                    functScript.DiscardCount++;
                }
            }
        }
    }
}
