using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICardScript : MonoBehaviour
{
    CardFucntionScript functScript;
    GameController Game;

    public GameObject cross;
    private Image image;
    private int cardNum;
    private bool discarded;

    // Start is called before the first frame update
    void Start()
    {
        functScript = GameObject.Find("GameController").GetComponent<CardFucntionScript>();
        Game = GameObject.Find("GameController").GetComponent<GameController>();

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
        if(!discarded && functScript.DiscardCount < functScript.DiscardLimit)
        {
            GameObject hand = GameObject.Find("normanHand");
            GameObject hand2 = GameObject.Find("saxonHand");
            image = gameObject.GetComponentInChildren<Image>();
            cardNum = int.Parse(image.sprite.name);

            foreach (Transform child in hand.transform)
            {
                if (child.gameObject.GetComponent<CardScript>().normanCard.cardNumber == cardNum)
                {
                    functScript.Destroy(child.gameObject.GetComponent<CardScript>().normanCard);
                    discarded = true;
                    functScript.DiscardCount++;
                    Destroy(child.gameObject);
                    //Game.discardList.Add(child.gameObject.GetComponent<CardScript>());
                }
            }

            foreach (Transform child in hand2.transform)
            {
                if (child.gameObject.GetComponent<CardScript>().saxonCard.cardNumber == cardNum)
                {
                    functScript.Destroy(child.gameObject.GetComponent<CardScript>().saxonCard);
                    discarded = true;
                    functScript.DiscardCount++;
                    Destroy(child.gameObject);
                    //Game.discardList.Add(child.gameObject.GetComponent<CardScript>());
                }
            }
        }
    }
}
