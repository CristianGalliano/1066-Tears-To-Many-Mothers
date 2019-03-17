using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [HideInInspector]
    public List<int> normanDropPointsZ = new List<int> { 250, 500, 750};
    [HideInInspector]
    public List<int> saxonDropPointsZ = new List<int> { -250, -500, -750};

    public List<int> xPositions = new List<int> { 300, 0, -300 };
    public List<int> normanLane = new List<int> { 0, 0, 0 };
    //public int[,] normanLane = new int[,] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
    public List<int> saxonlane = new List<int> { 0, 0, 0 };

    public DrawScript SDS , NDS;
    public int saxonResources = 0;
    public int normanResources = 0;
    public int turn = 0;
    public int phase = 0;
    public int numberOfTurns = 1;
    public Text SaxonResourcesText, NormanResourcesText;
    public int camCount = 0;
    public CameraControl camControl;
    public int normanTactics, saxonTactics = 0;

    public ObjectivesScript NormanObj, SaxonObj;

    public List <WedgeScript> wedges = new List<WedgeScript> {};
    public int normanWedgesTaken, saxonWedgesTaken = 0;

    public GameObject gameOverPanel, mainPanel;
    public Text winnerText;
    private bool gameOver = false;
    public bool NLeaderPlaced, SLeaderPlaced, FirstDrawN, FirstDrawS;

    private CardFucntionScript FunctScript;
    private CardDisplayScript UI;

    public bool USERESOURCES = false;

    // Use this for initialization
    void Start()
    {
        SDS = GameObject.Find("SaxonDeck").GetComponent<DrawScript>();
        NDS = GameObject.Find("NormanDeck").GetComponent<DrawScript>();
        FunctScript = gameObject.GetComponent<CardFucntionScript>();
        UI = GameObject.Find("UIController").GetComponent<CardDisplayScript>();
        mainPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        StartCoroutine(drawLeaders());
    }

    // Update is called once per frame
    void Update()
    {
        if(NLeaderPlaced && !FirstDrawN && numberOfTurns > 2)
        {
            StartCoroutine(NormanStartDraw());
            FirstDrawN = true;
        }

        if (SLeaderPlaced && !FirstDrawS && numberOfTurns > 2)
        {
            StartCoroutine(SaxonStartDraw());
            FirstDrawS = true;
        }

        if (gameOver == false)
        {
            SaxonResourcesText.text = "Saxon Resources : " + saxonResources;
            NormanResourcesText.text = "Norman Resources : " + normanResources;
            wedgeWon();
            showGameOverUI();
        }

        DownTo6();
    }

    private IEnumerator NormanStartDraw()
    {
        yield return new WaitForSeconds(0.25f);
        NDS.drawFunc(4);
    }

    private IEnumerator SaxonStartDraw()
    {
        yield return new WaitForSeconds(0.25f);
        SDS.drawFunc(4);
    }

    private IEnumerator drawLeaders()
    {
        yield return new WaitForSeconds(0.25f);
        SDS.drawFunc(1);
        NDS.drawFunc(1);
    }

    void DownTo6()
    {
        int numToDiscard = 0;

        GameObject hand = GameObject.Find("normanHand");
        CardScript[] cards = hand.GetComponentsInChildren<CardScript>();
        List<NormanCard> cardsInHand = new List<NormanCard>();

        foreach (CardScript card in cards)
        {
            cardsInHand.Add(card.normanCard);
        }

        if(cardsInHand.Count > 6)
            numToDiscard =  cardsInHand.Count - 6;

        if(numToDiscard > 0)
        {
            if(!FunctScript.DiscardLimitSet)
            {
                FunctScript.DiscardLimit = numToDiscard;
                FunctScript.DiscardLimitSet = true;
            }

            UI.ShowGraveyard(cardsInHand);
        }
    }

    public void EndTurn()
    {
        numberOfTurns++;
        turn++;

        foreach (WedgeScript wedge in wedges)
        {
            wedge.WedgeBattle();
        }

        if (turn > 1)
        {
            turn = 0;
        }
        if (turn == 1)
        {
            if(NormanObj.objNum  > 0)
            {
                NormanObj.AttackObjective();
            }
            else
            {
                NormanObj.activate = true;
            }
            
            normanResources = 0;
            //set normans to ready
            if(SLeaderPlaced && numberOfTurns > 3)
                SDS.drawFunc(2);
            
        }
        else if (turn == 0)
        {
            if (SaxonObj.objNum > 0)
            {
                SaxonObj.AttackObjective();
            }
            else
            {
                SaxonObj.activate = true;
            }
            saxonResources = 0;
            //set saxons to ready
            if (NLeaderPlaced && numberOfTurns > 3)
                NDS.drawFunc(2);
        }
        Debug.Log("turn : " + numberOfTurns);
        camControl.changeImage(turn);
    }

    public void damageObjective()
    {
    }

    public void wedgeWon()
    {
        int count = 0;
        foreach (WedgeScript wedge in wedges)
        {
            if (wedge.hasWon == true)
            {
                if (wedge.winner == 1)
                {
                    normanWedgesTaken++;
                }
                else if (wedge.winner == 2)
                {
                    saxonWedgesTaken++;
                }
                wedges.RemoveAt(count);
            }
            count++;
        }
    }

    public void damageEnemy(NormanCard attacker, NormanCard target)
    {

    }
    
    public void showGameOverUI()
    {
        if (normanWedgesTaken >= 2)
        {
            //show normans win.
            mainPanel.SetActive(false);
            gameOverPanel.SetActive(true);
            winnerText.text += " Normans Win!";
            gameOver = true;
        }
        else if (saxonWedgesTaken >= 2)
        {
            //show saxons win.
            mainPanel.SetActive(false);
            gameOverPanel.SetActive(true);
            winnerText.text += " Saxons Win!";
            gameOver = true;
        }
    }
}
