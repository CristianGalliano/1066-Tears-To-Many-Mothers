using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CardScript : MonoBehaviour
{

    private Vector3 cardPosition; //used to store the current card position;=.
    private Vector3 originalCardPosition; //used to store the original card position.
    private Vector3 boxPos; //used to store position of the box collider attached to this gameobject.
    private Vector3 boxScale; //used to store the scale of the box collider attached to this gameobject.
    private Vector3 positionOfMouse; //used to ustore position of the mouse in terms of vector3.

    private int clickCount = 0; //limiter for certain functions.
    int Count = 0;

    private bool dragging = false; // bools for setting conditions.
    private bool placed = false;
    public bool tired = false;
    private bool buttonPressed, timeRead, onPlayUsed = false;
    private BoxCollider thisCollider; //variable for the box collider attached to this gameobject.

    string[] NormanPlaceholders = new string[] { "Norman1", "Norman2", "Norman3" };
    string[] SaxonPlaceholders = new string[] { "Saxon1", "Saxon2", "Saxon3" };
    private GameObject[] placedCards;
    private GameController controller;
    private DeckManager deck;
    public CardDisplayScript UI;
    public NormanCard normanCard, saxonCard;
    private CardFucntionScript functScript;

    private MeshRenderer cardMesh;

    public TextMeshPro CostMesh, ZealMesh, MightMesh, HealthMesh, ResourceMesh;

    public float time;

    public int lane;
    public int laneNum;


    // Use this for initialization
    void Start()
    {
        //Initialising Script References
        controller = GameObject.Find("GameController").GetComponent<GameController>();
        deck = GameObject.Find("DeckManager").GetComponent<DeckManager>();
        UI = GameObject.Find("UIController").GetComponent<CardDisplayScript>();

        cardMesh = gameObject.GetComponent<MeshRenderer>();

        thisCollider = gameObject.GetComponent<BoxCollider>(); //set thisCollider to reference the collider attached to this gameObject.
        cardPosition = gameObject.transform.localPosition; //set card position.

        if (gameObject.tag == "Norman" && deck.NormanLeaderDrawn == true)
        {
            normanCard = deck.DrawRandomNormanCard();
            normanCard.StartingValues();
        }

        if (gameObject.tag == "Saxon" && deck.SaxonLeaderDrawn == true)
        {
            saxonCard = deck.DrawRandomSaxonCard();
            saxonCard.StartingValues();
        }

        if (gameObject.tag == "Norman" && deck.NormanLeaderDrawn == false)
        {
            normanCard = deck.DrawNormanCard(1);
            deck.NormanLeaderDrawn = true;
            Debug.Log("Leader Drawn : " + normanCard.name);
            normanCard.StartingValues();
        }

        if (gameObject.tag == "Saxon" && deck.SaxonLeaderDrawn == false)
        {
            saxonCard = deck.DrawSaxonCard(85);
            deck.SaxonLeaderDrawn = true;
            Debug.Log("Leader Drawn : " + saxonCard.name);
            saxonCard.StartingValues();
        }
        functScript = controller.GetComponent<CardFucntionScript>();
        AssignImage();
    }

    // Update is called once per frame
    void Update()
    {
        Destroy();
        GetPosition();
        if(placed)
        {
            Reposition();
        }

        if (controller.turn == 0 && gameObject.tag == "Saxon")
        {
            readyCard();           
        }
        if (controller.turn == 1 && gameObject.tag == "Norman")
        {
            readyCard();
        }

        UpdateStats();
        ShowUI();

        if(gameObject.tag == "Norman")
        {
            normanCard.lane = lane;
        }
        else if(gameObject.tag == "Saxon")
        {
            saxonCard.lane = lane;
        }
    }

    private void OnMouseEnter()
    {
        hover();
    }

    private void OnMouseExit()
    {
        if (clickCount == 0 && dragging == false)//conditions to run.
        {
            hoverEnd("Norman");
            hoverEnd("Saxon");
        }
    }

    private void OnMouseDown()
    {
        if (functScript.targeting && placed)
        {
            if (gameObject.tag == "Norman")
            {
                functScript.targetScript = this;
                functScript.target = normanCard;
            }
            else if (gameObject.tag == "Saxon")
            {
                functScript.targetScript = this;
                functScript.target = saxonCard;
            }
        }
        else if (!functScript.targeting && placed && !tired)
        {
            if (gameObject.tag == "Norman")
            {
                functScript.attackerScript = this;
                functScript.attacker = normanCard;
            }
            else if (gameObject.tag == "Saxon")
            {
                functScript.attackerScript = this;
                functScript.attacker = saxonCard;
            }
        }

        buttonPressed = true;
    }

    public void OnMouseDrag()
    {
        getPointer();//runs get point function.
        dragCard();
        enablePlaceholders();
    }

    public void OnMouseUp()
    {
        buttonPressed = false;
        UI.HideDisplay();

        thisCollider.enabled = true;//re enamble the collider.
        dropCard("Norman", controller.normanDropPointsZ, controller.normanLane, 0);
        dropCard("Saxon", controller.saxonDropPointsZ, controller.saxonlane, 1);
        disablePlacholders();
    }

    private void getPointer()
    {
        RaycastHit hit;//creats a raycasthit.
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))//reads the mouse position and transfers it to a ray point, outputs that into hit.
        {
            positionOfMouse = hit.point; //the endpoint of the raycast corresponds to mouse position relative to terrain
        }
    }

    public void tireCard()
    {
        if (placed)//if the card is placed.
        {
            if (!tired)//if the card isnt already tired.
            {
                if (gameObject.tag == "Norman" && controller.turn == 0)
                {
                    Vector3 Rotation = new Vector3(0, 0, 90);//vector to rotate.
                    transform.Rotate(Rotation);//rotate by the vector.
                    tired = true;//set tired to true.
                    //Debug.Log("norman resources: " + controller.normanResources);
                }
                else if (gameObject.tag == "Saxon" && controller.turn == 1)
                {
                    Vector3 Rotation = new Vector3(0, 0, 90);//vector to rotate.
                    transform.Rotate(Rotation);//rotate by the vector.
                    tired = true;//set tired to true.
                    //Debug.Log("saxon resources: " + controller.saxonResources);
                }
            }
        }
    }

    void dropCard(string str, List<int> List, List<int> count, int turnnum)
    {
        if (gameObject.tag == str)
        {
            if (controller.turn == turnnum)
            {
                foreach (int point in controller.xPositions)//for each vector3 in the array.
                {
                    if (positionOfMouse.x < point + 147 && positionOfMouse.x > point - 147)//check its in the correct parameters.
                    {
                        int index = controller.xPositions.IndexOf(point);
                        if (!placed && count[index] < 3)//checks that the card isnt placed.
                        {
                            Vector3 dropPosition = new Vector3(point, 2, List[count[index]]);//sets the drop position.
                            lane = index;
                            transform.position = dropPosition;//move the card to the drop position.
                            Vector3 Rotation = new Vector3(-50, 0, 0);//creat a vector to rotate.
                            transform.Rotate(Rotation);//rotate the card.
                            placed = true;//the card should now be placed so set placed to true.this will enable / disable certain functions based off conditions.
                            boxPos = new Vector3(thisCollider.center.x, thisCollider.center.y + 150, thisCollider.center.z);//used to modify position of the box collider.
                            boxScale = new Vector3(thisCollider.size.x, thisCollider.size.y / 2, thisCollider.size.z);//used to modify scale of the box collider.
                            thisCollider.center = boxPos;//move the collider by boxPos vector.
                            thisCollider.size = boxScale;//scale the collider by boxscale vector.
                            GameObject targetParent = GameObject.Find(str + "Field");
                            gameObject.transform.SetParent(targetParent.transform);//take this card out of the hand and put it as a child of the field
                            count[index]++;
                        }
                    }
                    else
                    {
                        Count++;//add to count.
                    }
                }
            }

            if (!placed)//if the card hasnt been placed, return position
            {
                transform.position = originalCardPosition;//send the card back to the hand.
                dragging = false;//set dragging to be false.
                boxScale = new Vector3(thisCollider.size.x, thisCollider.size.y / 2, thisCollider.size.z);
                boxPos = new Vector3(thisCollider.center.x, thisCollider.center.y + 150, thisCollider.center.z);
                thisCollider.center = boxPos;//move the collider by boxPos vector.
                thisCollider.size = boxScale;//scale the collider by boxscale vector.
            }
        }

        if (placed)
        {
            gameObject.layer = 1;
            FindPosition();
        }

        if(gameObject.tag == "Norman" && placed && !onPlayUsed)
        {
            if(normanCard.onPlay != "No OnPlay Ability")
            {
                functScript.onPlayAttacker = normanCard;
                functScript.attackerScript = this;
                functScript.targeting = true;
                onPlayUsed = true;
            }
        }

        if (gameObject.tag == "Saxon" && placed && !onPlayUsed)
        {
            if (saxonCard.onPlay != "No OnPlay Ability")
            {
                functScript.onPlayAttacker = saxonCard;
                functScript.attackerScript = this;
                functScript.targeting = true;
                onPlayUsed = true;
            }
        }
    }

    void dragCard()
    {
        if (!placed)//if the card hasnt been placed condition.
        {
            dragging = true;//adjust this condition to limit other functions.
            if (gameObject.tag == "Norman")
            {

                float distance = 1100;//distance.
                Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);//set mouse position to be equal to the mouse.
                Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                transform.position = objPosition;//set the position of this gameobject equal to obj position.
                thisCollider.enabled = false;//disable the collider.
            }
            else if (gameObject.tag == "Saxon")
            {
                float distance = 1100;//distance.
                Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);//set mouse position to be equal to the mouse.
                Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                transform.position = objPosition;//set the position of this gameobject equal to obj position.
                thisCollider.enabled = false;//disable the collider.
            }
        }
    }

    void enablePlaceholders()
    {
        if (!placed)
        {
            if (gameObject.tag == "Saxon")
            {
                GameObject obj;//sets a variable for storing gameobject references.
                Component[] outlines;//sets an array for storing component references.
                foreach (string name in SaxonPlaceholders)//goes through each string in the array.
                {
                    obj = GameObject.Find(name);//sets variable to the game object controlling the outline .
                    outlines = obj.GetComponentsInChildren<MeshRenderer>(); //adds each "outline" to an array                                                       

                    foreach (Component outline in outlines)
                    {
                        if (positionOfMouse.x < outline.transform.position.x + 147 && positionOfMouse.x > outline.transform.position.x - 147)//check its in the correct parameters.
                        {
                            outline.GetComponent<MeshRenderer>().enabled = true; //enables all outlines
                        }
                        else
                        {
                            outline.GetComponent<MeshRenderer>().enabled = false; //enables all outlines
                        }
                    }
                }
            }
            else if (gameObject.tag == "Norman")
            {
                //Glowing placeholders (On)
                GameObject obj;//sets a variable for storing gameobject references.
                Component[] outlines;//sets an array for storing component references.
                foreach (string name in NormanPlaceholders)//goes through each string in the array.
                {
                    obj = GameObject.Find(name);//sets variable to the game object controlling the outline .
                    outlines = obj.GetComponentsInChildren<MeshRenderer>(); //adds each "outline" to an array

                    foreach (Component outline in outlines)
                    {
                        if (positionOfMouse.x < outline.transform.position.x + 147 && positionOfMouse.x > outline.transform.position.x - 147)//check its in the correct parameters.
                        {
                            outline.GetComponent<MeshRenderer>().enabled = true; //enables all outlines
                        }
                        else
                        {
                            outline.GetComponent<MeshRenderer>().enabled = false; //enables all outlines
                        }
                    }
                }
            }
        }
    }

    void disablePlacholders()
    {
        //Glowing placeholders (Off)
        GameObject obj;//sets a variable for storing gameobject references.
        Component[] outlines;//sets an array for storing component references.
        foreach (string name in NormanPlaceholders)//goes through each string in the array.
        {
            obj = GameObject.Find(name);//sets variable to the game object controlling the outline .
            outlines = obj.GetComponentsInChildren<MeshRenderer>(); //adds each "outline" to an array

            foreach (Component outline in outlines)
            {
                outline.GetComponent<MeshRenderer>().enabled = false; //disables all outlines
            }
        }
        foreach (string name in SaxonPlaceholders)//goes through each string in the array.
        {
            obj = GameObject.Find(name);//sets variable to the game object controlling the outline .
            outlines = obj.GetComponentsInChildren<MeshRenderer>(); //adds each "outline" to an array

            foreach (Component outline in outlines)
            {
                outline.GetComponent<MeshRenderer>().enabled = false; //disables all outlines
            }
        }
    }


    void hover()
    {
        if (gameObject.tag == "Norman")
        {
            if (clickCount == 0 && dragging == false)//conditions to run.
            {
                originalCardPosition = transform.position;//sets the original card position(this will run after adjustPos function in the hand).
                boxScale = new Vector3(thisCollider.size.x, thisCollider.size.y * 2, thisCollider.size.z);//used to modify scale of the box collider.
                boxPos = new Vector3(thisCollider.center.x, thisCollider.center.y - 150, thisCollider.center.z);//used to modify position of the box collider.
                cardPosition.y = 500;//set card position axis.this determines where the card moves when hovered over.
                cardPosition.z = 1300;
                cardPosition.x = originalCardPosition.x;
                thisCollider.center = boxPos;//move the collider by boxPos vector.
                thisCollider.size = boxScale;//scale the collider by boxscale vector.
                gameObject.transform.position = cardPosition;//move to cardposition.
            }
        }
        else if (gameObject.tag == "Saxon")
        {

            if (clickCount == 0 && dragging == false)//conditions to run.
            {
                originalCardPosition = transform.position;//sets the original card position(this will run after adjustPos function in the hand).
                boxScale = new Vector3(thisCollider.size.x, thisCollider.size.y * 2, thisCollider.size.z);//used to modify scale of the box collider.
                boxPos = new Vector3(thisCollider.center.x, thisCollider.center.y - 150, thisCollider.center.z);//used to modify position of the box collider.
                cardPosition.y = 500;//set card position axis.this determines where the card moves when hovered over.
                cardPosition.z = -1300;
                cardPosition.x = originalCardPosition.x;
                thisCollider.center = boxPos;//move the collider by boxPos vector.
                thisCollider.size = boxScale;//scale the collider by boxscale vector.
                gameObject.transform.position = cardPosition;//move to cardposition.
            }
        }
    }

    void hoverEnd(string str)
    {
        if (gameObject.tag == str)
        {
            boxPos = new Vector3(thisCollider.center.x, thisCollider.center.y + 150, thisCollider.center.z);//used to modify position of the box collider.
            boxScale = new Vector3(thisCollider.size.x, thisCollider.size.y / 2, thisCollider.size.z);//used to modify scale of the box collider.
            thisCollider.center = boxPos;//move the collider by boxPos vector.
            thisCollider.size = boxScale;//scale the collider by boxscale vector.
            transform.position = originalCardPosition;//move to originalcardposition.
        }
    }

    void readyCard()
    {
        if (tired) //stops the card turning 360, this way it goes flipped and back to normal
        {
            Vector3 Rotation = new Vector3(0, 0, -90);//vector to rotate.
            transform.Rotate(Rotation);//rotate by the vector.
            tired = false;//card is no longer tired.
        }
    }

    void UpdateStats()
    {
        if (gameObject.tag == "Norman")
        {
            CostMesh.text = normanCard.cost.ToString();

            if (normanCard.type == "Leader" || normanCard.type == "Character" || normanCard.type == "Unit")
            {
                ZealMesh.text = normanCard.zeal.ToString();
                MightMesh.text = normanCard.might.ToString();
                HealthMesh.text = normanCard.health.ToString();
            }
            else
            {
                if (normanCard.zeal == 0)
                {
                    ZealMesh.text = "";

                }
                else
                {
                    ZealMesh.text = normanCard.zeal.ToString();
                }

                if (normanCard.might == 0)
                {
                    MightMesh.text = "";

                }
                else
                {
                    MightMesh.text = normanCard.might.ToString();
                }

                if (normanCard.health == 0)
                {
                    HealthMesh.text = "";
                }
                else
                {
                    HealthMesh.text = normanCard.health.ToString();
                }
            }

            if (normanCard.resources == 0)
            {
                ResourceMesh.text = "";
            }
            else
            {
                ResourceMesh.text = normanCard.resources.ToString();
            }

            if (normanCard.cost == normanCard.startCost)
                CostMesh.color = Color.black;
            if (normanCard.zeal == normanCard.startZeal)
                ZealMesh.color = Color.black;
            if (normanCard.might == normanCard.startMight)
                MightMesh.color = Color.black;
            if (normanCard.health == normanCard.startHealth)
                HealthMesh.color = Color.black;

            if (normanCard.cost < normanCard.startCost)
                CostMesh.color = Color.red;
            if (normanCard.zeal < normanCard.startZeal)
                ZealMesh.color = Color.red;
            if (normanCard.might < normanCard.startMight)
                MightMesh.color = Color.red;
            if (normanCard.health < normanCard.startHealth)
                HealthMesh.color = Color.red;

            if (normanCard.cost > normanCard.startCost)
                CostMesh.color = Color.green;
            if (normanCard.zeal > normanCard.startZeal)
                ZealMesh.color = Color.green;
            if (normanCard.might > normanCard.startMight)
                MightMesh.color = Color.green;
            if (normanCard.health > normanCard.startHealth)
                HealthMesh.color = Color.green;
        }
        else if (gameObject.tag == "Saxon")
        {
            CostMesh.text = saxonCard.cost.ToString();

            if (saxonCard.type == "Leader" || saxonCard.type == "Character" || saxonCard.type == "Unit")
            {
                ZealMesh.text = saxonCard.zeal.ToString();
                MightMesh.text = saxonCard.might.ToString();
                HealthMesh.text = saxonCard.health.ToString();
            }
            else
            {
                if (saxonCard.zeal == 0)
                {
                    ZealMesh.text = "";

                }
                else
                {
                    ZealMesh.text = saxonCard.zeal.ToString();
                }

                if (saxonCard.might == 0)
                {
                    MightMesh.text = "";

                }
                else
                {
                    MightMesh.text = saxonCard.might.ToString();
                }

                if (saxonCard.health == 0)
                {
                    HealthMesh.text = "";
                }
                else
                {
                    HealthMesh.text = saxonCard.health.ToString();
                }
            }

            if (saxonCard.resources == 0)
            {
                ResourceMesh.text = "";
            }
            else
            {
                ResourceMesh.text = saxonCard.resources.ToString();
            }

            if (saxonCard.cost == saxonCard.startCost)
                CostMesh.color = Color.black;
            if (saxonCard.zeal == saxonCard.startZeal)
                ZealMesh.color = Color.black;
            if (saxonCard.might == saxonCard.startMight)
                MightMesh.color = Color.black;
            if (saxonCard.health == saxonCard.startHealth)
                HealthMesh.color = Color.black;

            if (saxonCard.cost < saxonCard.startCost)
                CostMesh.color = Color.red;
            if (saxonCard.zeal < saxonCard.startZeal)
                ZealMesh.color = Color.red;
            if (saxonCard.might < saxonCard.startMight)
                MightMesh.color = Color.red;
            if (saxonCard.health < saxonCard.startHealth)
                HealthMesh.color = Color.red;

            if (saxonCard.cost > saxonCard.startCost)
                CostMesh.color = Color.green;
            if (saxonCard.zeal > saxonCard.startZeal)
                ZealMesh.color = Color.green;
            if (saxonCard.might > saxonCard.startMight)
                MightMesh.color = Color.green;
            if (saxonCard.health > saxonCard.startHealth)
                HealthMesh.color = Color.green;
        }

    }

    void AssignImage()
    {
        if (tag == "Norman")
        {
            cardMesh.material = Resources.Load<Material>("CardImages/Materials/" + normanCard.cardNumber);
        }
        else if (tag == "Saxon")
        {
            cardMesh.material = Resources.Load<Material>("CardImages/Materials/" + saxonCard.cardNumber);
        }

    }

    void GetPosition()
    {
        int ZPos = (int)gameObject.transform.position.z;

        if(gameObject.tag == "Norman")
        {
            switch (ZPos)
            {
                case 750:
                    laneNum = 3;
                    break;
                case 500:
                    laneNum = 2;
                    break;
                case 250:
                    laneNum = 1;
                    break;
            }
        }

        if (gameObject.tag == "Saxon")
        {
            switch (ZPos)
            {
                case -750:
                    laneNum = 3;
                    break;
                case -500:
                    laneNum = 2;
                    break;
                case -250:
                    laneNum = 1;
                    break;
            }
        }

    }

    void FindPosition()
    {
        int ZPos = (int)gameObject.transform.position.z;

        switch(ZPos)
        {
            case 750:
                normanCard.PositionZ = 1;
                break;
            case 500:
                normanCard.PositionZ = 2;
                break;
            case 250:
                normanCard.PositionZ = 3;
                break;
            case -250:
                saxonCard.PositionZ = 4;
                break;
            case -500:
                saxonCard.PositionZ = 5;
                break;
            case -750:
                saxonCard.PositionZ = 6;
                break;
        }
    }

    void ShowUI()
    {
        if(buttonPressed && !timeRead && placed)
        {
            time = Time.time + 0.5f;
            timeRead = true;
        }

        if(buttonPressed && Time.time > time && timeRead && placed)
        {
            if (gameObject.tag == "Norman")
            {
                UI.SetDisplay(normanCard);
            }
            else if (gameObject.tag == "Saxon")
            {
                UI.SetDisplay(saxonCard);
            }

        }
        else if(!buttonPressed)
        {
            timeRead = false;
        }
    }

    public void Reposition()
    {
        if(gameObject.tag == "Norman")
        {
            GameObject[] cards = GameObject.FindGameObjectsWithTag("Norman");

            if (controller.normanLane[lane] == 1)
            {
                Vector3 newPos = transform.position;
                newPos.z = controller.normanDropPointsZ[0];
                transform.position = newPos;
            }
            else if(controller.normanLane[lane] == 2)
            {
                if(laneNum == 2)
                {
                    CardScript card1 = null;
                    foreach (GameObject card in cards)
                    {
                        CardScript thisCard = card.GetComponent<CardScript>();
                        if (thisCard != null && thisCard.lane == lane && thisCard.laneNum == 1)
                        {
                            card1 = thisCard;
                        }
                    }

                    if (card1 == null)
                    {
                        Vector3 newPos = transform.position;
                        newPos.z = controller.normanDropPointsZ[0];
                        transform.position = newPos;
                    }
                }

                if (laneNum == 3)
                {
                    CardScript card1 = null;
                    foreach (GameObject card in cards)
                    {
                        CardScript thisCard = card.GetComponent<CardScript>();
                        if (thisCard != null && thisCard.lane == lane && thisCard.laneNum == 2)
                        {
                            card1 = thisCard;
                        }
                    }

                    if (card1 == null)
                    {
                        Vector3 newPos = transform.position;
                        newPos.z = controller.normanDropPointsZ[1];
                        transform.position = newPos;
                    }
                }
            }
        }

        if (gameObject.tag == "Saxon")
        {
            GameObject[] cards = GameObject.FindGameObjectsWithTag("Saxon");

            if (controller.saxonlane[lane] == 1)
            {
                Vector3 newPos = transform.position;
                newPos.z = controller.saxonDropPointsZ[0];
                transform.position = newPos;
            }
            else if (controller.saxonlane[lane] == 2)
            {
                if (laneNum == 2)
                {
                    CardScript card1 = null;
                    foreach (GameObject card in cards)
                    {
                        CardScript thisCard = card.GetComponent<CardScript>();
                        if (thisCard != null && thisCard.lane == lane && thisCard.laneNum == 1)
                        {
                            card1 = thisCard;
                        }
                    }

                    if (card1 == null)
                    {
                        Vector3 newPos = transform.position;
                        newPos.z = controller.saxonDropPointsZ[0];
                        transform.position = newPos;
                    }
                }

                if (laneNum == 3)
                {
                    CardScript card1 = null;
                    foreach (GameObject card in cards)
                    {
                        CardScript thisCard = card.GetComponent<CardScript>();
                        if (thisCard != null && thisCard.lane == lane && thisCard.laneNum == 2)
                        {
                            card1 = thisCard;
                        }
                    }

                    if (card1 == null)
                    {
                        Vector3 newPos = transform.position;
                        newPos.z = controller.saxonDropPointsZ[1];
                        transform.position = newPos;
                    }
                }
            }
        }


    }

    private void Destroy()
    {
        if (gameObject.tag == "Norman")
        {
            if (normanCard.health <= 0 && normanCard.type == "Unit" || normanCard.health <= 0 && normanCard.type == "Character" || normanCard.health <= 0 && normanCard.type == "Leader")
            {
                controller.normanLane[lane] -= 1;
                Destroy(gameObject);
            }
        }
        if (gameObject.tag == "Saxon")
        {
            if (saxonCard.health <= 0 && saxonCard.type == "Unit" || saxonCard.health <= 0 && saxonCard.type == "Character" || saxonCard.health <= 0 && saxonCard.type == "Leader")
            {
                Debug.Log("should be dead!");
                controller.saxonlane[lane] -= 1;
                Destroy(gameObject);
            }
        }
    }
}
