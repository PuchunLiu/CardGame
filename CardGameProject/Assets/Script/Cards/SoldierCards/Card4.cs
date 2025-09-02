using System.Xml.Linq;
using UnityEngine;

public class Card4 : SoldierCardBase
{
    public override void Start()
    {
        base.Start();
        cost = 10;
        ATK = 15;
        DEF = 10;
        HP = 30;
        AGI = 6;
        attackRange = 6f;
        attackMode = "远程";
        attackAttribute = "物理";
        actionMode = "追击";
        effectText.text = $"{attackMode},{attackAttribute},{actionMode}";
        Name.text = "精锐弓箭手";
    }
}
