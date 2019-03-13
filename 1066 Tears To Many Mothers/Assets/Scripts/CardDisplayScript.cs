using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplayScript : MonoBehaviour {

    public GameObject CardPanel, ObjPanel, GraveyardPanel, GraveScroll;
    public Image image, image2;
    public Text Name, Title, Type, Action, Constant, Response, OnPlay, Quote, Solo;
    public Text CostText, ZealText, MightText, HealthText, ResourcesText;
    public Text ObjStat, ObjHealth;
    public GameObject TagretingText;

    public RectTransform GraveContent;
    public GameObject UICard;

    public GameObject WedgePanel;
    public Image WedgeImage;
    public Text NormanDamage, SaxonDamage;

    public bool panelActive = false;

    public bool GraveyardShown = false;

    public Button InfoButton;

    private Vector3 startPos;
    private float startRight;
    // Use this for initialization
    void Start ()
    {
        startPos = GraveContent.transform.position;
        startRight = GraveContent.rect.width;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SetDisplay(NormanCard card)
    {
        CardPanel.gameObject.SetActive(true);
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
            if (card.cost == card.startCost)
                CostText.color = Color.black;
            if (card.zeal == card.startZeal)
                ZealText.color = Color.black;
            if (card.might == card.startMight)
                MightText.color = Color.black;
            if (card.health == card.startHealth)
                HealthText.color = Color.black;

            if (card.cost < card.startCost)
                CostText.color = Color.red;
            if (card.zeal < card.startZeal)
                ZealText.color = Color.red;
            if (card.might < card.startMight)
                MightText.color = Color.red;
            if (card.health < card.startHealth)
                HealthText.color = Color.red;

            if (card.cost > card.startCost)
                CostText.color = Color.green;
            if (card.zeal > card.startZeal)
                ZealText.color = Color.green;
            if (card.might > card.startMight)
                MightText.color = Color.green;
            if (card.health > card.startHealth)
                HealthText.color = Color.green;

            ZealText.text = card.zeal.ToString();
            MightText.text = card.might.ToString();
            HealthText.text = card.health.ToString();
            ResourcesText.text = card.resources.ToString();
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

    public void SetObjDisplay(NormanObjectiveCard card)
    {
        ObjPanel.gameObject.SetActive(true);
        panelActive = true;

        if (card.might > 0 && card.zeal == 0)
            ObjStat.text = card.might.ToString();
        if (card.zeal > 0 && card.might == 0)
            ObjStat.text = card.zeal.ToString();
        if (card.health != 0)
        {
            ObjHealth.text = card.health.ToString();
        }



        if (card.zeal == card.startZeal)
            ZealText.color = Color.black;
        if (card.might == card.startMight)
            MightText.color = Color.black;
        if (card.health == card.startHealth)
            ObjHealth.color = Color.black;

        if (card.zeal < card.startZeal)
            ObjStat.color = Color.red;
        if (card.might < card.startMight)
            ObjStat.color = Color.red;
        if (card.health < card.startHealth)
            ObjHealth.color = Color.red;

        if (card.zeal > card.startZeal)
            ObjStat.color = Color.green;
        if (card.might > card.startMight)
            ObjStat.color = Color.green;
        if (card.health > card.startHealth)
            ObjHealth.color = Color.green;

        image2.sprite = Resources.Load<Sprite>("CardImages/" + card.cardNumber);
    }

    public void HideDisplay()
    {

        if(panelActive == true)
        {
            panelActive = false;
            CardPanel.gameObject.SetActive(false);
            ObjPanel.gameObject.SetActive(false);

            GraveyardPanel.gameObject.SetActive(false);
            GraveContent.transform.position = startPos;
            GraveyardShown = false;

            foreach(Transform child in GraveContent)
            {
                Destroy(child.gameObject);
            }

            WedgePanel.SetActive(false);
        }
    }

    public void ShowGraveyard(List<NormanCard> cards)
    {
        if(!GraveyardShown)
        {
            List<NormanCard> newCards = new List<NormanCard>();

            foreach(NormanCard card in cards)
            {
                newCards.Add(card);
            }

            newCards.Reverse();

            GraveContent.sizeDelta = new Vector2(0, 1500);

            float boxPosition = 1100;
            float cardPos = 0;

            GraveyardPanel.SetActive(true);
            panelActive = true;

            foreach (NormanCard card in newCards)
            {
                GraveContent.sizeDelta = new Vector2(GraveContent.sizeDelta.x + boxPosition, GraveContent.sizeDelta.y);
            }

            foreach (NormanCard card in newCards)
            {
                GameObject currentCard = Instantiate(UICard, new Vector3(0,0,0), Quaternion.identity, GraveContent.transform);
                currentCard.transform.localPosition = new Vector3(500 + cardPos, 0, 0);
                Debug.Log(currentCard.transform.localPosition.x);
                Debug.Log(currentCard.transform.localPosition.y);

                cardPos += 1100;


                currentCard.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("CardImages/" + card.cardNumber);
                Text[] texts = currentCard.GetComponentsInChildren<Text>();

                

                if (card.type == "Leader" || card.type == "Character" || card.type == "Unit")
                {
                    texts[0].text = card.cost.ToString();
                    texts[1].text = card.startZeal.ToString();
                    texts[2].text = card.startMight.ToString();
                    texts[3].text = card.startHealth.ToString();
                
                    if (card.resources == 0)
                    {
                        texts[4].text = "";

                    }
                    else
                    {
                        texts[4].text = card.resources.ToString();
                    }
                    
                }
                else
                {
                    if (card.zeal == 0)
                    {
                        texts[1].text = "";

                    }
                    else
                    {
                        texts[1].text = card.startZeal.ToString();
                    }

                    if (card.might == 0)
                    {
                         texts[2].text = "";

                    }
                    else
                    {
                        texts[2].text = card.might.ToString();
                    }

                    if (card.health == 0)
                    {
                        texts[3].text = "";

                    }
                    else
                    {
                         texts[3].text = card.health.ToString();
                    }

                    if (card.resources == 0)
                    {
                        texts[4].text = "";

                    }
                    else
                    {
                        texts[4].text = card.resources.ToString();
                    }
                }
            }

            GraveyardShown = true;
        }
    }

    public void ShowWedge(WedgeScript wedge)
    {
        WedgePanel.SetActive(true);

        switch(wedge.wedgeNum)
        {
            case 0:
                WedgeImage.sprite = Resources.Load<Sprite>("CardImages/WedgeCardOne");
                break;
            case 1:
                WedgeImage.sprite = Resources.Load<Sprite>("CardImages/WedgeCardTwo");
                break;
            case 2:
                WedgeImage.sprite = Resources.Load<Sprite>("CardImages/WedgeCardThree");
                break;

        }

        if (wedge.NormanDamage != 0)
        {
            NormanDamage.text = wedge.NormanDamage.ToString();
        }
        else
        {
            NormanDamage.text = "";
        }
        if (wedge.SaxonDamage != 0)
        {
            SaxonDamage.text = wedge.SaxonDamage.ToString();
        }
        else
        {
            SaxonDamage.text = "";
        }

        panelActive = true;

    }

}
