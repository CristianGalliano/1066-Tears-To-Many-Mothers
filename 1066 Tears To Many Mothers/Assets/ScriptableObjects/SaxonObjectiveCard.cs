using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Saxon Objective Card", menuName = "Cards/Saxon Objective Card")]
public class SaxonObjectiveCard : ScriptableObject
{
    public new string name;

    public int battleAttribute;
    public int health;

    public char letter;

    public string abilities;
}
