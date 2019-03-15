using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFucntionScript : MonoBehaviour
{
    private DeckManager Deck;
    private GameController Game;
    private HandScript HandN, HandS;
    private Vector3 positionOfMouse;
    public bool targeting, targetIsValid = false;
    public CardScript attackerScript, targetScript;
    public NormanCard attacker, target, onPlayAttacker, eventAttacker, eventTarget, attachment, attachTarget, tactic;
    public GameObject wedgeTarget;
    private bool usedEvent = false;

    public GraveyardScripts saxonGrave, normanGrave;

    public int DiscardCount = 0;

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
            UI.InfoButton.gameObject.SetActive(true);
        }
        else if (attacker == null && attackerScript == null)
        {
            UI.InfoButton.gameObject.SetActive(false);
        }

        if (targeting)
        {
            UI.TagretingText.SetActive(true);
            UI.Cancel.gameObject.SetActive(true);
        }
        else if(!targeting)
        {
            UI.TagretingText.SetActive(false);
            UI.Cancel.gameObject.SetActive(false);
        }
    }

    public void EnterTargeting()
    {
        if(attacker != null)
        {
            if(attacker.needTarget == true)
            {
                targeting = true;
            }
            else
            {
                target = attacker;
                targetScript = attackerScript;
            }
        }

    }

    void UseAbility()
    {
        targetIsValid = false;

        //if (attackerScript.gameObject.tag == "Norman")
        //{
        //    Spy("saxon");
        //}
        //else
        //{
        //    Spy("norman");
        //}
        
        switch (attacker.cardNumber)
        {
            case 1:
                Agile(target, 0);
                break;
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
            case 63:
                Agile(target, 0);
                break;
            case 64:
                Damage(attacker, target, 1, 3);
                break;
            case 66:
                Agile(target, 0);
                break;
            case 67:
                Agile(target, 0);
                break;
            case 68:
                Damage(attacker, target, 1, 3);
                break;
            case 69:
                Damage(attacker, target, 1, 5);
                break;
            case 70:
                Agile(target, 0);
                break;
            case 73:
                Agile(target, 0);
                break;
            case 76:
                if (target.type == "Cavalry")
                {
                    Destroy(attacker);
                    Heal(target, target.startHealth - target.health);
                }
                break;
            case 85:
                Agile(target, 0);
                break;
            case 91:
                if (target.type == "Unit" || target.type == "Character")
                {
                    Heal(target, 1);
                }
                break;
            case 104:
                if (target.zeal <= 1)
                {
                    Destroy(target);
                }
                break;
            case 105:
                //buff all saxons by 1 might
                break;
            case 130:
                Damage(attacker, target, 1, 5);
                break;
            case 131:
                Agile(target, 0);
                break;
            case 132:
                Agile(target, 0);
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
            case 159:
                Agile(target, 0);
                break;
            case 160:
                Agile(target, 0);
                break;
            case 161:
                Agile(target, 0);
                break;
        }

        if (targetIsValid)
        {
            attackerScript.tireCard();
        }
        targetIsValid = false;

        Reset();
    }

    void OnPlayAbility()
    {
        Damage(onPlayAttacker, target, 3, 3);

        /*
        switch (attacker.cardNumber)
        {
            case 1:

                break;
        }
        */

        if (targetIsValid)
        {
            attackerScript.tireCard();
        }

        targetIsValid = false;
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

        //Reset();
    }

    void Heal(NormanCard Target, int value)
    {
        if(attackerScript.tag == targetScript.tag)
        {
                Target.health += value;
                targetIsValid = true;
        }

        //Reset();
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

        //targetIsValid = true;
        //Reset();
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

        //Reset();
    }

    public void Destroy(NormanCard card)
    {
        if (card.cardNumber < 76)
        {
            normanGrave.sendToGrave(card);
        }
        else
        {
            saxonGrave.sendToGrave(card);
        }
        targetIsValid = true;
        card.health = 0;

        //Reset();
    }

    void Spy(string target)
    {
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
        int i = 0;
        if (gameObject.tag == "Norman")
        {
            foreach (GameObject child in HandN.transform)
            {
                i++;
            }
            if (i != 0)
            {
                target.cost -= value;
            }
        }
        else if (gameObject.tag == "Saxon")
        {
            foreach (GameObject child in HandS.transform)
            {
                i++;
            }
            if (1 != 0)
            {
                target.cost -= value;
            }
        }

        //Reset();
    }

    void Agile(NormanCard target, int targetWedge)
    {
        if(attackerScript.gameObject.tag == "Norman")
        {
            if (Game.normanLane[targetWedge] != 3)
            {
                Game.normanLane[target.lane]--;
                targetScript.gameObject.transform.position = new Vector3(Game.xPositions[targetWedge], targetScript.gameObject.transform.position.y, Game.normanDropPointsZ[2]);
                targetScript.lane = targetWedge;
                targetScript.laneNum = 3;
                Game.normanLane[targetWedge]++;
            }
        }

        if(attackerScript.gameObject.tag == "Saxon")
        {
            if (Game.saxonlane[targetWedge] != 3)
            {
                Game.saxonlane[target.lane]--;
                targetScript.gameObject.transform.position = new Vector3(Game.xPositions[targetWedge], targetScript.gameObject.transform.position.y, Game.saxonDropPointsZ[2]);
                targetScript.lane = targetWedge;
                targetScript.laneNum = 3;
                Game.saxonlane[targetWedge]++;
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
        targeting = false;
        usedEvent = false;
        DiscardCount = 0;
        Debug.Log("should have reset");
    }

    public void useEventCard()
    {
        Spy("norman");

        if (targetIsValid && !usedEvent)
        {
            if (attackerScript.gameObject.tag == "Norman")
                normanGrave.sendToGrave(eventAttacker);
            else if (attackerScript.gameObject.tag == "Saxon")
                saxonGrave.sendToGrave(eventAttacker);

            Destroy(attackerScript.gameObject);

            usedEvent = true;
        }
    }

    public void useAttachment()
    {
        if(wedgeTarget != null)
        {
            wedgeTarget.GetComponent<WedgeScript>().NormanZealBuff = 3;

            attackerScript.gameObject.transform.parent = wedgeTarget.transform;
            attackerScript.gameObject.transform.localPosition = new Vector3(40, 0, -1);
            attackerScript.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            attackerScript.InLane = false;
            attackerScript.placed = true;
        }

        if(targetScript != null)
        {
            Buff(attachTarget, "Zeal", 10);

            attackerScript.gameObject.transform.parent = targetScript.gameObject.transform;
            attackerScript.gameObject.transform.localPosition = new Vector3(40, 0, -1);
            attackerScript.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            attackerScript.InLane = false;
            attackerScript.placed = true;
        }

        Reset();
    }
}
