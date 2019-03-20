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

    public string[] traits = new string[3] {"","",""};

    

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
                traits[0] = "Norman";
                traits[1] = "Duke";
                break;
            case 2:
                needTarget = true;
                traits[0] = "Norman";
                traits[1] = "Noble";
                break;
            case 3:
                traits[0] = "Breton";
                traits[1] = "Noble";
                break;
            case 4:
                traits[0] = "Saxon";
                traits[1] = "Clergy";
                break;
            case 5:
                traits[0] = "Flemish";
                traits[1] = "Noble";
                break;
            case 6:
                needTarget = true;
                traits[0] = "Norman";
                traits[1] = "Noble";
                break;
            case 7:
                traits[0] = "Norman";
                traits[1] = "Noble";
                break;
            case 8:
                needTarget = true;
                traits[0] = "Norman";
                traits[1] = "Noble";
                break;
            case 9:
                traits[0] = "Norman";
                traits[1] = "Noble";
                break;
            case 10:
                traits[0] = "Norman";
                traits[1] = "Noble";
                break;
            case 11:
                traits[0] = "Norman";
                traits[1] = "Clergy";
                break;
            case 12:
                traits[0] = "Norman";
                traits[1] = "Clergy";
                break;
            case 13:
                traits[0] = "Norman";
                traits[1] = "Noble";
                break;
            case 14:
                traits[0] = "Norman";
                traits[1] = "Noble";
                break;
            case 15:
                traits[0] = "Norman";
                traits[1] = "Clergy";
                break;
            case 16:
                traits[0] = "Norman";
                traits[1] = "Noble";
                break;
            case 17:
                traits[0] = "Norman";
                traits[1] = "Noble";
                break;
            case 18:
                traits[0] = "Norman";
                traits[1] = "Noble";
                break;
            case 19:
                traits[0] = "Norman";
                traits[1] = "Noble";
                break;
            case 20:
                needTarget = true;
                traits[0] = "Norman";
                traits[1] = "Noble";
                break;
            case 21:
                traits[0] = "Norman";
                traits[1] = "Noble";
                break;
            case 22:
                needTarget = true;
                traits[0] = "Norman";
                traits[1] = "Lunatic";
                break;
            case 23:
                traits[0] = "Norman";
                traits[1] = "Noble";
                break;
            case 24:
                needTarget = true;
                traits[0] = "Norman";
                traits[1] = "Noble";
                break;
            case 25:
                traits[0] = "Norman";
                traits[1] = "Noble";
                break;
            case 26:
                traits[0] = "Norman";
                traits[1] = "Noble";
                break;
            case 27:
                traits[0] = "Norman";
                traits[1] = "Clergy";
                break;
            case 28:
                traits[0] = "Norman";
                traits[1] = "Noble";
                break;
            case 29:
                traits[0] = "Norman";
                traits[1] = "Noble";
                break;
            case 30:
                traits[0] = "Norman";
                break;
            case 31:
                needTarget = true;
                traits[0] = "Norman";
                break;
            case 32:
                needTarget = true;
                traits[0] = "Norman";
                break;
            case 33:
                needTarget = true;
                traits[0] = "Norman";
                break;
            case 34:
                needTarget = true;
                traits[0] = "Norman";
                break;
            case 35:
                traits[0] = "Norman";
                break;
            case 36:
                traits[0] = "Norman";
                break;
            case 37:
                needTarget = true;
                traits[0] = "Norman";
                break;
            case 38:
                traits[0] = "Viking";
                traits[1] = "King";
                break;
            case 39:
                needTarget = true;
                traits[0] = "Norman";
                break;
            case 40:
                traits[0] = "Viking";
                break;
            case 41:
                traits[0] = "Scots";
                traits[1] = "King";
                break;
            case 42:
                traits[0] = "Norman";
                break;
            case 43:
                needTarget = true;
                traits[0] = "Viking";
                break;
            case 44:
                needTarget = true;
                traits[0] = "Norman";
                break;
            case 45:
                needTarget = true;
                traits[0] = "Norman";
                break;
            case 46:
                needTarget = true;
                traits[0] = "Norman";
                break;
            case 47:
                needTarget = true;
                traits[0] = "Norman";
                break;
            case 48:
                traits[0] = "Saxon";
                traits[1] = "Exile";
                break;
            case 49:
                needTarget = true;
                traits[0] = "Saxon";
                break;
            case 50:
                needTarget = true;
                traits[0] = "Norman";
                break;
            case 51:
                traits[0] = "Norman";
                break;
            case 52:
                traits[0] = "Hungarian";
                traits[1] = "Noble";
                break;
            case 53:
                traits[0] = "Norman";
                break;
            case 54:
                traits[0] = "Norman";
                break;
            case 55:
                needTarget = true;
                traits[0] = "Norman";
                break;
            case 56:
                needTarget = true;
                traits[0] = "Norman";
                break;
            case 57:
                needTarget = true;
                traits[0] = "Norman";
                break;
            case 58:
                needTarget = true;
                traits[0] = "Norman";
                break;
            case 59:
                needTarget = true;
                traits[0] = "Norman";
                break;
            case 60:
                needTarget = true;
                traits[0] = "Norman";
                traits[1] = "Archer";
                break;
            case 61:
                needTarget = true;
                traits[0] = "Breton";
                traits[1] = "Archer";
                break;
            case 62:
                traits[0] = "Breton";
                traits[1] = "Cavalry";
                break;
            case 63:
                traits[0] = "Breton";
                traits[1] = "Infantry";
                break;
            case 64:
                needTarget = true;
                traits[0] = "Flemish";
                traits[1] = "Archer";
                break;
            case 65:
                traits[0] = "Flemish";
                traits[1] = "Cavalry";
                break;
            case 66:
                traits[0] = "Flemish";
                traits[1] = "Infantry";
                break;
            case 67:
                traits[0] = "Norman";
                traits[1] = "Infantry";
                break;
            case 68:
                needTarget = true;
                traits[0] = "Norman";
                traits[1] = "Archer";
                break;
            case 69:
                needTarget = true;
                traits[0] = "Norman";
                traits[1] = "Archer";
                break;
            case 70:
                traits[0] = "Norman";
                traits[1] = "Infantry";
                break;
            case 71:
                traits[0] = "Norman";
                traits[1] = "Infantry";
                break;
            case 72:
                traits[0] = "Norman";
                traits[1] = "Cavalry";
                traits[2] = "Elite";
                break;
            case 73:
                traits[0] = "Norman";
                traits[1] = "Infantry";
                break;
            case 74:
                traits[0] = "Norman";
                traits[1] = "Cavalry";
                break;
            case 75:
                traits[0] = "Norman";
                traits[1] = "Cavalry";
                break;
            case 76:
                needTarget = true;
                traits[0] = "Norman";
                traits[1] = "Infantry";
                break;
            case 77:
                needTarget = true;
                traits[0] = "Norman";
                traits[1] = "Infantry";
                break;
            case 85:
                needTarget = true;
                traits[0] = "Saxon";
                traits[1] = "King";
                break;
            case 86:
                needTarget = true;
                traits[0] = "Saxon";
                traits[1] = "Noble";
                break;
            case 87:
                traits[0] = "Saxon";
                traits[1] = "Clergy";
                break;
            case 88:
                traits[0] = "Saxon";
                traits[1] = "Noble";
                break;
            case 89:
                traits[0] = "Saxon";
                traits[1] = "Clergy";
                break;
            case 90:
                traits[0] = "Saxon";
                traits[1] = "Noble";
                break;
            case 91:
                needTarget = true;
                traits[0] = "Saxon";
                traits[1] = "Noble";
                break;
            case 92:
                traits[0] = "Saxon";
                traits[1] = "Noble";
                break;
            case 93:
                traits[0] = "Saxon";
                traits[1] = "Sheriff";
                break;
            case 94:
                traits[0] = "Saxon";
                traits[1] = "Sheriff";
                break;
            case 95:
                traits[0] = "Saxon";
                traits[1] = "Noble";
                break;
            case 96:
                traits[0] = "Saxon";
                traits[1] = "Noble";
                break;
            case 97:
                traits[0] = "Saxon";
                traits[1] = "Clergy";
                break;
            case 98:
                traits[0] = "Saxon";
                traits[1] = "Noble";
                break;
            case 99:
                traits[0] = "Saxon";
                traits[1] = "Noble";
                break;
            case 100:
                traits[0] = "Saxon";
                traits[1] = "Clergy";
                break;
            case 101:
                needTarget = true;
                traits[0] = "Saxon";
                traits[1] = "Danish";
                traits[2] = "Noble";
                break;
            case 102:
                needTarget = true;
                traits[0] = "Saxon";
                traits[1] = "Noble";
                break;
            case 103:
                traits[0] = "Saxon";
                traits[1] = "Noble";
                break;
            case 104:
                needTarget = true;
                traits[0] = "Saxon";
                break;
            case 105:
                traits[0] = "Saxon";
                break;
            case 106:
                traits[0] = "Saxon";
                break;
            case 107:
                traits[0] = "Saxon";
                break;
            case 108:
                traits[0] = "Saxon";
                break;
            case 109:
                traits[0] = "Saxon";
                break;
            case 110:
                needTarget = true;
                traits[0] = "Saxon";
                break;
            case 111:
                traits[0] = "Saxon";
                break;
            case 112:
                needTarget = true;
                traits[0] = "Saxon";
                break;
            case 113:
                needTarget = true;
                traits[0] = "Saxon";
                break;
            case 114:
                needTarget = true;
                traits[0] = "Saxon";
                break;
            case 115:
                traits[0] = "Saxon";
                break;
            case 116:
                traits[0] = "Saxon";
                break;
            case 117:
                traits[0] = "Saxon";
                break;
            case 118:
                traits[0] = "Saxon";
                break;
            case 119:
                needTarget = true;
                traits[0] = "Saxon";
                break;
            case 120:
                needTarget = true;
                traits[0] = "Saxon";
                break;
            case 121:
                needTarget = true;
                traits[0] = "Saxon";
                break;
            case 122:
                traits[0] = "Saxon";
                break;
            case 123:
                traits[0] = "Saxon";
                break;
            case 124:
                traits[0] = "Saxon";
                break;
            case 125:
                traits[0] = "Saxon";
                break;
            case 126:
                needTarget = true;
                traits[0] = "Saxon";
                break;
            case 127:
                needTarget = true;
                traits[0] = "Saxon";
                break;
            case 128:
                needTarget = true;
                traits[0] = "Saxon";
                break;
            case 129:
                needTarget = true;
                traits[0] = "Saxon";
                break;
            case 130:
                needTarget = true;
                traits[0] = "Saxon";
                traits[1] = "Archer";
                traits[2] = "Elite";
                break;
            case 131:
                traits[0] = "Saxon";
                traits[1] = "Infantry";
                break;
            case 132:
                traits[0] = "Saxon";
                traits[1] = "Infantry";
                break;
            case 133:
                needTarget = true;
                traits[0] = "Saxon";
                traits[1] = "Infantry";
                traits[2] = "Elite";
                break;
            case 134:
                needTarget = true;
                traits[0] = "Saxon";
                traits[1] = "Infantry";
                traits[2] = "Elite";
                break;
            case 135:
                needTarget = true;
                traits[0] = "Saxon";
                traits[1] = "Infantry";
                traits[2] = "Elite";
                break;
            case 136:
                needTarget = true;
                traits[0] = "Saxon";
                traits[1] = "Infantry";
                traits[2] = "Militia";
                break;
            case 137:
                needTarget = true;
                traits[0] = "Saxon";
                traits[1] = "Infantry";
                traits[2] = "Elite";
                break;
            case 138:
                traits[0] = "Saxon";
                traits[1] = "Infantry";
                traits[2] = "Housecarl";
                break;
            case 139:
                traits[0] = "Saxon";
                traits[1] = "Infantry";
                traits[2] = "Housecarl";
                break;
            case 140:
                traits[0] = "Saxon";
                traits[1] = "Infantry";
                traits[2] = "Militia";
                break;
            case 141:
                needTarget = true;
                traits[0] = "Saxon";
                traits[1] = "Archer";
                traits[2] = "Militia";
                break;
            case 142:
                needTarget = true;
                traits[0] = "Saxon";
                traits[1] = "Archer";
                traits[2] = "Militia";
                break;
            case 143:
                traits[0] = "Saxon";
                traits[1] = "Infantry";
                traits[2] = "Housecarl";
                break;
            case 144:
                traits[0] = "Saxon";
                traits[1] = "Infantry";
                traits[2] = "Housecarl";
                break;
            case 145:
                traits[0] = "Saxon";
                traits[1] = "Infantry";
                traits[2] = "Housecarl";
                break;
            case 146:
                needTarget = true;
                traits[0] = "Saxon";
                traits[1] = "Infantry";
                break;
            case 147:
                needTarget = true;
                traits[0] = "Saxon";
                traits[1] = "Infantry";
                traits[2] = "Militia";
                break;
            case 148:
                needTarget = true;
                traits[0] = "Saxon";
                traits[1] = "Archer";
                break;
            case 149:
                traits[0] = "Saxon";
                traits[1] = "Infantry";
                break;
            case 150:
                needTarget = true;
                traits[0] = "Danish";
                traits[1] = "Infantry";
                traits[2] = "Elite";
                break;
            case 151:
                needTarget = true;
                traits[0] = "Danish";
                traits[1] = "Infantry";
                traits[2] = "Elite";
                break;
            case 152:
                needTarget = true;
                traits[0] = "Saxon";
                traits[1] = "Infantry";
                traits[2] = "Militia";
                break;
            case 153:
                needTarget = true;
                traits[0] = "Saxon";
                traits[1] = "Infantry";
                traits[2] = "Elite";
                break;
            case 154:
                traits[0] = "Saxon";
                traits[1] = "Infantry";
                traits[2] = "Militia";
                break;
            case 155:
                needTarget = true;
                traits[0] = "Saxon";
                traits[1] = "Infantry";
                traits[2] = "Elite";
                break;
            case 156:
                needTarget = true;
                traits[0] = "Saxon";
                traits[1] = "Infantry";
                break;
            case 157:
                needTarget = true;
                traits[0] = "Saxon";
                traits[1] = "Infantry";
                break;
            case 158:
                needTarget = true;
                traits[0] = "Saxon";
                traits[1] = "Infantry";
                break;
            case 159:
                traits[0] = "Saxon";
                traits[1] = "Infantry";
                break;
            case 160:
                traits[0] = "Saxon";
                traits[1] = "Infantry";
                break;
            case 161:
                traits[0] = "Saxon";
                traits[1] = "Infantry";
                break;
        }
    }
}
