using UnityEngine;
using UnityEngine.UI;

public class Card1 : SoldierCardBase
{
    public override void Start()
    {
        base.Start();
        cost = 5;
        ATK = 5;
        DEF = 10;
        HP = 10;
        AGI = 5;
        attackRange = 2f;
        attackMode = "近战";
        attackAttribute = "物理";
        actionMode = "追击";
        effectText.text = $"{attackMode},{attackAttribute},{actionMode}";
        Name.text = "士兵";
    }
}
