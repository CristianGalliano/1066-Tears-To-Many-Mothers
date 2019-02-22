using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivesScript : MonoBehaviour
{
    public bool activate = false;
    public float decreaseHeight;
    private Vector3 scale;
    private Vector3 oPosition;
    private Vector3 mPosition;
    public int objNum = 0;
    private Renderer rend;

    public Material[] mats;
    // Start is called before the first frame update
    void Start()
    {
        decreaseHeight = (gameObject.transform.localScale.z) / 7;
        oPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        rend = gameObject.GetComponent<Renderer>();
        changeMat();
    }

    // Update is called once per frame
    void Update()
    {
        if (activate == true)
        {
            nextCard();
            activate = false;
        }
    }

    void nextCard()
    {
        if (objNum <6)
        {
            objNum++;
            changeMat();
            scale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z - decreaseHeight);
            mPosition = new Vector3(oPosition.x, (scale.z / 33) / 2, oPosition.z);
            transform.localPosition = mPosition;
            transform.localScale = scale;
        }
    }

    private void changeMat()
    {
        Material[] temp = rend.materials;
        temp[0] = mats[objNum];
        rend.materials = temp;
    }
}
