using UnityEngine;
using UnityEngine.UI;

public class SoldierCardBase : CardBase
{
    public Text costText;
    public Text ATKText;
    public Text DEFText;
    public Text HPText;
    public Text AGIText;

    private int _cost;
    private int _ATK;
    private int _DEF;
    private int _HP;
    private int _AGI;

    public event System.Action<int> OnCostChanged;
    public event System.Action<int> OnATKChanged;
    public event System.Action<int> OnDEFChanged;
    public event System.Action<int> OnHPChanged;
    public event System.Action<int> OnAGIChanged;

    public int cost
    {
        get => _cost;
        set
        {
            if (_cost != value)
            {
                _cost = value;
                OnCostChanged?.Invoke(_cost);
            }
        }
    }
    public int ATK
    {
        get => _ATK;
        set
        {
            if (_ATK != value)
            {
                _ATK = value;
                OnATKChanged?.Invoke(_ATK);
            }
        }
    }
    public int DEF
    {
        get => _DEF;
        set
        {
            if (_DEF != value)
            {
                _DEF = value;
                OnDEFChanged?.Invoke(_DEF);
            }
        }
    }
    public int HP
    {
        get => _HP;
        set
        {
            if (_HP != value)
            {
                _HP = value;
                OnHPChanged?.Invoke(_HP);
            }
        }
    }
    public int AGI
    {
        get => _AGI;
        set
        {
            if (_AGI != value)
            {
                _AGI = value;
                OnAGIChanged?.Invoke(_AGI);
            }
        }
    }

    public float attackRange;
    public string attackMode;
    public string attackAttribute;
    public string actionMode;
    public GameObject soldier1Prefab;

    private void Awake()
    {
        OnCostChanged += SetCostText;
        OnATKChanged += SetATKText;
        OnDEFChanged += SetDEFText;
        OnHPChanged += SetHPText;
        OnAGIChanged += SetAGIText;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BattleField"))
        {
            GameObject obj = Instantiate(soldier1Prefab, transform.position, transform.rotation, battleField);
            Soldier1 soldierScript = obj.GetComponent<Soldier1>();
            soldierScript.cardPrefab = gameObject;
            soldierScript.isDrag = true;
            soldierScript.cost = cost;
            soldierScript.ATK = ATK;
            soldierScript.DEF = DEF;
            soldierScript.HP = HP;
            soldierScript.AGI = AGI;
            soldierScript.attackRange = attackRange;
            gameObject.SetActive(false);
        }
    }

    public void SetCostText(int cost)
    {
        costText.text = cost.ToString();
    }

    public void SetATKText(int ATK)
    {
        ATKText.text = ATK.ToString();
    }

    public void SetDEFText(int DEF)
    {
        DEFText.text = DEF.ToString();
    }

    public void SetHPText(int HP)
    {
        HPText.text = HP.ToString();
    }

    public void SetAGIText(int AGI)
    {
        AGIText.text = AGI.ToString();
    }
}
