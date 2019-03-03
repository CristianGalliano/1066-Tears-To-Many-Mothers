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
    public NormanCard attacker, target;

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

        if (attacker != null && attackerScript != null)
        {
            UI.InfoButton.gameObject.SetActive(true);
        }
        else if (attacker == null && attackerScript == null)
        {
            UI.InfoButton.gameObject.SetActive(false);
        }
    }

    public void EnterTargeting()
    {
        if(attacker != null)
        {
            targeting = true;
        }
    }

    void UseAbility()
    {
        targetIsValid = false;
        Spy("saxon");
        switch (attacker.cardNumber)
        {
            case 1:

                break;
            case 22:

                break;
            case 60:
                Damage(attacker, target, 1, 3);
                break;
            case 61:
                Damage(attacker, target, 1, 3);
                break;
            case 63:

                break;
            case 64:
                Damage(attacker, target, 1, 3);
                break;
            case 66:

                break;
            case 67:

                break;
            case 68:
                Damage(attacker, target, 1, 3);
                break;
            case 69:
                Damage(attacker, target, 1, 5);
                break;
            case 70:

                break;
            case 73:

                break;
            case 76:

                break;
        }

        if (targetIsValid)
        {
            attackerScript.tireCard();
        }
    }

    void Damage(NormanCard Attacker, NormanCard Target, int Amount, int Range)
    {
        if(attackerScript.tag != targetScript.tag)
        {
            if (InRange(Attacker, Target, Range))
            {
                targetIsValid = true;
                Target.health -= Amount;
            }           
        }

        Reset();
    }

    void Heal(NormanCard Target, int value)
    {
        if(attackerScript.tag == targetScript.tag)
        {
            Target.health += value;
            targetIsValid = true;
        }

        Reset();
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

        targetIsValid = true;
        Reset();
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
        }

        Reset();
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

        Reset();
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
                Game.SDS.drawFunc(value);
            }
        }

        Reset();
    }

    void Agile(NormanCard target, int targetWedge)
    {
        if(Game.normanLane[targetWedge] < 3)
        {
            
        }

        Reset();
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
        targeting = false;
        DiscardCount = 0;
    }
}
