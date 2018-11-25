using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Norman Card", menuName = "Cards/Norman Card")]
public class NormanCard : ScriptableObject
{
    public Sprite image;

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
