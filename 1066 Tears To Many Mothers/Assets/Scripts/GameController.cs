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
    private bool NormanDT6, SaxonDT6 = false;
    public bool passTurn = false;

    public GameObject playerHUD;

    public List<Transform> discardList = new List<Transform>();


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
        if (gameOver == false)
        {
            SaxonResourcesText.text = "Saxon Resources : " + saxonResources;
            NormanResourcesText.text = "Norman Resources : " + normanResources;
            wedgeWon();
            showGameOverUI();
        }
        if (phase == 0)
        {
            playerHUD.SetActive(false);
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

            if (FirstDrawN && FirstDrawS && (!NormanDT6 || !SaxonDT6))
            {
                Debug.Log("Downto6");
                if(passedFirst == 0)
                {
                    DownTo6("norman");
                    if (NormanDT6)
                    {
                        DownTo6("saxon");
                        if(!SaxonDT6)
                        {
                            //show pass ui
                            if (passTurn == false)
                            {
                                PassTurn();
                                passTurn = true;

                            }
                        }
                    }
                }

                if (passedFirst == 1)
                {
                    DownTo6("saxon");
                    if (SaxonDT6)
                    {
                        DownTo6("norman");
                        if (!NormanDT6)
                        {
                            //show pass ui
                            if (passTurn == false)
                            {
                                PassTurn();
                                passTurn = true;
                            }
                        }
                    }
                }

                if(NormanDT6 && SaxonDT6)
                {
                    canDraw = true;
                }
            }

            if (NLeaderPlaced && SLeaderPlaced && NormanDT6 && SaxonDT6)
            {
                NDS.drawFunc(2);
                SDS.drawFunc(2);
                phase = 1;
            }
        }

        if (phase == 1)
        {
            playerHUD.SetActive(true);
            passTurnButton.SetActive(true);
            endRoundButton.SetActive(false);
            if (SaxonPass && turn == 0)
            {
                endturnButton.SetActive(false);
            }
            else if (NormanPass && turn == 1)
            {
                endturnButton.SetActive(false);
            }
            else
            {
                endturnButton.SetActive(true);
            }
        }

        if(phase == 2)
        {
            playerHUD.SetActive(false);
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
                if (SaxonObj.objNum == 0)
                {
                    SaxonObj.activate = true;
                }
                else
                {
                    SaxonObj.AttackObjective();
                }

                if (NormanObj.objNum == 0)
                {
                    NormanObj.activate = true;
                }
                else
                {
                    NormanObj.AttackObjective();
                }

                ObjAttacked = true;
            }
            Debug.Log("Saxon Objective Num: " + SaxonObj.objNum);
            Debug.Log("Norman Objective Num: " + NormanObj.objNum);
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
        firstPasser = false;
        passTurn = false;

        SaxonDT6 = false;
        NormanDT6 = false;

        if (passedFirst == 0)
        { 
            turn = 1;
        }
        else if (passedFirst == 1)
        {
            turn = 0;
        }

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

    void DownTo6(string s)
    {
        int numToDiscard = 0;

        GameObject Hand = GameObject.Find(s + "Hand");
        CardScript[] Cards = Hand.GetComponentsInChildren<CardScript>();
        List<NormanCard> normanCardsInHand = new List<NormanCard>();

        if (s == "norman")
        {
            foreach (CardScript card in Cards)
            {
                normanCardsInHand.Add(card.normanCard);
            }

        }
        else if(s == "saxon")
        {
            foreach (CardScript card in Cards)
            {
                normanCardsInHand.Add(card.saxonCard);
            }
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

            if (s == "norman")
            {
                NormanDT6 = true;
            }
            else if (s == "saxon")
            {
                SaxonDT6 = true;
            }
        }
    }

    public void EndTurn()
    {
        if(phase != 2)
        {
            turn++;
        }

        numberOfTurns++;
        checkHand = true;

        if (turn > 1)
        {
            turn = 0;
        }

        passUI.SetActive(false);
        colliders.SetActive(false);
        camControl.changeImage(turn);
        if (NormanPass && SaxonPass)
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
                firstPasser = true;
                passedFirst = 0;
            }

            NormanPass = true;
        }
        if (turn == 1)
        {
            if (!firstPasser)
            {
                firstPasser = true;
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
        if (phase == 1)
        {
            if (!SaxonPass && turn == 0)
            {
                passUI.SetActive(true);
            }
            if (!NormanPass && turn == 1)
            {
                passUI.SetActive(true);
            }
        }
        else
        {
            passUI.SetActive(true);
        }
        if (turn == 0)
        {
            PassText.text = "Pass To Saxon Player!";
        }
        else if (turn == 1)
        {
            PassText.text = "Pass To Norman Player!";
        }
        colliders.SetActive(true);
        if (NormanPass && SaxonPass)
        {
            phase = 2;
            camControl.switchTopDown();
            colliders.SetActive(false);
        }
    }

    public void RunDiscard()
    {
        //foreach(CardScript card in discardList)
        //{
        //    discardList.Remove(card);

        //    if (card.tag == "Norman")
        //        FunctScript.Destroy(card.normanCard);
        //    if (card.tag == "Saxon")
        //        FunctScript.Destroy(card.saxonCard);

        //    Destroy(card.gameObject);

        //}
    }
}
