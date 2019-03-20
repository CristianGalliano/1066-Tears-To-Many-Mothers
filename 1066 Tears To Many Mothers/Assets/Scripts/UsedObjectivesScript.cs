using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsedObjectivesScript : MonoBehaviour
{
    public ObjectivesScript objective;
    private Vector3 scale;
    private Vector3 oPosition;
    private Vector3 mPosition;
    // Start is called before the first frame update
    void Start()
    {       
        oPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);        
    }

    // Update is called once per frame
    void Update()
    {
        if (objective.objNum < 1)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    public void increaseSize()
    {
        scale = new Vector3(transform.localScale.x, transform.localScale.y, objective.decreaseHeight * (objective.objNum + 1));
        mPosition = new Vector3(oPosition.x, (scale.z / 60), oPosition.z);
        transform.localPosition = mPosition;
        transform.localScale = scale;
    }
}
