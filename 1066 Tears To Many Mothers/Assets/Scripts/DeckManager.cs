using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public Dictionary<int, NormanCard> NormanDeck = new Dictionary<int, NormanCard>();
    string[] cardDataRows;
    string[] cardDataText;

    int idNum;
    int rnd;
    public List<int> usedCards;

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
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public NormanCard DrawRandomNormanCard()
    {
        rnd = Random.Range(0, NormanDeck.Count);
        
        foreach(int num in usedCards)
        {
            if(rnd == num)
            {
                rnd = Random.Range(0, NormanDeck.Count);
            }
            else
            {
                usedCards.Add(rnd);
            }
        }

        return NormanDeck[rnd];
    }

}
