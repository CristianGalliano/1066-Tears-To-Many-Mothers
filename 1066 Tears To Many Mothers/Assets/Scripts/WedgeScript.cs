using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WedgeScript : MonoBehaviour
{
    int wedgeNum;
    int pos;

    int NormanMightTBV, SaxonMightTBV, NormanZealTBV, SaxonZealTBV;
    int NormanDamage, SaxonDamage;
    bool NormanAtBOH, SaxonAtBOH;

    public ObjectivesScript NormanObj, SaxonObj;
    
    void Start()
    {
        pos = (int)transform.position.x;

        switch(pos)
        {
            case 300: wedgeNum = 1;
                break;
            case 0: wedgeNum = 2;
                break;
            case -300: wedgeNum = 3;
                break;
        }
    }

   
    void Update()
    {
        CheckObjectiveState();
    }

    void WedgeBattle()
    {
        CalculateAllCards();
        MightBattle();
        ZealBattle();
        Reset();
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
        GameObject[] cards = GameObject.FindGameObjectsWithTag("Norman");
        List<CardScript> normans = new List<CardScript>();

        foreach (GameObject card in cards)
        {
            if(card.GetComponent<CardScript>().lane == wedgeNum)
                normans.Add(card.GetComponent<CardScript>());
        }

        //Gather Saxon Cards
        GameObject[] cards2 = GameObject.FindGameObjectsWithTag("Saxon");
        List<CardScript> saxons = new List<CardScript>();

        foreach (GameObject card in cards)
        {
            if (card.GetComponent<CardScript>().lane == wedgeNum)
                saxons.Add(card.GetComponent<CardScript>());
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
                if(NormanZealTBV != 0)
                {
                    NormanDamage++;
                    SaxonDamage++;
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
    }
}
