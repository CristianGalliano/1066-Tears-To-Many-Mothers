using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawScript : MonoBehaviour
{
    public GameObject card;
    private Vector3 location = new Vector3(0, 200, 1300);
    private Vector3 scale;
    private Vector3 oPosition;
    private Vector3 mPosition;
    Quaternion qrotation = Quaternion.Euler(-40, 0, 0);

    float decreaseHeight;

    // Use this for initialization
    void Start ()
    {
        decreaseHeight = (gameObject.transform.localScale.y) / 76 ;
        oPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnMouseDown()
    {
        if (transform.localScale.y != 0)
        {
            scale = new Vector3(transform.localScale.x, transform.localScale.y - decreaseHeight, transform.localScale.z);
            mPosition = new Vector3(oPosition.x, scale.y / 2, oPosition.z);
            Instantiate(card, location, qrotation);
            transform.localPosition = mPosition;
            transform.localScale = scale;
        }
    }


}
