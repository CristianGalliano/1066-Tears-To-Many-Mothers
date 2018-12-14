using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplayScript : MonoBehaviour {

    public GameObject panel;
    public Image sprite;
    public Text Name, Type, Cost, Might, Zeal, Health, Abilities, CardNumber;
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SetDisplay(NormanCard card)
    {
        panel.gameObject.SetActive(true);

        Name.text = card.name;
        Type.text = card.type;
        Cost.text = "Cost: " + card.cost.ToString();
        Might.text = "Might: " + card.might.ToString();
        Zeal.text = "Zeal: " + card.zeal.ToString();
        Health.text = "Health: " + card.health.ToString();
        Abilities.text = card.abilities;
        CardNumber.text = "Card Number: " + card.cardNumber.ToString();
    }
}
