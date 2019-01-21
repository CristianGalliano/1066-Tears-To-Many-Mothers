using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public List<int> normanDropPointsZ = new List<int> { 326, 663, 1000};
    public List<int> saxonDropPointsZ = new List<int> { -326, -663, -1000};
    public List<int> xPositions = new List<int> { 300, 0, -300 };
    public List<int> normanLane = new List<int> { 0, 0, 0 };
    public List<int> saxonlane = new List<int> { 0, 0, 0 };
    public DrawScript SDS , NDS;
    public int saxonResources = 0;
    public int normanResources = 0;
    public int turn = 0;
    public int numberOfTurns = 1;
    public Text SaxonResourcesText, NormanResourcesText;

    // Use this for initialization
    void Start()
    {
        SDS = GameObject.Find("SaxonDeck").GetComponent<DrawScript>();
        NDS = GameObject.Find("NormanDeck").GetComponent<DrawScript>();
        StartCoroutine("startDraw");
    }

    // Update is called once per frame
    void Update()
    {
        SaxonResourcesText.text = "Saxon Resources : " + saxonResources;
        NormanResourcesText.text = "Norman Resources : " + normanResources;
    }

    private IEnumerator startDraw()
    {
        yield return new WaitForSeconds(0.25f);
        SDS.drawFunc(4);
        NDS.drawFunc(6);
    }

    private IEnumerator drawLeaders()
    {
        yield return new WaitForSeconds(0.25f);
    }

    public void EndTurn()
    {
        numberOfTurns++;
        turn++;
        if (turn > 1)
        {
            turn = 0;
        }
        if (turn == 1)
        {
            normanResources = 0;
            //set normans to ready
            SDS.drawFunc(2);
        }
        else if (turn == 0)
        {
            saxonResources = 0;
            //set saxons to ready
            NDS.drawFunc(2);
        }
        Debug.Log("turn : " + numberOfTurns);

    }

    public void damageObjective()
    {
    }

    public void damageWedge()
    {
    }

    public void damageEnemy(NormanCard attacker, NormanCard target)
    {

    }
}
