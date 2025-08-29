using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Text hpText;
    public Text coinsText;
    public Text resourcesText;
    public Text spellSpaceText;

    private int _hp;
    private int _coins;
    private int _resources;
    private int _spellSpace;

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
                _spellSpace = value;
                OnSpellSpaceChanged?.Invoke(_spellSpace);
            }
        }
    }

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

    public void SetHpText(int hp)
    {
        hpText.text = hp.ToString();
    }

    public void SetCoinText(int coins)
    {
        coinsText.text = coins.ToString();
    }

    public void SetResourcesText(int resources)
    {
        resourcesText.text = resources.ToString();
    }

    public void SetSpellSpaceText(int spellSpace)
    {
        spellSpaceText.text = spellSpace.ToString();
    }
}
