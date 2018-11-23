using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Saxon Card", menuName = "Cards/Saxon Card")]
public class SaxonCard : ScriptableObject
{
    public new string name;
    public string traits;
    public string type;

    public int cost;
    public int zeal;
    public int might;
    public int health;
    public int resources;

    public string abilities;
    public string flavour;

    public int cardNumber;

}
