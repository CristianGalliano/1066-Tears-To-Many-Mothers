using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplayScript : MonoBehaviour {

    public GameObject panel;
    public Image sprite;
    public Text Name, Type, Abilities, Flavour;
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
        Abilities.text = card.abilities;
        Flavour.text = card.flavour;
    }
}
