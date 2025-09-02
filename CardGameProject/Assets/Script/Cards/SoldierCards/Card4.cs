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
        attackMode = "Զ��";
        attackAttribute = "����";
        actionMode = "׷��";
        effectText.text = $"{attackMode},{attackAttribute},{actionMode}";
        Name.text = "���񹭼���";
    }
}
