using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplayScript : MonoBehaviour {

    public GameObject panel;
    public Image image;
    public Text Name, Title, Type, Action, Constant, Response, OnPlay, Quote, Solo;
    public Text CostText, ZealText, MightText, HealthText;

    public bool panelActive = false;

    public Button InfoButton;
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
        panelActive = true;

        Name.text = card.name;
        Type.text = card.type;
        Title.text = card.title;

        Action.text = card.action;
        Constant.text = card.constant;
        Response.text = card.response;
        OnPlay.text = card.onPlay;

        Quote.text = card.quote;
        Solo.text = card.solo;

        CostText.text = card.cost.ToString();

        if (card.type == "Leader" || card.type == "Character" || card.type == "Unit")
        {
            ZealText.text = card.zeal.ToString();
            MightText.text = card.might.ToString();
            HealthText.text = card.health.ToString();
        }
        else
        {
            if (card.zeal == 0)
            {
                ZealText.text = "";

            }
            else
            {
                ZealText.text = card.zeal.ToString();
            }

            if (card.might == 0)
            {
                MightText.text = "";

            }
            else
            {
                MightText.text = card.might.ToString();
            }

            if (card.health == 0)
            {
                HealthText.text = "";

            }
            else
            {
                HealthText.text = card.health.ToString();
            }
        }

        image.sprite = Resources.Load<Sprite>("CardImages/" + card.cardNumber);
    }

    public void HideDisplay()
    {

        if(panelActive == true)
        {
            panelActive = false;
            panel.gameObject.SetActive(false);
        }
    }
}
