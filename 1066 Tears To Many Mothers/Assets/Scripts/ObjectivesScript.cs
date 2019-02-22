﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivesScript : MonoBehaviour
{
    public bool activate = false;
    public float decreaseHeight;
    private Vector3 scale;
    private Vector3 oPosition;
    private Vector3 mPosition;
    public int objNum = 0;
    private Renderer rend;
    public UsedObjectivesScript usedObj;

    public Material[] mats;
    private Dictionary<int, NormanObjectiveCard> objectives;
    private NormanObjectiveCard objective;
    private DeckManager deck;

    public GameObject field;

    public TextMesh stat, health;
    private int statToAttack, totalStat;
    // Start is called before the first frame update
    void Start()
    {
        deck = GameObject.Find("DeckManager").GetComponent<DeckManager>();

        if(gameObject.tag == "Norman")
        {
            objectives =  deck.NormanObjectives;
        }

        if (gameObject.tag == "Saxon")
        {
            objectives = deck.SaxonObjectives;
        }

        decreaseHeight = (gameObject.transform.localScale.z) / 7;
        oPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        rend = gameObject.GetComponent<Renderer>();
        changeMat();
    }

    // Update is called once per frame
    void Update()
    {
        if (activate == true)
        {
            nextCard();
            usedObj.increaseSize();
            activate = false;
        }
    }

    void nextCard()
    {
        if (objNum < 6)
        {
            objNum++;
            changeMat();
            changeCard();
            scale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z - decreaseHeight);
            mPosition = new Vector3(oPosition.x, (scale.z / 33) / 2, oPosition.z);
            transform.localPosition = mPosition;
            transform.localScale = scale;
        }
    }

    private void changeMat()
    {
        Material[] temp = rend.materials;
        temp[0] = mats[objNum];
        rend.materials = temp;
    }

    private void changeCard()
    {
        objective = objectives[objNum + 1];
        objective.startHealth = objective.health;
        objective.startMight = objective.might;
        objective.startZeal = objective.zeal;

        UpdateText();
    }

    public void UpdateText()
    {
        if (objective.zeal > 0 && objective.might == 0)
        {
            stat.text = objective.zeal.ToString();
            statToAttack = 0;

            if (objective.zeal > objective.startZeal)
                stat.color = Color.green;
            if (objective.zeal < objective.startZeal)
                stat.color = Color.red;
            if (objective.zeal == objective.startZeal)
                stat.color = Color.black;
        }
        else if (objective.might > 0 && objective.zeal == 0)
        {
            stat.text = objective.might.ToString();
            statToAttack = 1;

            if (objective.might > objective.startMight)
                stat.color = Color.green;
            if (objective.might < objective.startMight)
                stat.color = Color.red;
            if (objective.might == objective.startMight)
                stat.color = Color.black;
        }
        else
        {
            stat.text = "";
        }

        if (objective.health > 0)
        {
            health.text = objective.health.ToString();

            if (objective.health > objective.startHealth)
                health.color = Color.green;
            if (objective.health < objective.startHealth)
                health.color = Color.red;
            if (objective.health == objective.startHealth)
                health.color = Color.black;
        }
        else
        {
            health.text = "";
        }
    }

    public void AttackObjective()
    {
        CardScript[] cardscripts = field.GetComponentsInChildren<CardScript>();

        totalStat = 0;

        foreach(CardScript cardScript in cardscripts)
        {
            switch(cardScript.tag)
            {
                case "Norman":
                    switch (statToAttack)
                    {
                        case 0:
                            if (cardScript.normanCard != null && !cardScript.tired)
                                totalStat += cardScript.normanCard.zeal;
                            break;
                        case 1:
                            if (cardScript.normanCard != null && !cardScript.tired)
                                totalStat += cardScript.normanCard.might;
                            break;
                    }
                    break;
                case "Saxon":
                    switch (statToAttack)
                    {
                        case 0:
                            if (cardScript.saxonCard != null && !cardScript.tired)
                                totalStat += cardScript.saxonCard.zeal;
                            break;
                        case 1:
                            if (cardScript.saxonCard != null && !cardScript.tired)
                                totalStat += cardScript.saxonCard.might;
                            break;
                    }
                    break;
            }
        }

        switch (statToAttack)
        {
            case 0:
                objective.health -= totalStat - objective.zeal;
                break;
            case 1:
                objective.health -= totalStat - objective.might;
                break;
        }

        if(objective.health <= 0)
        {
            activate = true;
        }

        UpdateText();
    }
}
