using UnityEngine;
using UnityEngine.UI;

public class EnemyBase : MonoBehaviour
{
    public Text hpText;

    private int _hp;

    public event System.Action<int> OnHpChanged;

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
    public int coins;
    public int resources;
    public int spellSpace;

    private void Awake()
    {
        OnHpChanged += SetHpText;
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

    public virtual void GetHit(int damage)
    {
        if (hp > 0)
        {
            hp -= damage;
        }
    }
}
