using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveyardScripts : MonoBehaviour
{
    private CardFucntionScript funcScript;
    public List<NormanCard> UsedCards;
    public Renderer rend;

    public DrawScript deck;
    private Vector3 scale;
    private Vector3 oPosition;
    private Vector3 mPosition;

    public bool activated = false;

    // Start is called before the first frame update
    void Start()
    {
        funcScript = GameObject.Find("GameController").GetComponent<CardFucntionScript>();
        rend = gameObject.GetComponent<Renderer>();
        oPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    void Update()
    {
        increaseSize();
    }

    public void increaseSize()
    {
        scale = new Vector3(transform.localScale.x, transform.localScale.y, deck.decreaseHeight * (UsedCards.Count));
        mPosition = new Vector3(oPosition.x, (scale.z / 60), oPosition.z);
        transform.localPosition = mPosition;
        transform.localScale = scale;
    }

    public void sendToGrave(NormanCard card)
    {
        UsedCards.Add(card);       
        Material mat = Resources.Load<Material>("CardImages/Materials/" + card.cardNumber);
        changeMat(mat);
    }

    private void changeMat(Material mat)
    {    
        Material[] temp = rend.materials;
        temp[0] = mat;
        rend.materials = temp;
    }
}
