using UnityEngine;
using UnityEngine.UI;
using static System.Net.WebRequestMethods;

public class Player : MonoBehaviour
{
    public Text hpText;
    public Text coinsText;
    public Text resourcesText;
    public Text spellSpaceText;
    public Text getHpText;
    public Text getCoinsText;
    public Text getResourcesText;
    public Text getSpellSpaceText;

    private int _hp;
    private int _coins;
    private int _resources;
    private int _spellSpace;
    public int getHp;
    public int getCoins;
    public int getResources;
    public int getSpellSpace;

    public event System.Action<int> OnHpChanged;
    public event System.Action<int> OnCoinsChanged;
    public event System.Action<int> OnResourcesChanged;
    public event System.Action<int> OnSpellSpaceChanged;

    public int hp
    {
        get => _hp;
        set
        {
            if (_hp != value)
            {
                getHp = _hp - value;
                _hp = value;
                OnHpChanged?.Invoke(_hp);
            }
        }
    }
    public int coins
    {
        get => _coins;
        set
        {
            if (_coins != value)
            {
                getCoins = _coins - value;
                _coins = value;
                OnCoinsChanged?.Invoke(_coins);
            }
        }
    }
    public int resources
    {
        get => _resources;
        set
        {
            if (_resources != value)
            {
                getResources = _resources - value;
                _resources = value;
                OnResourcesChanged?.Invoke(_resources);
            }
        }
    }
    public int spellSpace
    {
        get => _spellSpace;
        set
        {
            if (_spellSpace != value)
            {
                getSpellSpace = _spellSpace - value;
                _spellSpace = value;
                OnSpellSpaceChanged?.Invoke(_spellSpace);
            }
        }
    }

    public AddTip hpTip;
    public AddTip coinsTip;
    public AddTip resourcesTip;
    public AddTip spellTip;
    public Building building1;
    public Building building2;
    public Building building3;

    private void Awake()
    {
        OnHpChanged += SetHpText;
        OnCoinsChanged += SetCoinText;
        OnResourcesChanged += SetResourcesText;
        OnSpellSpaceChanged += SetSpellSpaceText;
    }

    private void Start()
    {
        hp = 100;
        coins = 10;
        resources = 5;
        spellSpace = 1;
    }

    public void TurnStart()
    {
        coins += 10;
        resources += 5;
        spellSpace += 1;
        if (building1.isActive)
        {
            building1.tipNum += 1;
        }
        if (building2.isActive)
        {
            building2.tipNum += 1;
        }
        if (building3.isActive)
        {
            building3.tipNum += 1;
        }
    }

    public void SetHpText(int hp)
    {
        getHpText.text = getHp.ToString();
        if (getHp > 0)
        {
            getHpText.color = Color.red;
        }
        else
        {
            getHpText.color = Color.green;
        }
        hpTip.ShowTip();
        hpText.text = hp.ToString();
    }

    public void SetCoinText(int coins)
    {
        getCoinsText.text = getCoins.ToString();
        if (getCoins > 0)
        {
            getCoinsText.color = Color.red;
        }
        else
        {
            getCoinsText.color = Color.green;
        }
        coinsTip.ShowTip();
        coinsText.text = coins.ToString();
    }

    public void SetResourcesText(int resources)
    {
        getResourcesText.text = getResources.ToString();
        if (getResources > 0)
        {
            getResourcesText.color = Color.red;
        }
        else
        {
            getResourcesText.color = Color.green;
        }
        resourcesTip.ShowTip();
        resourcesText.text = resources.ToString();
    }

    public void SetSpellSpaceText(int spellSpace)
    {
        getSpellSpaceText.text = getSpellSpace.ToString();
        if (getSpellSpace > 0)
        {
            getSpellSpaceText.color = Color.red;
        }
        else
        {
            getSpellSpaceText.color = Color.green;
        }
        spellTip.ShowTip();
        spellSpaceText.text = spellSpace.ToString();
    }
}
