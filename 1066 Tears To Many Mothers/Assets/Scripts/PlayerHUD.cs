using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public GameController controller;
    public ObjectivesScript normanOBJ, saxonOBJ;

    public Text playerResourceNum, playerZealNum, playerMightNum, objectiveHealthNum, objectiveLetter;

    public Text nW1Z, nW1M, sW1Z, sW1M, nW2Z, nW2M, sW2Z, sW2M, nW3Z, nW3M, sW3Z, sW3M;

    public WedgeScript W1, W2, W3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerHud();
    }

    private void UpdatePlayerHud()
    {
        int Nw1Z = 0, Nw1M = 0, Nw2Z = 0, Nw2M = 0, Nw3Z = 0, Nw3M = 0, Sw1Z = 0, Sw1M = 0, Sw2Z = 0, Sw2M = 0, Sw3Z = 0, Sw3M = 0;
        //if norman turn.
        if (controller.turn == 0)
        {
            int zeal = 0;
            int might = 0;
            int objectiveHealth = 0;

            playerResourceNum.text = controller.normanResources.ToString();
            GameObject[] cards = GameObject.FindGameObjectsWithTag("Norman");
            foreach (GameObject card in cards)
            {
                if (card.GetComponent<CardScript>() && card.GetComponent<CardScript>().placed)
                {
                    zeal += card.GetComponent<CardScript>().normanCard.zeal;
                    might += card.GetComponent<CardScript>().normanCard.might;                    
                }
            }

            objectiveHealth = normanOBJ.objective.health;
            playerZealNum.text = zeal.ToString();
            playerMightNum.text = might.ToString();
            objectiveHealthNum.text = objectiveHealth.ToString();
            objectiveLetter.text = normanOBJ.objective.idChar;
        }

        

        if (controller.turn == 1)
        {
            int zeal = 0;
            int might = 0;
            int objectiveHealth = 0;

            playerResourceNum.text = controller.saxonResources.ToString();
            GameObject[] cards = GameObject.FindGameObjectsWithTag("Saxon");
            foreach (GameObject card in cards)
            {
                if (card.GetComponent<CardScript>() && card.GetComponent<CardScript>().placed)
                {
                    zeal += card.GetComponent<CardScript>().saxonCard.zeal;
                    might += card.GetComponent<CardScript>().saxonCard.might;
                }
            }         
            objectiveHealth = saxonOBJ.objective.health;
            playerZealNum.text = zeal.ToString();
            playerMightNum.text = might.ToString();
            objectiveHealthNum.text = objectiveHealth.ToString();
            objectiveLetter.text = saxonOBJ.objective.idChar;
        }
        //nW1M.text = W1.NormanMightTBV.ToString();
        //nW1Z.text = W1.NormanZealTBV.ToString();
        //nW2M.text = W2.NormanMightTBV.ToString();
        //nW2Z.text = W2.NormanZealTBV.ToString();
        //nW3M.text = W3.NormanMightTBV.ToString();
        //nW3Z.text = W3.NormanZealTBV.ToString();

        //sW1M.text = W1.SaxonMightTBV.ToString();
        //sW1Z.text = W1.SaxonZealTBV.ToString();
        //sW2M.text = W2.SaxonMightTBV.ToString();
        //sW2Z.text = W2.SaxonZealTBV.ToString();
        //sW3M.text = W3.SaxonMightTBV.ToString();
        //sW3Z.text = W3.SaxonZealTBV.ToString();
        GameObject[] Normancards = GameObject.FindGameObjectsWithTag("Norman");
        foreach (GameObject card in Normancards)
        {
            if (card.GetComponent<CardScript>() && card.GetComponent<CardScript>().placed)
            {
                if (card.GetComponent<CardScript>().lane == 0)
                {
                    Nw1M += card.GetComponent<CardScript>().normanCard.might;
                    Nw1Z += card.GetComponent<CardScript>().normanCard.zeal;
                }
                if (card.GetComponent<CardScript>().lane == 1)
                {
                    Nw2M += card.GetComponent<CardScript>().normanCard.might;
                    Nw2Z += card.GetComponent<CardScript>().normanCard.zeal;
                }
                if (card.GetComponent<CardScript>().lane == 2)
                {
                    Nw3M += card.GetComponent<CardScript>().normanCard.might;
                    Nw3Z += card.GetComponent<CardScript>().normanCard.zeal;
                }
            }
        }

        GameObject[] Saxoncards = GameObject.FindGameObjectsWithTag("Saxon");
        foreach (GameObject card in Saxoncards)
        {
            if (card.GetComponent<CardScript>() && card.GetComponent<CardScript>().placed)
            {
                if (card.GetComponent<CardScript>().lane == 0)
                {
                    Sw1M += card.GetComponent<CardScript>().saxonCard.might;
                    Sw1Z += card.GetComponent<CardScript>().saxonCard.zeal;
                }
                if (card.GetComponent<CardScript>().lane == 1)
                {
                    Sw2M += card.GetComponent<CardScript>().saxonCard.might;
                    Sw2Z += card.GetComponent<CardScript>().saxonCard.zeal;
                }
                if (card.GetComponent<CardScript>().lane == 2)
                {
                    Sw3M += card.GetComponent<CardScript>().saxonCard.might;
                    Sw3Z += card.GetComponent<CardScript>().saxonCard.zeal;
                }
            }
        }
        nW1M.text = Nw1M.ToString();
        nW1Z.text = Nw1Z.ToString();
        nW2M.text = Nw2M.ToString();
        nW2Z.text = Nw2Z.ToString();
        nW3M.text = Nw3M.ToString();
        nW3Z.text = Nw3Z.ToString();

        sW1M.text = Sw1M.ToString();
        sW1Z.text = Sw1Z.ToString();
        sW2M.text = Sw2M.ToString();
        sW2Z.text = Sw2Z.ToString();
        sW3M.text = Sw3M.ToString();
        sW3Z.text = Sw3Z.ToString();
    }
}
