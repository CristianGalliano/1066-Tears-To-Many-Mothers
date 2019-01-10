using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public Dictionary<int, NormanCard> NormanDeck = new Dictionary<int, NormanCard>();
    public Dictionary<int, NormanCard> SaxonDeck = new Dictionary<int, NormanCard>();

    public Dictionary<string, NormanObjectiveCard> NormanObjectives = new Dictionary<string, NormanObjectiveCard>();
    public Dictionary<string, NormanObjectiveCard> SaxonObjectives = new Dictionary<string, NormanObjectiveCard>();

    public string[] cardDataRows;
    public string[] cardDataText;
    int rnd, idNum, temp;
    string idChar;

    public List<int> usedNormanCards;
    public List<int> usedSaxonCards;
    public List<string> testList;

    // Use this for initialization
    void Start()
    {
        //Initialising Norman Deck
        TextAsset NormanCardData = Resources.Load<TextAsset>("NormanCardData");
        cardDataRows = NormanCardData.text.Split('\n');

        for (int i = 1; i < cardDataRows.Length; i++)
        {
            cardDataText = cardDataRows[i].Split(',');
            idNum = int.Parse(cardDataText[0]) - 1;
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



        for (int i = 0; i < NormanDeck.Count; i++)
        {
            testList.Add(NormanDeck[i].name);
            usedNormanCards.Add(i);
        }



        //Initialising Saxon Deck
        TextAsset SaxonCardData = Resources.Load<TextAsset>("SaxonCardData");
        cardDataRows = SaxonCardData.text.Split('\n');

        for (int i = 1; i < cardDataRows.Length; i++)
        {
            cardDataText = cardDataRows[i].Split(',');
            idNum = int.Parse(cardDataText[0]) - 1;
            SaxonDeck.Add(idNum, ScriptableObject.CreateInstance<NormanCard>());


            SaxonDeck[idNum].cardNumber = idNum;
            SaxonDeck[idNum].name = cardDataText[1];
            SaxonDeck[idNum].title = cardDataText[2];
            SaxonDeck[idNum].type = cardDataText[3];

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

        for (int i = 85; i < SaxonDeck.Count + 84; i++)
        {
            testList.Add(SaxonDeck[i].name);
            usedSaxonCards.Add(i);
        }


        //Initialising Norman Objectives
        TextAsset NormanObjData = Resources.Load<TextAsset>("NormanObjData");
        cardDataRows = NormanObjData.text.Split('\n');

        for (int i = 1; i < cardDataRows.Length; i++)
        {
            cardDataText = cardDataRows[i].Split(',');
            idChar = cardDataText[1];
            NormanObjectives.Add(idChar, ScriptableObject.CreateInstance<NormanObjectiveCard>());

            NormanObjectives[idChar].cardNumber = int.Parse(cardDataText[0]);
            NormanObjectives[idChar].idChar = idChar;
            NormanObjectives[idChar].name = cardDataText[2];
            NormanObjectives[idChar].title = cardDataText[3];
            NormanObjectives[idChar].type = cardDataText[4];

            NormanObjectives[idChar].zeal = int.Parse(cardDataText[5]);
            NormanObjectives[idChar].might = int.Parse(cardDataText[6]);
            NormanObjectives[idChar].health = int.Parse(cardDataText[7]);

            NormanObjectives[idChar].ability = cardDataText[8];
            NormanObjectives[idChar].quote = cardDataText[9];
            NormanObjectives[idChar].solo = cardDataText[10];

        }

        //Initialising Saxon Objectives
        TextAsset SaxonObjData = Resources.Load<TextAsset>("SaxonObjData");
        cardDataRows = SaxonObjData.text.Split('\n');

        for (int i = 1; i < cardDataRows.Length; i++)
        {
            cardDataText = cardDataRows[i].Split(',');
            idChar = cardDataText[1];
            SaxonObjectives.Add(idChar, ScriptableObject.CreateInstance<NormanObjectiveCard>());

            SaxonObjectives[idChar].cardNumber = int.Parse(cardDataText[0]);
            SaxonObjectives[idChar].idChar = idChar;
            SaxonObjectives[idChar].name = cardDataText[2];
            SaxonObjectives[idChar].title = cardDataText[3];
            SaxonObjectives[idChar].type = cardDataText[4];

            SaxonObjectives[idChar].zeal = int.Parse(cardDataText[5]);
            SaxonObjectives[idChar].might = int.Parse(cardDataText[6]);
            SaxonObjectives[idChar].health = int.Parse(cardDataText[7]);

            SaxonObjectives[idChar].ability = cardDataText[8];
            SaxonObjectives[idChar].quote = cardDataText[9];
            SaxonObjectives[idChar].solo = cardDataText[10];

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

    public NormanObjectiveCard DrawNormanObj(string index)
    {
        return NormanObjectives[index];
    }
    public NormanObjectiveCard DrawSaxonObj(string index)
    {
        return SaxonObjectives[index];
    }
}
