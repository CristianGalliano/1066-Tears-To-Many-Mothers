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
    public bool NLeaderPlaced, SLeaderPlaced, FirstDrawN, FirstDrawS = false;

    private CardFucntionScript FunctScript;
    private CardDisplayScript UI;

    public bool USERESOURCES = false;

    private bool canDraw = true;
    private bool checkHand = true;

    public GameObject endturnButton, passTurnButton, endRoundButton;
    public GameObject passUI;
    public GameObject colliders;
    public Text PassText;

    public int phase = 1;
    public bool NormanPass, SaxonPass;
    public bool WedgeAttacked, ObjAttacked;
    public bool firstPasser;
    public int passedFirst;


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
        if(phase == 0)
        {
            passTurnButton.SetActive(false);
            endRoundButton.SetActive(false);

            normanResources = 0;
            saxonResources = 0;

            EndButtonHide();

            if(NLeaderPlaced && SLeaderPlaced && !FirstDrawN && !FirstDrawS && turn == 0)
            {
                StartCoroutine(NormanStartDraw());
                StartCoroutine(SaxonStartDraw());
                FirstDrawN = true;
                FirstDrawS = true;
            }

            if (FirstDrawN && FirstDrawS && turn == 0)
            {
                Debug.Log("Downto6");
                DownTo6("norman");
                DownTo6("saxon");
            }

            if (NLeaderPlaced && SLeaderPlaced && canDraw == true && turn == 0)
            {
                NDS.drawFunc(2);
                SDS.drawFunc(2);
                canDraw = false;
                phase = 1;
            }
        }

        if (phase == 1)
        {
            passTurnButton.SetActive(true);
            endRoundButton.SetActive(false);
        }

        if(phase == 2)
        {
            passTurnButton.SetActive(false);
            endturnButton.SetActive(false);
            endRoundButton.SetActive(true);

            if (!WedgeAttacked)
            {
                foreach(WedgeScript wedge in wedges)
                {
                    wedge.WedgeBattle();
                }

                WedgeAttacked = true;
            }

            if (!ObjAttacked)
            {
                if (NormanObj.objNum > 0)
                    NormanObj.AttackObjective();
                else
                    NormanObj.activate = true;



                if (SaxonObj.objNum > 0)
                    SaxonObj.AttackObjective();
                else
                    SaxonObj.activate = true;

                ObjAttacked = true;
            }
        }

        /*
        if (numberOfTurns <= 2)
        {
            if (turn == 0 && NLeaderPlaced)
            {
                Debug.Log("norman leader placed, conditions met");
                endturnButton.SetActive(true);
            }
            else if (turn == 0 && !NLeaderPlaced)
            {
                endturnButton.SetActive(false);              
            }

            if (turn == 1 && SLeaderPlaced)
            {
                endturnButton.SetActive(true);
            }
            else if (turn == 1 && !SLeaderPlaced)
            {
                endturnButton.SetActive(false);
            }
        }
        if (turn == 0)
        {
            if (checkHand == true)
            {
                DownTo6("norman");
            }
            if (NLeaderPlaced && numberOfTurns > 3 && canDraw == true)
            {
                NDS.drawFunc(2);
                canDraw = false;
            }
        }
        else if (turn == 1)
        {
            if (checkHand == true)
            {
                DownTo6("saxon");
            }
            if (SLeaderPlaced && numberOfTurns > 3 && canDraw == true)
            {
                SDS.drawFunc(2);
                canDraw = false;
            }
        }
        if (NLeaderPlaced && !FirstDrawN && numberOfTurns > 2 )
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
        */

    }

    public void NextRound()
    {
        phase = 0;
        WedgeAttacked = false;
        ObjAttacked = false;
        SaxonPass = false;
        NormanPass = false;

        turn = passedFirst - 1;
        PassTurn();
        camControl.switchTopDown();
    }

    void EndButtonHide()
    {
        if (turn == 0 && NLeaderPlaced)
        {
            Debug.Log("norman leader placed, conditions met");
            endturnButton.SetActive(true);
        }
        else if (turn == 0 && !NLeaderPlaced)
        {
            endturnButton.SetActive(false);
        }

        if (turn == 1 && SLeaderPlaced)
        {
            endturnButton.SetActive(true);
        }
        else if (turn == 1 && !SLeaderPlaced)
        {
            endturnButton.SetActive(false);
        }
    }

    private IEnumerator NormanStartDraw()
    {
        yield return null;
        NDS.drawFunc(4);
    }

    private IEnumerator SaxonStartDraw()
    {
        yield return null;
        SDS.drawFunc(4);
    }

    private IEnumerator drawLeaders()
    {
        yield return null;
        SDS.drawFunc(1);
        NDS.drawFunc(1);
    }

    void DownTo6(string i)
    {
        int numToDiscard = 0;
        switch (i)
        {
            case "norman":
                GameObject normanHand = GameObject.Find("normanHand");
                CardScript[] normanCards = normanHand.GetComponentsInChildren<CardScript>();
                List<NormanCard> normanCardsInHand = new List<NormanCard>();

                foreach (CardScript card in normanCards)
                {
                    normanCardsInHand.Add(card.normanCard);
                }

                Debug.Log(numToDiscard + " " + normanCardsInHand.Count);

                if (normanCardsInHand.Count > 6)
                {
                    numToDiscard = normanCardsInHand.Count - 6;
                }

                if (numToDiscard > 0)
                {
                    if (!FunctScript.DiscardLimitSet)
                    {
                        FunctScript.DiscardLimit = numToDiscard;
                        FunctScript.DiscardLimitSet = true;
                    }
                    UI.ShowGraveyard(normanCardsInHand);
                }
                if (numToDiscard == 0)
                {
                    checkHand = false;
                    canDraw = true;
                }
                break;
            case "saxon":
                GameObject saxonHand = GameObject.Find("saxonHand");
                CardScript[] saxonCards = saxonHand.GetComponentsInChildren<CardScript>();
                List<NormanCard> saxonCardsInHand = new List<NormanCard>();

                foreach (CardScript card in saxonCards)
                {
                    saxonCardsInHand.Add(card.saxonCard);
                }

                if (saxonCardsInHand.Count > 6)
                {
                    numToDiscard = saxonCardsInHand.Count - 6;
                }

                if (numToDiscard > 0)
                {
                    if (!FunctScript.DiscardLimitSet)
                    {
                        FunctScript.DiscardLimit = numToDiscard;
                        FunctScript.DiscardLimitSet = true;
                    }
                    UI.ShowGraveyard(saxonCardsInHand);
                }
                if (numToDiscard == 0)
                {
                    checkHand = false;
                    canDraw = true;
                }
                break;
        }
    }

    public void EndTurn()
    {
        numberOfTurns++;
        turn++;
        checkHand = true;

        if (turn > 1)
        {
            turn = 0;
        }

        passUI.SetActive(false);
        colliders.SetActive(false);
        camControl.changeImage(turn);

        if(NormanPass && SaxonPass)
        {
            phase = 2;
            camControl.switchTopDown();
        }
    }

    public void SetPass()
    {
        if (turn == 0)
        {
            if(!firstPasser)
            {
                passedFirst = 0;
            }

            NormanPass = true;
        }
        if (turn == 1)
        {
            if (!firstPasser)
            {
                passedFirst = 1;
            }

            SaxonPass = true;
        }

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

    public void PassTurn()
    {
        passUI.SetActive(true);
        if (turn == 0)
        {
            PassText.text = "Pass To Saxon Player!";
        }
        else if (turn == 1)
        {
            PassText.text = "Pass To Norman Player!";
        }
        colliders.SetActive(true);
    }
}
