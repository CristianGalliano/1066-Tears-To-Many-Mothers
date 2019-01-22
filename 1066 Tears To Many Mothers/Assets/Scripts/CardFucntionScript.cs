using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFucntionScript : MonoBehaviour
{
    private DeckManager Deck;
	// Use this for initialization
	void Start ()
    {
        Deck = GameObject.Find("DeckManager").GetComponent<DeckManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Damage(NormanCard attacker, NormanCard Target, int Amount, int Range ,string Condition)
    {
        
    }
}
