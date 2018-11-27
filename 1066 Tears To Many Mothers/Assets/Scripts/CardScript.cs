using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardScript : MonoBehaviour
{
    private Vector3[] dropPoints = new Vector3[] { new Vector3(300,0,326), new Vector3(0,0,326), new Vector3(-300,0,326),
                                           new Vector3(300,0,663), new Vector3(0,0,663), new Vector3(-300,0,663),
                                           new Vector3(300,0,1000), new Vector3(0,0,1000), new Vector3(-300,0,1000)};//Vector of drop points for the card to clip onto.

    private Vector3 cardPosition; //used to store the current card position;=.
    private Vector3 originalCardPosition; //used to store the original card position.
    private Vector3 boxPos; //used to store position of the box collider attached to this gameobject.
    private Vector3 boxScale; //used to store the scale of the box collider attached to this gameobject.
    private Vector3 positionOfMouse; //used to ustore position of the mouse in terms of vectore 3.

    private int clickCount = 0; //limiter for certain functions.

    public GameObject panel; //references to ui;
    public Image sprite;
    public Text nameTraitType;
    public Text costZeal;
    public Text mightHealthResources;
    public Text abilities;
    public Text flavour;
    public Text cardNumber;
    public NormanCard card; //gets the norman card scriptable object attached to this gameobject.

    private bool dragging = false; // bools for setting conditions.
    private bool placed = false;
    private bool tired = false;
    private BoxCollider thisCollider; //variable for the box collider attached to this gameobject.


    string[] dropPlaceholders = new string[] {"Norman11", "Norman12", "Norman13", "Norman21", "Norman22", "Norman23", "Norman31", "Norman32", "Norman33", };

    // Use this for initialization
    void Start ()
    {
        thisCollider = gameObject.GetComponent<BoxCollider>(); //set thisCollider to reference the collider attached to this gameObject.
        cardPosition = gameObject.transform.localPosition; //set card position.
        if (panel != null) //if there is a panel gameobject set it to false.
        {
            panel.gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    private void OnMouseEnter()
    {
        originalCardPosition = transform.position;//sets the original card position(this will run after adjustPos function in the hand).
        if (clickCount == 0 && dragging == false)//conditions to run.
        {
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

    private void OnMouseExit()
    {
        if (clickCount == 0 && dragging == false)//conditions to run.
        {
            boxPos = new Vector3(thisCollider.center.x, thisCollider.center.y + 150, thisCollider.center.z);//used to modify position of the box collider.
            boxScale = new Vector3(thisCollider.size.x, thisCollider.size.y / 2, thisCollider.size.z);//used to modify scale of the box collider.
            thisCollider.center = boxPos;//move the collider by boxPos vector.
            thisCollider.size = boxScale;//scale the collider by boxscale vector.
            transform.position = originalCardPosition;//move to originalcardposition.
        }
    }

    private void OnMouseDown()
    {
        if (placed)//if the card is placed on the field;
        {
            clickCount++;//counts the amount of clicks.
            if (clickCount == 1)//if this is the first click.
            {
                if (panel != null)//if panel exists set it to true and set the different ui elements (based off of car parameters).
                {
                    panel.gameObject.SetActive(true);
                    sprite.sprite = card.image;
                    nameTraitType.text = card.name + ", " + card.traits + ", " + card.type;
                    costZeal.text = "Cost: " + card.cost.ToString() + ", Zeal: " + card.zeal.ToString();
                    mightHealthResources.text = "Might: " + card.might.ToString() + ", Health: " + card.health.ToString() + ", Resources: " + card.resources.ToString();
                    abilities.text = card.abilities;
                    flavour.text = card.flavour;
                    cardNumber.text = "Card Number: " + card.cardNumber.ToString();
                }
            }
            else//if this is not the first click.
            {
                if (panel != null)//if panel exists set it to false.
                {
                    panel.gameObject.SetActive(false);
                }
                clickCount = 0;//resets click count to loop this function.
            }
        }
    }

    public void OnMouseDrag()
    {
        getPointer();//runs get point function.
        if (!placed)//if the card hasnt been placed condition.
        {
            dragging = true;//adjust this condition to limit other functions.
            float distance = 1100;//distance.
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);//set mouse position to be equal to the mouse.
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = objPosition;//set the position of this gameobject equal to obj position.
            thisCollider.enabled = false;//disable the collider.
        }

        //Glowing placeholders (On)
        GameObject obj;//sets a variable for storing gameobject references.
        Component[] outlines;//sets an array for storing component references.
        foreach (string name in dropPlaceholders)//goes through each string in the array.
        {
            obj = GameObject.Find(name);//sets variable to the game object controlling the outline .
            outlines = obj.GetComponentsInChildren<MeshRenderer>(); //adds each "outline" to an array

            foreach (Component outline in outlines)
            {
                outline.GetComponent<MeshRenderer>().enabled = true; //enables all outlines
            }
        }
    }

    public void OnMouseUp()
    {
        int Count = 0;//count to see how many points we have ran through.
        thisCollider.enabled = true;//re enamble the collider.
        foreach (Vector3 point in dropPoints)//for each vector3 in the array.
        {
            if (positionOfMouse.x < point.x + 99 && positionOfMouse.x > point.x - 99 && positionOfMouse.z < point.z + 167 && positionOfMouse.z > point.z - 167)//check its in the correct parameters.
            {
                if (!placed)//checks that the card isnt placed.
                {
                    Vector3 dropPosition = new Vector3(point.x, point.y + 2, point.z);//sets the drop position.
                    transform.position = dropPosition;//move the card to the drop position.
                    Vector3 Rotation = new Vector3(-50, 0, 0);//creat a vector to rotate.
                    transform.Rotate(Rotation);//rotate the card.
                    placed = true;//the card should now be placed so set placed to true.this will enable / disable certain functions based off conditions.
                    boxPos = new Vector3(thisCollider.center.x, thisCollider.center.y + 150, thisCollider.center.z);//used to modify position of the box collider.
                    boxScale = new Vector3(thisCollider.size.x, thisCollider.size.y / 2, thisCollider.size.z);//used to modify scale of the box collider.
                    thisCollider.center = boxPos;//move the collider by boxPos vector.
                    thisCollider.size = boxScale;//scale the collider by boxscale vector.
                    gameObject.transform.SetParent(null);//take this card out of the hand.
                }
            }
            else
            {
                Count++;//add to count.
            }
        }
        if (Count == dropPoints.Length)//if count is equal to the number of vectors in the array.
        {
            transform.position = originalCardPosition;//send the card back to the hand.
            dragging = false;//set dragging to be false.
        }


        //Glowing placeholders (On)
        GameObject obj;//sets a variable for storing gameobject references.
        Component[] outlines;//sets an array for storing component references.
        foreach (string name in dropPlaceholders)//goes through each string in the array.
        {
            obj = GameObject.Find(name);//sets variable to the game object controlling the outline .
            outlines = obj.GetComponentsInChildren<MeshRenderer>(); //adds each "outline" to an array

            foreach (Component outline in outlines)
            {
                outline.GetComponent<MeshRenderer>().enabled = false; //disables all outlines
            }
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))//on right click.
        {
            if (placed)//if the card is placed.
            {
                if (!tired)//if the card isnt already tired.
                {
                    Vector3 Rotation = new Vector3(0, 0, 90);//vector to rotate.
                    transform.Rotate(Rotation);//rotate by the vector.
                    tired = true;//set tired to true.
                }
                else if (tired) //stops the card turning 360, this way it goes flipped and back to normal
                {
                    Vector3 Rotation = new Vector3(0, 0, -90);//vector to rotate.
                    transform.Rotate(Rotation);//rotate by the vector.
                    tired = false;//card is no longer tired.
                }
            }
        }
    }

    private void getPointer()
    {
        RaycastHit hit;//creats a raycasthit.
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))//reads the mouse position and transfers it to a ray point, outputs that into hit.
        {
            positionOfMouse = hit.point; //the endpoint of the raycast corresponds to mouse position relative to terrain
        }
    }
}
