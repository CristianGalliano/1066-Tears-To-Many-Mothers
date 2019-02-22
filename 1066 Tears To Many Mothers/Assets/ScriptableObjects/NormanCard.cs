using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Norman Card", menuName = "Cards/Norman Card")]
public class NormanCard : ScriptableObject
{
    public Sprite image;

    public int cardNumber;

    public new string name;
    public string title, type;

    public int cost, might, health, zeal, resources;
    public int startCost, startZeal, startMight, startHealth;

    public string action, constant, response, onPlay;

    public string quote;
    public string solo;

    public int PositionZ;
    public int lane;

    public void StartingValues()
    {
        startCost = cost;
        startZeal = zeal;
        startMight = might;
        startHealth = health;
    }


}
