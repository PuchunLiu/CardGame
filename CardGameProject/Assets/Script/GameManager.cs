using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class Card
{
    public string ID;
    public string Type;
    public string Name;
    public string Effect;
    public string AttackAttribute;
    public string ActionMode;
    public int AttackRange;
    public int Cost;
    public int ATK;
    public int DEF;
    public int HP;
    public int AGI;
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;
    public bool isSolo = true;
    public bool embattle = false;
    public Dictionary<string, Card> SoldierCards = new Dictionary<string, Card>();
    public Dictionary<string, Card> ItemCards = new Dictionary<string, Card>();
    public Dictionary<string, Card> SpellCards = new Dictionary<string, Card>();
    public GraphicRaycaster raycaster;

    private void Awake()
    {
        instance = this;
        string[] lines = File.ReadAllLines("Assets/cards.csv");
        for (int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i].Split(',');
            Card card = new Card();
            card.ID = values[0];
            card.Type = values[1];
            card.Name = values[2];
            card.Effect = values[3];
            card.AttackAttribute = values[4];
            card.ActionMode = values[5];
            card.AttackRange = int.Parse(values[6]);
            card.Cost = int.Parse(values[7]);
            card.ATK = int.Parse(values[8]);
            card.DEF = int.Parse(values[9]);
            card.HP = int.Parse(values[10]);
            card.AGI = int.Parse(values[11]);
            if (card.Type == "soldier")
            {
                SoldierCards.Add(card.ID, card);
            }
            else if (card.Type == "item")
            {
                ItemCards.Add(card.ID, card);
            }
            else if (card.Type == "spell")
            {
                SpellCards.Add(card.ID, card);
            }
        }
    }

    private void Start()
    {

    }

    public void BlockClicks()
    {
        raycaster.enabled = false;
    }

    public void UnblockClicks()
    {
        raycaster.enabled = true;
    }
}
