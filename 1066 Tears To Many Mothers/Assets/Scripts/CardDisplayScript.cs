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

    public RectTransform GraveContent;
    public GameObject UICard;

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

        ObjHealth.text = card.health.ToString();



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

            foreach(Transform child in GraveContent)
            {
                Destroy(child.gameObject);
            }
        }
    }

    public void ShowGraveyard(List<NormanCard> cards)
    {
        if(!GraveyardShown)
        {
            List<NormanCard> newCards = cards;
            newCards.Reverse();

            float boxPosition = 1000;
            float cardPos = 0;

            GraveyardPanel.SetActive(true);
            panelActive = true;

            foreach (NormanCard card in newCards)
            {
                boxPosition += 1000;
            }

            GraveContent.right += new Vector3(GraveContent.right.x + boxPosition, GraveContent.right.y, GraveContent.right.z);

            foreach (NormanCard card in newCards)
            {
                Instantiate(UICard, new Vector3(GraveContent.transform.position.x + cardPos, GraveContent.transform.position.y - 300, GraveContent.transform.position.z), Quaternion.identity, GraveContent.transform);
                cardPos += 300;
            }

            GraveyardShown = true;
        }

    }

}
