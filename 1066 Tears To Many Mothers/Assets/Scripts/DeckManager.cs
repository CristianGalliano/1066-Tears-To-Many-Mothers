using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public Dictionary<int, NormanCard> NormanDeck = new Dictionary<int, NormanCard>();
    public Dictionary<int, NormanCard> SaxonDeck = new Dictionary<int, NormanCard>();

    public Dictionary<int, NormanObjectiveCard> NormanObjectives = new Dictionary<int, NormanObjectiveCard>();
    public Dictionary<int, NormanObjectiveCard> SaxonObjectives = new Dictionary<int, NormanObjectiveCard>();

    public string[] cardDataRows;
    public string[] cardDataText;
    int rnd, idNum, temp;
    string idChar;

    public List<int> usedNormanCards;
    public List<int> usedSaxonCards;
    public List<int> testList;

    public bool NormanLeaderDrawn, SaxonLeaderDrawn = false;

    // Use this for initialization
    void Start()
    {
        //Initialising Norman Deck
        TextAsset NormanCardData = Resources.Load<TextAsset>("NormanCardData");
        cardDataRows = NormanCardData.text.Split('\n');

        for (int i = 1; i < cardDataRows.Length; i++)
        {
            cardDataText = cardDataRows[i].Split(',');
            idNum = int.Parse(cardDataText[0]);
            NormanDeck.Add(idNum, ScriptableObject.CreateInstance<NormanCard>());

            NormanDeck[idNum].cardNumber = idNum;
            NormanDeck[idNum].name = cardDataText[1];
            NormanDeck[idNum].title = cardDataText[2];
            NormanDeck[idNum].type = cardDataText[3];

            NormanDeck[idNum].cost = int.Parse(cardDataText[4]);
            NormanDeck[idNum].zeal = int.Parse(cardDataText[5]);
            NormanDeck[idNum].might = int.Parse(cardDataText[6]);
            NormanDeck[idNum].health = int.Parse(cardDataText[7]);
            NormanDeck[idNum].resources = int.Parse(cardDataText[8]);

            NormanDeck[idNum].action = cardDataText[9];
            NormanDeck[idNum].constant = cardDataText[10];
            NormanDeck[idNum].response = cardDataText[11];
            NormanDeck[idNum].onPlay = cardDataText[12];

            NormanDeck[idNum].quote = cardDataText[13];
            NormanDeck[idNum].solo = cardDataText[14];
        }



        for (int i = 2; i < NormanDeck.Count; i++)
        {
            testList.Add(NormanDeck[i].cardNumber);
            usedNormanCards.Add(i);
        }



        //Initialising Saxon Deck
        TextAsset SaxonCardData = Resources.Load<TextAsset>("SaxonCardData");
        cardDataRows = SaxonCardData.text.Split('\n');

        for (int i = 1; i < cardDataRows.Length; i++)
        {
            cardDataText = cardDataRows[i].Split(',');
            idNum = int.Parse(cardDataText[0]);
            SaxonDeck.Add(idNum, ScriptableObject.CreateInstance<NormanCard>());


            SaxonDeck[idNum].cardNumber = idNum;
            SaxonDeck[idNum].name = cardDataText[1];
            SaxonDeck[idNum].type = cardDataText[2];
            SaxonDeck[idNum].title = cardDataText[3];

            SaxonDeck[idNum].cost = int.Parse(cardDataText[4]);
            SaxonDeck[idNum].zeal = int.Parse(cardDataText[5]);
            SaxonDeck[idNum].might = int.Parse(cardDataText[6]);
            SaxonDeck[idNum].health = int.Parse(cardDataText[7]);
            SaxonDeck[idNum].resources = int.Parse(cardDataText[8]);

            SaxonDeck[idNum].action = cardDataText[9];
            SaxonDeck[idNum].constant = cardDataText[10];
            SaxonDeck[idNum].response = cardDataText[11];
            SaxonDeck[idNum].onPlay = cardDataText[12];

            SaxonDeck[idNum].quote = cardDataText[13];
            SaxonDeck[idNum].solo = cardDataText[14];
        }

        for (int i = 86; i < SaxonDeck.Count + 85; i++)
        {
            testList.Add(SaxonDeck[i].cardNumber);
            usedSaxonCards.Add(i);
        }


        //Initialising Norman Objectives
        TextAsset NormanObjData = Resources.Load<TextAsset>("NormanObjData");
        cardDataRows = NormanObjData.text.Split('\n');

        for (int i = 1; i < cardDataRows.Length; i++)
        {
            cardDataText = cardDataRows[i].Split(',');
            idChar = cardDataText[1];
            NormanObjectives.Add(i, ScriptableObject.CreateInstance<NormanObjectiveCard>());

            NormanObjectives[i].cardNumber = int.Parse(cardDataText[0]);
            NormanObjectives[i].idChar = idChar;
            NormanObjectives[i].name = cardDataText[2];
            NormanObjectives[i].title = cardDataText[3];
            NormanObjectives[i].type = cardDataText[4];

            NormanObjectives[i].zeal = int.Parse(cardDataText[5]);
            NormanObjectives[i].might = int.Parse(cardDataText[6]);
            NormanObjectives[i].health = int.Parse(cardDataText[7]);

            NormanObjectives[i].ability = cardDataText[8];
            NormanObjectives[i].quote = cardDataText[9];
            NormanObjectives[i].solo = cardDataText[10];

            NormanObjectives[i].startHealth = NormanObjectives[i].health;
            NormanObjectives[i].startZeal = NormanObjectives[i].zeal;
            NormanObjectives[i].startMight = NormanObjectives[i].might;

        }

        //Initialising Saxon Objectives
        TextAsset SaxonObjData = Resources.Load<TextAsset>("SaxonObjData");
        cardDataRows = SaxonObjData.text.Split('\n');

        for (int i = 1; i < cardDataRows.Length; i++)
        {
            cardDataText = cardDataRows[i].Split(',');
            idChar = cardDataText[1];
            SaxonObjectives.Add(i, ScriptableObject.CreateInstance<NormanObjectiveCard>());

            SaxonObjectives[i].cardNumber = int.Parse(cardDataText[0]);
            SaxonObjectives[i].idChar = idChar;
            SaxonObjectives[i].name = cardDataText[2];
            SaxonObjectives[i].title = cardDataText[3];
            SaxonObjectives[i].type = cardDataText[4];

            SaxonObjectives[i].zeal = int.Parse(cardDataText[5]);
            SaxonObjectives[i].might = int.Parse(cardDataText[6]);
            SaxonObjectives[i].health = int.Parse(cardDataText[7]);

            SaxonObjectives[i].ability = cardDataText[8];
            SaxonObjectives[i].quote = cardDataText[9];
            SaxonObjectives[i].solo = cardDataText[10];

            SaxonObjectives[i].startHealth = SaxonObjectives[i].health;
            SaxonObjectives[i].startZeal = SaxonObjectives[i].zeal;
            SaxonObjectives[i].startMight = SaxonObjectives[i].might;

        }
    }
	
	// Update is called once per frame
	void Update ()
    {

	}

    public NormanCard DrawRandomNormanCard()
    {
        rnd = Random.Range(0, usedNormanCards.Count);
        temp = usedNormanCards[rnd];
        usedNormanCards.RemoveAt(rnd);
        return NormanDeck[temp];
    }

    public NormanCard DrawNormanCard(int index)
    {
        return NormanDeck[index];
    }

    public NormanCard DrawRandomSaxonCard()
    {
        rnd = Random.Range(0, usedSaxonCards.Count);
        temp = usedSaxonCards[rnd];
        usedSaxonCards.RemoveAt(rnd);
        return SaxonDeck[temp];
    }

    public NormanCard DrawSaxonCard(int index)
    {
        return SaxonDeck[index];
    }

    public NormanObjectiveCard DrawNormanObj(int index)
    {
        return NormanObjectives[index];
    }
    public NormanObjectiveCard DrawSaxonObj(int index)
    {
        return SaxonObjectives[index];
    }
}
