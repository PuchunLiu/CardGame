using System.Xml.Linq;
using UnityEngine;

public class Card3 : SoldierCardBase
{
    public override void Start()
    {
        base.Start();
        cost = 10;
        ATK = 10;
        DEF = 15;
        HP = 30;
        AGI = 7;
        attackRange = 2f;
        attackMode = "近战";
        attackAttribute = "物理";
        actionMode = "追击";
        effectText.text = $"{attackMode},{attackAttribute},{actionMode}";
        Name.text = "精锐士兵";
    }
}
