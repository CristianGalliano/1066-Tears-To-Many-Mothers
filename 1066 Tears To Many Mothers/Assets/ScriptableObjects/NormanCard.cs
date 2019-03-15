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
    public bool needTarget = false;

    

    public void StartingValues()
    {
        startCost = cost;
        startZeal = zeal;
        startMight = might;
        startHealth = health;
        switch (cardNumber)
        {
            case 1:
                needTarget = true;
                break;
            case 2:
                needTarget = true;
                break;
            case 6:
                needTarget = true;
                break;
            case 8:
                needTarget = true;
                break;
            case 20:
                needTarget = true;
                break;
            case 22:
                needTarget = true;
                break;
            case 24:
                needTarget = true;
                break;
            case 31:
                needTarget = true;
                break;
            case 32:
                needTarget = true;
                break;
            case 33:
                needTarget = true;
                break;
            case 34:
                needTarget = true;
                break;
            case 37:
                needTarget = true;
                break;
            case 39:
                needTarget = true;
                break;
            case 43:
                needTarget = true;
                break;
            case 44:
                needTarget = true;
                break;
            case 45:
                needTarget = true;
                break;
            case 46:
                needTarget = true;
                break;
            case 47:
                needTarget = true;
                break;
            case 49:
                needTarget = true;
                break;
            case 50:
                needTarget = true;
                break;
            case 55:
                needTarget = true;
                break;
            case 56:
                needTarget = true;
                break;
            case 57:
                needTarget = true;
                break;
            case 58:
                needTarget = true;
                break;
            case 59:
                needTarget = true;
                break;
            case 60:
                needTarget = true;
                break;
            case 61:
                needTarget = true;
                break;
            case 64:
                needTarget = true;
                break;
            case 68:
                needTarget = true;
                break;
            case 69:
                needTarget = true;
                break;
            case 76:
                needTarget = true;
                break;
            case 85:
                needTarget = true;
                break;
            case 86:
                needTarget = true;
                break;
            case 91:
                needTarget = true;
                break;
            case 101:
                needTarget = true;
                break;
            case 102:
                needTarget = true;
                break;
            case 104:
                needTarget = true;
                break;
            case 110:
                needTarget = true;
                break;
            case 112:
                needTarget = true;
                break;
            case 113:
                needTarget = true;
                break;
            case 114:
                needTarget = true;
                break;
            case 119:
                needTarget = true;
                break;
            case 120:
                needTarget = true;
                break;
            case 121:
                needTarget = true;
                break;
            case 127:
                needTarget = true;
                break;
            case 128:
                needTarget = true;
                break;
            case 129:
                needTarget = true;
                break;
            case 130:
                needTarget = true;
                break;
            case 133:
                needTarget = true;
                break;
            case 134:
                needTarget = true;
                break;
            case 135:
                needTarget = true;
                break;
            case 136:
                needTarget = true;
                break;
            case 137:
                needTarget = true;
                break;
            case 138:
                needTarget = true;
                break;
            case 139:
                needTarget = true;
                break;
            case 141:
                needTarget = true;
                break;
            case 142:
                needTarget = true;
                break;
            case 146:
                needTarget = true;
                break;
            case 147:
                needTarget = true;
                break;
            case 148:
                needTarget = true;
                break;
            case 150:
                needTarget = true;
                break;
            case 151:
                needTarget = true;
                break;
            case 152:
                needTarget = true;
                break;
            case 153:
                needTarget = true;
                break;
            case 155:
                needTarget = true;
                break;
            case 156:
                needTarget = true;
                break;
            case 157:
                needTarget = true;
                break;
            case 158:
                needTarget = true;
                break;               
        }
    }
}
