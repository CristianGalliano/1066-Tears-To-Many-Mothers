using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardScript : MonoBehaviour
{
    Vector3[] dropPoints = new Vector3[] { new Vector3(300,0,326), new Vector3(0,0,326), new Vector3(-300,0,326), new Vector3(300,0,663), new Vector3(0,0,663), new Vector3(-300,0,663), new Vector3(300,0,1000), new Vector3(0,0,1000), new Vector3(-300,0,1000)};
    public Vector3 cardPosition;
    public Vector3 originalCardPosition;
    public Vector3 lastPosition;
    public Vector3 boxPos;
    private int clickCount = 0;
    public GameObject panel;
    public Image sprite;
    public Text nameTraitType;
    public Text costZeal;
    public Text mightHealthResources;
    public Text abilities;
    public Text flavour;
    public Text cardNumber;
    public NormanCard card;
    private bool dragging = false;
    bool placed = false;
    bool tired = false;
    public BoxCollider thisCollider;
    public Vector3 boxScale;
    private Vector3 positionOfMouse;

    string[] dropPlaceholders = new string[] {"Norman11", "Norman12", "Norman13", "Norman21", "Norman22", "Norman23", "Norman31", "Norman32", "Norman33", };
    // Use this for initialization
    void Start ()
    {
        thisCollider = gameObject.GetComponent<BoxCollider>();
        originalCardPosition = transform.position;
        cardPosition = gameObject.transform.localPosition;
        if (panel != null)
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
        originalCardPosition = transform.position;
        if (clickCount == 0 && dragging == false)
        {
            boxScale = new Vector3(thisCollider.size.x, thisCollider.size.y * 2, thisCollider.size.z);
            boxPos = new Vector3(thisCollider.center.x, thisCollider.center.y - 150, thisCollider.center.z);
            cardPosition.y = 500;
            cardPosition.z = 1300;
            cardPosition.x = originalCardPosition.x;
            thisCollider.center = boxPos;
            thisCollider.size = boxScale;
            gameObject.transform.position = cardPosition;
        }
    }

    private void OnMouseExit()
    {
        if (clickCount == 0 && dragging == false)
        {
            boxPos = new Vector3(thisCollider.center.x, thisCollider.center.y + 150, thisCollider.center.z);
            boxScale = new Vector3(thisCollider.size.x, thisCollider.size.y / 2, thisCollider.size.z);
            thisCollider.center = boxPos;
            thisCollider.size = boxScale;
            transform.position = originalCardPosition;
        }
    }

    private void OnMouseDown()
    {
        if (placed)
        {
            clickCount++;
            if (clickCount == 1)
            {
                if (panel != null)
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
            else
            {
                if (panel != null)
                {
                    panel.gameObject.SetActive(false);
                }
                clickCount = 0;
            }
        }
    }

    public void OnMouseDrag()
    {
        getPointer();
        if (!placed)
        {
            dragging = true;
            float distance = 1100;
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = objPosition;
            thisCollider.enabled = false;
        }

        //Glowing placeholders (On)
        if (!placed && dragging == true)
        {
            Behaviour halo;
            foreach (string obj in dropPlaceholders)
            {
                halo = (Behaviour)GameObject.Find(obj).GetComponent("Halo");
                halo.enabled = true;
            }
        }
    }

    public void OnMouseUp()
    {
        int Count = 0;
        thisCollider.enabled = true;
        foreach (Vector3 point in dropPoints)
        {
            if (positionOfMouse.x < point.x + 99 && positionOfMouse.x > point.x - 99 && positionOfMouse.z < point.z + 167 && positionOfMouse.z > point.z - 167)
            {
                if (!placed)
                {
                    Vector3 dropPosition = new Vector3(point.x, point.y + 2, point.z);
                    transform.position = dropPosition;
                    lastPosition = transform.position;
                    Vector3 Rotation = new Vector3(-50, 0, 0);
                    transform.Rotate(Rotation);
                    placed = true;
                    boxPos = new Vector3(thisCollider.center.x, thisCollider.center.y + 150, thisCollider.center.z);
                    boxScale = new Vector3(thisCollider.size.x, thisCollider.size.y / 2, thisCollider.size.z);
                    thisCollider.center = boxPos;
                    thisCollider.size = boxScale;
                    gameObject.transform.SetParent(null);
                }
            }
            else
            {
                Count++;
            }
        }
        if (Count == dropPoints.Length)
        {
            transform.position = originalCardPosition;
            dragging = false;
        }

        //Glowing placeholders (Off)
        Behaviour halo;
        foreach (string obj in dropPlaceholders)
        {
            halo = (Behaviour)GameObject.Find(obj).GetComponent("Halo");
            halo.enabled = false;
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (placed)
            {
                if (!tired)
                {
                    Vector3 Rotation = new Vector3(0, 0, 90);
                    transform.Rotate(Rotation);
                    tired = true;
                }
                else if (tired) //stops the card turning 360, this way it goes flipped and back to normal
                {
                    Vector3 Rotation = new Vector3(0, 0, -90);
                    transform.Rotate(Rotation);
                    tired = false;
                }
            }
        }
    }

    private void getPointer()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            positionOfMouse = hit.point; //the endpoint of the raycast corresponds to mouse position relative to terrain
            Debug.Log(positionOfMouse);
        }
        else
        {
            Debug.Log("cannot find terrain");
        }
    }

}
