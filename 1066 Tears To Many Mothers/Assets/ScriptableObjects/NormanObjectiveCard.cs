using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Norman Objective Card", menuName = "Cards/Norman Objective Card")]
public class NormanObjectiveCard : ScriptableObject
{
    public int cardNumber;
    public string idChar;

    public new string name;
    public string title;
    public string type;

    public int zeal;
    public int might;
    public int health;

    public string ability;

    public string quote;
    public string solo;
}
