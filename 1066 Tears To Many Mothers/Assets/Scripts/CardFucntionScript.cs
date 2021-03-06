﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFucntionScript : MonoBehaviour
{
    private DeckManager Deck;
    private GameController Game;
    private HandScript HandN, HandS;
    private Vector3 positionOfMouse;
    public bool targeting, agileTargeting, targetIsValid = false;
    public CardScript attackerScript, targetScript;
    public NormanCard attacker, target, onPlayAttacker, eventAttacker, eventTarget, attachment, attachTarget, agileTarget, tactic;
    public GameObject wedgeTarget;
    private bool usedEvent = false;

    public GraveyardScripts saxonGrave, normanGrave;

    public int DiscardCount, DiscardLimit = 0;
    public bool DiscardLimitSet;

    private CardDisplayScript UI;
    // Use this for initialization
    void Start ()
    {
        Deck = GameObject.Find("DeckManager").GetComponent<DeckManager>();
        Game = GameObject.Find("GameController").GetComponent<GameController>();
        HandN = GameObject.Find("normanHand").GetComponent<HandScript>();
        HandS = GameObject.Find("saxonHand").GetComponent<HandScript>();
        UI = GameObject.Find("UIController").GetComponent<CardDisplayScript>();
        saxonGrave = GameObject.Find("SaxonDiscardPile").GetComponent<GraveyardScripts>();
        normanGrave = GameObject.Find("NormanDiscardPile").GetComponent<GraveyardScripts>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(attacker != null && target != null)
        {
            UseAbility();         
        }

        if((attacker != null && agileTarget != null && wedgeTarget != null))
        {
            AgileAbility();
        }

        if(attacker == null && onPlayAttacker != null && target != null)
        {
            OnPlayAbility();
        }

        if(eventAttacker != null)
        {
            if (eventAttacker.needTarget)
                targeting = true;
            else
            {
                eventTarget = eventAttacker;
                targetScript = attackerScript;
            }

            if(eventTarget != null)
            {
                useEventCard();
            }
        }

        if(attachment != null)
        {
            targeting = true;

            if(attachTarget != null || wedgeTarget != null)
            {
                useAttachment();
            }
        }

        if (attacker != null && attackerScript != null)
        {
            if(attacker.action != "No Action Ability" || attacker.action.Contains("Move"))
                UI.InfoButton.gameObject.SetActive(true);

            if (attacker.action == "No Action Ability" || !attacker.action.Contains("Move"))
                UI.InfoButton.gameObject.SetActive(false);
        }
        else if (attacker == null && attackerScript == null)
        {
            UI.InfoButton.gameObject.SetActive(false);
        }

        if (targeting || agileTargeting)
        {
            UI.TagretingText.SetActive(true);
            UI.Cancel.gameObject.SetActive(true);
        }
        else if(!targeting && !agileTargeting)
        {
            UI.TagretingText.SetActive(false);
            UI.Cancel.gameObject.SetActive(false);
        }
    }

    void TargetValidityCheck(NormanCard attacker)
    {
        List<GameObject> cardsToGrey = new List<GameObject>();
        GameObject[] allNormanCards = GameObject.FindGameObjectsWithTag("Norman");
        GameObject[] allSaxonCards = GameObject.FindGameObjectsWithTag("Saxon");

        switch(attacker.cardNumber)
        {
            case 1:
                foreach(GameObject card in allNormanCards)
                {
                    if(card.GetComponent<CardScript>())
                    {
                        CardScript script = card.GetComponent<CardScript>();

                        if (script.placed && script.normanCard.type != "Character" && script.normanCard.type != "Unit")
                        {
                            cardsToGrey.Add(card);
                        }
                    }
                }

                foreach(GameObject card in allSaxonCards)
                {
                    if (card.GetComponent<CardScript>() && card.GetComponent<CardScript>().placed)
                        cardsToGrey.Add(card);
                }

                break;
        }

        foreach (GameObject card in cardsToGrey)
        {
            card.GetComponent<CardScript>().GreyOut();
        }

    }

    public void EnterTargeting()
    {
        if(attacker)
        {
            if (attacker.action.Contains("Move"))
            {
                if (attacker.action.Contains("Commander"))
                {
                    agileTargeting = true;
                    TargetValidityCheck(attacker);
                }
                else if (attacker.action.Contains("Agile"))
                {
                    agileTarget = attacker;
                    targetScript = attackerScript;
                    agileTargeting = true;
                }

            }
            else if (attacker.needTarget == true)
            {
                targeting = true;
                TargetValidityCheck(attacker);
            }
            else
            {
                target = attacker;
                targetScript = attackerScript;
            }
        }

        if(onPlayAttacker)
        {
            if(onPlayAttacker.needTarget)
            {
                targeting = true;
                TargetValidityCheck(onPlayAttacker);
            }
            else
            {
                target = onPlayAttacker;
                targetScript = attackerScript;
            }
        }
    }

    void UseAbility()
    {
        targetIsValid = false;

        switch (attacker.cardNumber)
        {
            case 22:
                Destroy(attacker);
                Damage(attacker, target, 1, 100);
                break;
            case 60:
                Damage(attacker, target, 1, 3);
                break;
            case 61:
                Damage(attacker, target, 1, 3);
                break;
            case 64:
                Damage(attacker, target, 1, 3);
                break;
            case 68:
                Damage(attacker, target, 1, 3);
                break;
            case 69:
                Damage(attacker, target, 1, 5);
                break;
            case 76:
                if (target.type == "Cavalry")
                {
                    Destroy(attacker);
                    Heal(target, target.startHealth - target.health);
                }
                break;
            case 91:
                if (target.type == "Unit" || target.type == "Character")
                {
                    Heal(target, 1);
                }
                break;
            case 130:
                Damage(attacker, target, 1, 5);
                break;
            case 133:
                Damage(attacker, attacker, 1, 100);
                Damage(attacker, target, 1, 100);
                break;
            case 134:
                Damage(attacker, attacker, 1, 100);
                Damage(attacker, target, 1, 100);
                break;
            case 135:
                Damage(attacker, attacker, 1, 100);
                Damage(attacker, target, 1, 100);
                break;
            case 141:
                Damage(attacker, target, 1, 3);
                break;
            case 142:
                Damage(attacker, target, 1, 3);
                break;
            case 148:
                Damage(attacker, target, 1, 3);
                break;
            case 155:
                Damage(attacker, target, 1,3);
                break;
        }

        if (targetIsValid)
        {
            attackerScript.tireCard();
        }
        targetIsValid = false;

        Reset();
    }

    void AgileAbility()
    {
        targetIsValid = false;

        Agile(ref targetScript, wedgeTarget.GetComponent<WedgeScript>().wedgeNum);
        
        if (targetIsValid)
        {
            attackerScript.tireCard();
        }

        targetIsValid = false;
        Reset();
    }

    void OnPlayAbility()
    {
        CardScript[] hand;

        switch (onPlayAttacker.cardNumber)
        {
            case 6:
                if (target.type != "Leader")
                    Damage(onPlayAttacker, target, 1);
                break;
            case 8:
                if (target.type != "Leader")
                    Destroy(target);
                break;
            case 14:
                DrawCard(1);
                break;
            case 20:
                if (target.type != "Leader")
                    Destroy(target);
                break;
            case 24:
                if (target.type != "Leader")
                    Damage(onPlayAttacker, target, 1);
                break;
            case 27:
                DrawCard(1);
                break;
            case 71:
                Spy("saxon", 1);
                break;
            case 72:
                if (target.type != "Leader")
                    Destroy(target);
                break;
            case 86:
                if (target.type != "Leader")
                    Destroy(target);
                break;
            case 89:
                DrawCard(1);
                break;
            case 94:
                DrawCard(1);
                break;
            case 100:
                DrawCard(1);
                break;
            case 101:
                if (target.type != "Leader")
                    Damage(onPlayAttacker, target, 1);
                break;
            case 102:
                if (target.type != "Leader")
                    Destroy(onPlayAttacker, target, 10);
                break;
            case 136:
                if (target.type != "Leader")
                    targetScript.tireCard();
                break;
            case 137:
                if (target.type != "Leader")
                    Destroy(target);
                break;
            case 138:
                foreach(CardScript card in GameObject.Find("saxonHand").GetComponents<CardScript>())
                {
                    Discount(card.saxonCard, 1);
                    card.tempBuffed = true;
                }
                break;
            case 139:
                hand = GameObject.Find("saxonHand").GetComponentsInChildren<CardScript>();

                foreach (CardScript card in hand)
                {
                    Discount(card.saxonCard, 1);
                    card.tempBuffed = true;
                }
                break;
            case 146:
                if (target.type != "Leader")
                    Damage(onPlayAttacker, target, 1);
                break;
            case 147:
                GameObject[] wedges = GameObject.FindGameObjectsWithTag("Wedge");
                bool damaged = false;

                foreach(GameObject wedge in wedges)
                {
                    WedgeScript w = wedge.GetComponent<WedgeScript>();

                    if (w.NormanDamage > 0 || w.SaxonDamage > 0)
                        damaged = true;
                }

                if (damaged)
                    if (target.resources > 0)
                        targetScript.tired = false;
                break;
            case 150:
                if (target.type != "Leader")
                    Damage(onPlayAttacker, target, 1);
                break;
            case 151:
                if (target.type != "Leader")
                    Damage(onPlayAttacker, target, 1);
                break;
            case 152:
                hand = GameObject.Find("saxonHand").GetComponentsInChildren<CardScript>();

                foreach (CardScript card in hand)
                {
                    Discount(card.saxonCard, 1);
                    card.tempBuffed = true;
                }
                break;
            case 153:
                if (target.type != "Leader")
                    Destroy(target, 10);
                break;
            case 156:
                foreach(string trait in target.traits)
                {
                    if (trait == "Cavalry")
                        Damage(onPlayAttacker, target, 1, 6);
                }
                break;
            case 157:
                foreach (string trait in target.traits)
                {
                    if (trait == "Cavalry")
                        Damage(onPlayAttacker, target, 1, 6);
                }
                break;
            case 158:
                foreach (string trait in target.traits)
                {
                    if (trait == "Cavalry")
                        Damage(onPlayAttacker, target, 1, 6);
                }
                break;
        }

        targetIsValid = false;

        Reset();
    }

    void Damage(NormanCard Attacker, NormanCard Target, int Amount, int Range)
    {
        if(attackerScript.tag != targetScript.tag)
        {
            if (InRange(Attacker, Target, Range))
            {
                targetIsValid = true;
                Target.health -= Amount;
                if(Target.health <= 0)
                {
                    if (targetScript.tag == "Norman")
                        normanGrave.sendToGrave(target);
                    if (targetScript.tag == "Saxon")
                        saxonGrave.sendToGrave(target);
                }
            }           
        }
        else
        {
            Reset();
        }
    }

    void Damage(NormanCard Attacker, NormanCard Target, int Amount)
    {
        if (attackerScript.tag != targetScript.tag)
        {
            targetIsValid = true;
            Target.health -= Amount;
            if (Target.health <= 0)
            {
                if (targetScript.tag == "Norman")
                    normanGrave.sendToGrave(target);
                if (targetScript.tag == "Saxon")
                    saxonGrave.sendToGrave(target);
            }
        }
        else
        {
            Reset();
        }
    }

    void Heal(NormanCard Target, int value)
    {
        if(attackerScript.tag == targetScript.tag)
        {
                Target.health += value;
                targetIsValid = true;
        }
    }

    void DrawCard(int value)
    {
        if(attackerScript.tag == "Norman")
        {
            Game.NDS.drawFunc(value);
        }
        else if (attackerScript.tag == "Saxon")
        {
            Game.SDS.drawFunc(value);
        }
    }

    void Buff(NormanCard target, string stat, int value)
    {
        if (attackerScript.tag == targetScript.tag)
        {
            switch (stat)
            {
                case "Zeal":
                    target.zeal += value;
                    break;
                case "Might":
                    target.might += value;
                    break;
            }

            targetIsValid = true;
        }
    }

    public void Destroy(NormanCard attacker,NormanCard target, int range)
    {
        if (InRange(attacker, target, range))
        {
            if (target.cardNumber <= 77)
            {
                normanGrave.sendToGrave(target);
            }
            else
            {
                saxonGrave.sendToGrave(target);
            }

            targetIsValid = true;
            target.health = 0;
        }
    }

    public void Destroy(NormanCard card)
    {
        if (card.cardNumber <=77)
        {
            normanGrave.sendToGrave(card);
        }
        else
        {
            saxonGrave.sendToGrave(card);
        }
        targetIsValid = true;
        card.health = 0;
    }

    void Spy(string target, int num)
    {
        DiscardLimit = num;

        GameObject hand = GameObject.Find(target + "Hand");
        CardScript[] cards = hand.GetComponentsInChildren<CardScript>();
        List<NormanCard> cardsInHand = new List<NormanCard>();

        foreach (CardScript card in cards)
        {
            if(target == "norman")
                cardsInHand.Add(card.normanCard);

            if(target == "saxon")
                cardsInHand.Add(card.saxonCard);
        }

        UI.ShowGraveyard(cardsInHand);

        targetIsValid = true;
    }

    void Discount(NormanCard target, int value)
    {
        if (target.cost - value > 0)
        {
            target.cost -= value;
        }
        else if (target.cost - value <= 0)
        {
            target.cost = 0;
        }
    }

    void Agile(ref CardScript target, int targetWedge)
    {
        if(target.gameObject.tag == "Norman")
        {
            if (Game.normanLane[targetWedge] != 3)
            {
                Game.normanLane[target.lane]--;
                target.gameObject.transform.position = new Vector3(Game.xPositions[targetWedge], target.gameObject.transform.position.y, Game.normanDropPointsZ[2]);
                target.lane = targetWedge;
                target.laneNum = 3;
                Game.normanLane[targetWedge]++;
                targetIsValid = true;
            }
        }

        if(target.gameObject.tag == "Saxon")
        {
            if (Game.saxonlane[targetWedge] != 3)
            {
                Game.saxonlane[target.lane]--;
                target.gameObject.transform.position = new Vector3(Game.xPositions[targetWedge], target.gameObject.transform.position.y, Game.saxonDropPointsZ[2]);
                target.lane = targetWedge;
                target.laneNum = 3;
                Game.saxonlane[targetWedge]++;
                targetIsValid = true;
            }
        }
    }

    void Attachment(NormanCard target, string stat)
    {

    }

    bool InRange(NormanCard attacker, NormanCard target, int Range)
    {
        if (highest(attacker, target).PositionZ - lowest(attacker,target).PositionZ <= Range && (attacker.lane == target.lane))
        {
            return true;
        }
        else 
        {
            return false;
        }

    }

    NormanCard highest(NormanCard a, NormanCard b)
    {
        if(a.PositionZ > b.PositionZ)
        {
            return a;
        }
        else
        {
            return b;
        }
    }

    NormanCard lowest(NormanCard a, NormanCard b)
    {
        if (a.PositionZ < b.PositionZ)
        {
            return a;
        }
        else
        {
            return b;
        }
    }

    public void Reset()
    {
        attacker = null;
        target = null;

        onPlayAttacker = null;

        eventAttacker = null;
        eventTarget = null;

        attachment = null;
        attachTarget = null;

        wedgeTarget = null;

        agileTarget = null;

        targeting = false;
        agileTargeting = false;

        usedEvent = false;

        DiscardCount = 0;
        DiscardLimit = 0;
        DiscardLimitSet = false;

        UnGrey();
    }

    public void UnBuff(string targetHand)
    {

        if (targetHand == "norman")
        {
            foreach (CardScript card in GameObject.Find(targetHand + "Hand").GetComponentsInChildren<CardScript>())
            {
                if (card.tempBuffed)
                {
                    card.normanCard.cost = card.normanCard.startCost;
                    card.tempBuffed = false;
                }
            }
        }

        if (targetHand == "saxon")
        {
            foreach (CardScript card in GameObject.Find(targetHand + "Hand").GetComponentsInChildren<CardScript>())
            {
                if (card.tempBuffed)
                {
                    card.saxonCard.cost = card.saxonCard.startCost;
                    card.tempBuffed = false;
                }
            }
        }
    }

    private void UnGrey()
    {
        GameObject[] allNormanCards = GameObject.FindGameObjectsWithTag("Norman");
        GameObject[] allSaxonCards = GameObject.FindGameObjectsWithTag("Saxon");

        foreach(GameObject card in allNormanCards)
        {
            if(card.GetComponent<CardScript>())
            {
                card.GetComponent<CardScript>().UnGrey();
            }
        }

        foreach (GameObject card in allSaxonCards)
        {
            if (card.GetComponent<CardScript>())
            {
                card.GetComponent<CardScript>().UnGrey();
            }
        }
    }

    public void useEventCard()
    {
        switch (eventAttacker.cardNumber)
        {
            case 104:
                if (target.zeal <= 1)
                {
                    Destroy(target);
                }
                break;
            case 105:
                GameObject[] saxons = GameObject.FindGameObjectsWithTag("Saxon");
                foreach (GameObject saxon in saxons)
                {
                    if (saxon.GetComponent<CardScript>())
                    {
                        if (saxon.GetComponent<CardScript>().placed && saxon.GetComponent<CardScript>().saxonCard && saxon.GetComponent<CardScript>().saxonCard.type == "Unit")
                        {
                            if (saxon.GetComponent<CardScript>().saxonCard.traits[0] == "Saxon" || saxon.GetComponent<CardScript>().saxonCard.traits[1] == "Saxon" || saxon.GetComponent<CardScript>().saxonCard.traits[2] == "Saxon")
                            {
                                saxon.GetComponent<CardScript>().saxonCard.might++;
                            }
                        }
                    }
                }
                break;
        }


        if (targetIsValid && !usedEvent)
        {
            if (attackerScript.gameObject.tag == "Norman")
                normanGrave.sendToGrave(eventAttacker);
            else if (attackerScript.gameObject.tag == "Saxon")
                saxonGrave.sendToGrave(eventAttacker);

            Destroy(attackerScript.gameObject);

            usedEvent = true;
        }

        Reset();
    }

    public void useAttachment()
    {
        if(wedgeTarget != null)
        {
            switch(attachment.cardNumber)
            {
                case 55:
                    wedgeTarget.GetComponent<WedgeScript>().NormanMightBuff += 2;
                    break;
                case 56:
                    wedgeTarget.GetComponent<WedgeScript>().NormanZealBuff += 2;
                    break;
                case 58:
                    wedgeTarget.GetComponent<WedgeScript>().NormanMightBuff += 1;
                    break;
                case 125:
                    wedgeTarget.GetComponent<WedgeScript>().SaxonMightBuff += 2;
                    break;
                case 128:
                    wedgeTarget.GetComponent<WedgeScript>().SaxonMightBuff += 1;
                    break;
            }

            attackerScript.gameObject.transform.parent = wedgeTarget.transform;
            attackerScript.gameObject.transform.localPosition = new Vector3(40, 0, -1);
            attackerScript.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            attackerScript.InLane = false;
            attackerScript.placed = true;

            if(attackerScript.tag == "Norman")
            {
                attackerScript.normanCard.cost = attackerScript.normanCard.startCost;
                UnBuff("norman");
            }

            if (attackerScript.tag == "Saxon")
            {
                attackerScript.saxonCard.cost = attackerScript.saxonCard.startCost;
                UnBuff("saxon");
            }
        }

        if(targetScript != null)
        {
            switch (attachment.cardNumber)
            {
                case 57:
                    if (target.type == "Unit")
                        target.zeal += 1;
                    break;
                case 59:
                    if (target.cardNumber == 85)
                        target.zeal -= 1;
                    break;
                case 125:
                    if (target.cardNumber == 85)
                        target.zeal += 1;
                    break;
                case 126:
                    if (target.cardNumber == 85)
                        target.zeal += 1;
                    break;
                case 127:
                    if (target.type == "Unit")
                        target.zeal += 2;
                    break;
            }

            attackerScript.gameObject.transform.parent = targetScript.gameObject.transform;
            attackerScript.gameObject.transform.localPosition = new Vector3(40, 0, -1);
            attackerScript.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            attackerScript.InLane = false;
            attackerScript.placed = true;
        }

        Reset();
    }
}
