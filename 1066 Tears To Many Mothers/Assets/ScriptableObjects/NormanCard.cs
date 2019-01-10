using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Norman Card", menuName = "Cards/Norman Card")]
public class NormanCard : ScriptableObject
{
    public Sprite image;

    public int cardNumber;

    public new string name;
    public string title;
    public string type;

    public int cost;
    public int zeal;
    public int might;
    public int health;
    public int resources;

    public string action;
    public string constant;
    public string response;
    public string onPlay;

    public string quote;

    public string solo;


}
