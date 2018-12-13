﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public List<Vector3> normanDropPoints = new List<Vector3> { new Vector3(300,0,326), new Vector3(0,0,326), new Vector3(-300,0,326),
                                                               new Vector3(300,0,663), new Vector3(0,0,663), new Vector3(-300,0,663),
                                                               new Vector3(300,0,1000), new Vector3(0,0,1000), new Vector3(-300,0,1000)};
    public List<Vector3> saxonDropPoints = new List<Vector3> { new Vector3(300,0,-326), new Vector3(0,0,-326), new Vector3(-300,0,-326),
                                                               new Vector3(300,0,-663), new Vector3(0,0,-663), new Vector3(-300,0,-663),
                                                               new Vector3(300,0,-1000), new Vector3(0,0,-1000), new Vector3(-300,0,-1000)};

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
        NDS.drawFunc(4);
        NDS.drawFunc(2);
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
            SDS.drawFunc(2);
        }
        else if (turn == 0)
        {
            saxonResources = 0;
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

    public void damageEnemy()
    {
    }
}
