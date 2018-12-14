using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public Dictionary<int, NormanCard> NormanDeck = new Dictionary<int, NormanCard>();
    public Dictionary<int, NormanCard> SaxonDeck = new Dictionary<int, NormanCard>();
    string[] cardDataRows;
    string[] cardDataText;

    int idNum;
    int rnd;
    int rnd2;
    public List<int> usedNormanCards;
    public List<int> usedSaxonCards;
    public List<string> testList;

    // Use this for initialization
    void Start()
    {
        TextAsset NormanCardData = Resources.Load<TextAsset>("NormanCardData");
        cardDataRows = NormanCardData.text.Split('\n');

        for (int i = 1; i < cardDataRows.Length - 1; i++)
        {
            cardDataText = cardDataRows[i].Split(',');
            idNum = int.Parse(cardDataText[0]);
            NormanDeck.Add(idNum, new NormanCard());
            NormanDeck[idNum].cardNumber = int.Parse(cardDataText[0]);
            NormanDeck[idNum].name = cardDataText[1];
            NormanDeck[idNum].type = cardDataText[2];
            NormanDeck[idNum].might = int.Parse(cardDataText[3]);
            NormanDeck[idNum].zeal = int.Parse(cardDataText[4]);
            NormanDeck[idNum].health = int.Parse(cardDataText[5]);
            NormanDeck[idNum].cost = int.Parse(cardDataText[6]);
            NormanDeck[idNum].abilities = cardDataText[7];
        }

        for(int i = 0; i < NormanDeck.Count; i++)
        {
            testList.Add(NormanDeck[i].name);
            usedNormanCards.Add(i);
        }

        TextAsset SaxonCardData = Resources.Load<TextAsset>("SaxonCardData");
        cardDataRows = SaxonCardData.text.Split('\n');

        for (int i = 1; i < cardDataRows.Length - 1; i++)
        {
            cardDataText = cardDataRows[i].Split(',');
            idNum = int.Parse(cardDataText[0]);
            SaxonDeck.Add(idNum, new NormanCard());
            SaxonDeck[idNum].cardNumber = int.Parse(cardDataText[0]);
            SaxonDeck[idNum].name = cardDataText[1];
            SaxonDeck[idNum].type = cardDataText[2];
            SaxonDeck[idNum].might = int.Parse(cardDataText[3]);
            SaxonDeck[idNum].zeal = int.Parse(cardDataText[4]);
            SaxonDeck[idNum].health = int.Parse(cardDataText[5]);
            SaxonDeck[idNum].cost = int.Parse(cardDataText[6]);
            SaxonDeck[idNum].abilities = cardDataText[7];
        }


        for (int i = 0; i < SaxonDeck.Count; i++)
        {
            testList.Add(NormanDeck[i].name);
            usedSaxonCards.Add(i);
        }

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public NormanCard DrawRandomNormanCard()
    {
        rnd = Random.Range(0, NormanDeck.Count);

        if(usedNormanCards[rnd] != 999)
        {
            usedNormanCards[rnd] = 999;
        }
        else
        {
            DrawRandomNormanCard();
        }

        return NormanDeck[rnd];
    }

    public NormanCard DrawNormanCard(int index)
    {
        return NormanDeck[index];
    }

    public NormanCard DrawRandomSaxonCard()
    {
        rnd2 = Random.Range(0, SaxonDeck.Count);

        if (usedSaxonCards[rnd2] != 999)
        {
            usedSaxonCards[rnd2] = 999;
        }
        else
        {
            DrawRandomSaxonCard();
        }

        return SaxonDeck[rnd2];
    }

    public NormanCard DrawSaxonCard(int index)
    {
        return SaxonDeck[index];
    }

}
