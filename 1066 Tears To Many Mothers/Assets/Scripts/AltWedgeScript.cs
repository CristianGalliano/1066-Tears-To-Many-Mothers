using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltWedgeScript : MonoBehaviour
{
    public WedgeScript parentWedge, thisWedge;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        thisWedge.NormanDamage = parentWedge.NormanDamage;
        thisWedge.SaxonDamage = parentWedge.SaxonDamage;
    }
}
