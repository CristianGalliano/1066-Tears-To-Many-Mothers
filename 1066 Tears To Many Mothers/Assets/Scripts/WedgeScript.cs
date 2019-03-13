using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WedgeScript : MonoBehaviour
{
    int wedgeNum;
    int pos;

    int NormanMightTBV , SaxonMightTBV, NormanZealTBV, SaxonZealTBV = 0;
    public int NormanDamage, SaxonDamage;
    bool NormanAtBOH, SaxonAtBOH;

    public ObjectivesScript NormanObj, SaxonObj;

    public GameObject[] cards;
    public List<CardScript> normans = new List<CardScript>();
    public List<CardScript> saxons = new List<CardScript>();

    public TextMeshPro[] normanDamageText;
    public TextMeshPro[] saxonDamageText;


    void Start()
    {
        pos = (int)transform.position.x;

        switch(pos)
        {
            case 300: wedgeNum = 0;
                break;
            case 0: wedgeNum = 1;
                break;
            case -300: wedgeNum = 2;
                break;
        }
    }

   
    void Update()
    {
        CheckObjectiveState();
    }

    public void WedgeBattle()
    {
        Reset();
        CalculateAllCards();
        MightBattle();
        ZealBattle();
        UpdateText();
    }

    void CheckObjectiveState()
    {
        NormanAtBOH = NormanObj.objNum == 6;
        SaxonAtBOH = SaxonObj.objNum == 6;

        if (SaxonAtBOH && NormanAtBOH)
        {
            Debug.Log("Wedges can be attacked by both players.");
        }
    }

    void CalculateAllCards()
    {
        //Gather Norman Cards

        cards = GameObject.FindGameObjectsWithTag("Norman");
        foreach (GameObject card in cards)
        {
            if(card.GetComponent<CardScript>())
            {
                if(card.GetComponent<CardScript>().placed && card.GetComponent<CardScript>().lane == wedgeNum)
                {
                    normans.Add(card.GetComponent<CardScript>());
                }
            }
        }

        //Gather Saxon Cards

        cards = GameObject.FindGameObjectsWithTag("Saxon");
        foreach (GameObject card in cards)
        {
            if (card.GetComponent<CardScript>())
            {
                if (card.GetComponent<CardScript>().placed && card.GetComponent<CardScript>().lane == wedgeNum)
                {
                    saxons.Add(card.GetComponent<CardScript>());
                }
            }
        }


        //Calculate Total TBV's
        foreach(CardScript card in normans)
        {
            if (!card.tired)
            {
                NormanMightTBV += card.normanCard.might;
                NormanZealTBV += card.normanCard.zeal;
            }

        }

        foreach (CardScript card in saxons)
        {
            if (!card.tired)
            {
                SaxonMightTBV += card.saxonCard.might;
                SaxonZealTBV += card.saxonCard.zeal;
            }
        }
    }

    public void MightBattle()
    {
        if(NormanAtBOH && SaxonAtBOH)
        {
            switch (NormanMightTBV.CompareTo(SaxonMightTBV))
            {
                case 0:
                    NormanDamage++;
                    SaxonDamage++;
                    break;
                case -1:
                    SaxonDamage += SaxonMightTBV - NormanMightTBV;
                    break;
                case 1:
                    NormanDamage += NormanMightTBV - SaxonMightTBV;
                    break;
            }
        }
    }

    public void ZealBattle()
    {
        switch (NormanZealTBV.CompareTo(SaxonZealTBV))
        {
            case 0:
                if (SaxonAtBOH)
                {
                    if (NormanZealTBV != 0)
                    {
                        SaxonDamage++;
                    }
                }
                else if (NormanAtBOH)
                {
                    if (NormanZealTBV != 0)
                    {
                        NormanDamage++;
                    }
                }
                else if (NormanAtBOH && SaxonAtBOH)
                {
                    if (NormanZealTBV != 0)
                    {
                        NormanDamage++;
                        SaxonDamage++;
                    }
                }
                break;
            case -1:
                if(SaxonAtBOH)
                    SaxonDamage++;
                break;
            case 1:
                if(NormanAtBOH)
                    NormanDamage++;
                break;
        }
    }

    void Reset()
    {
        NormanMightTBV = 0;
        SaxonMightTBV = 0;
        NormanZealTBV = 0;
        SaxonZealTBV = 0;

        normans = new List<CardScript>();
        saxons = new List<CardScript>();
    }

    void UpdateText()
    {
        foreach(TextMeshPro text in normanDamageText)
        {
            if(text != null)
            {
                if(NormanDamage != 0)
                    text.text = NormanDamage.ToString();
            }

        }

        foreach (TextMeshPro text in saxonDamageText)
        {
            if (text != null)
            {
                if (SaxonDamage != 0)
                    text.text = SaxonDamage.ToString();
            }
        }
    }
}
