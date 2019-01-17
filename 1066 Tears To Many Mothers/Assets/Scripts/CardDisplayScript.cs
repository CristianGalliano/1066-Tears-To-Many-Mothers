using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplayScript : MonoBehaviour {

    public GameObject panel;
    public Image image;
    public Text Name, Title, Type, Action, Constant, Response, OnPlay, Quote, Solo;
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
        Title.text = card.title;

        Action.text = card.action;
        Constant.text = card.constant;
        Response.text = card.response;
        OnPlay.text = card.onPlay;

        Quote.text = card.quote;
        Solo.text = card.solo;

        image.sprite = Resources.Load<Sprite>("CardImages/Sprites" + card.cardNumber);
    }
}
