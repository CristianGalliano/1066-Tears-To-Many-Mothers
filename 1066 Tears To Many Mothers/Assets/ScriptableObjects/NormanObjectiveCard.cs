using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Norman Objective Card", menuName = "Cards/Norman Objective Card")]
public class NormanObjectiveCard : ScriptableObject
{
    public new string name;

    public int battleAttribute;
    public int health;

    public char letter;

    public string abilities;
}
