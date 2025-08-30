using UnityEngine;

public class Card2 : SoldierCardBase
{
    public override void Start()
    {
        base.Start();
        cost = 5;
        ATK = 8;
        DEF = 5;
        HP = 10;
        AGI = 4;
        attackRange = 4f;
        attackMode = "远程";
        attackAttribute = "物理";
        actionMode = "追击";
        effectText.text = $"{attackMode},{attackAttribute},{actionMode}";
        Name.text = "弓箭手";
    }
}
