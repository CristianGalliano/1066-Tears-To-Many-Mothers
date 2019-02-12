using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFucntionScript : MonoBehaviour
{
    private DeckManager Deck;
    private GameController Game;
    private HandScript HandN, HandS;
    private Vector3 positionOfMouse;
    public bool targeting = false;
    public NormanCard attacker, target;
    // Use this for initialization
    void Start ()
    {
        Deck = GameObject.Find("DeckManager").GetComponent<DeckManager>();
        Game = GameObject.Find("GameController").GetComponent<GameController>();
        HandN = GameObject.Find("normanHand").GetComponent<HandScript>();
        HandS = GameObject.Find("saxonHand").GetComponent<HandScript>();
    }
	
	// Update is called once per frame
	void Update ()
    {
    }

    public void UseAbility()
    {
        Debug.Log(attacker.name);
    }

    void Damage(NormanCard Attacker, NormanCard Target, int Amount, int Range)
    {
        Target = target;
        if(InRange(Attacker,Target, Range))
        {
            Target.health -= Amount;
        }
    }

    void Destroy(NormanCard Attacker, NormanCard Target, int Range)
    {
        Target = target;
        if (InRange(Attacker, Target, Range))
        {
            Target.health = 0;
        }
    }

    void Heal(NormanCard Target, int value)
    {
        Target = target;
        Target.health += value;
    }

    void DrawCard(int value)
    {
        if(gameObject.tag == "Norman")
        {
            Game.NDS.drawFunc(value);
        }
        else if (gameObject.tag == "Saxon")
        {
            Game.SDS.drawFunc(value);
        }
    }

    void Buff(NormanCard target, string stat, int value)
    {
        switch(stat)
        {
            case "Zeal": target.zeal += value;
                break;
            case "Might":
                target.might += value;
                break;
        }
    }

    void Discard()
    {

    }

    void Spy()
    {

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
            foreach (GameObject child in HandN.transform)
            {
                i++;
            }
            if (1 != 0)
            {
                Game.SDS.drawFunc(value);
            }
        }

    }

    void Agile(NormanCard target, int targetWedge)
    {
        if(Game.normanLane[targetWedge] < 3)
        {
            
        }
    }

    bool InRange(NormanCard attacker, NormanCard target, int Range)
    {
        if (highest(attacker, target).PositionZ - lowest(attacker,target).PositionZ < Range)
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
}
