using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SoldierBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text ATKText;
    public Text DEFText;
    public Text HPText;
    public Text AGIText;
    public Transform attackRangeTrans;

    private int _ATK;
    private int _DEF;
    private int _HP;
    private int _AGI;
    private float _attackRange;

    public event System.Action<int> OnATKChanged;
    public event System.Action<int> OnDEFChanged;
    public event System.Action<int> OnHPChanged;
    public event System.Action<int> OnAGIChanged;
    public event System.Action<float> OnAttackRangeChanged;

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
    public float attackRange
    {
        get => _attackRange;
        set
        {
            if (_attackRange != value)
            {
                _attackRange = value;
                OnAttackRangeChanged?.Invoke(_attackRange);
            }
        }
    }

    public int cost;
    public Image soldierImage;
    public GameObject cardPrefab;
    public bool isDrag = false;
    public bool canDeploy = false;
    public bool isDeploy = false;
    public Sprite sprite;
    public string owner;
    public bool canRecycle;
    public Collider2D playerArea;
    public Collider2D enemyArea;

    private void Awake()
    {
        OnATKChanged += SetATKText;
        OnDEFChanged += SetDEFText;
        OnHPChanged += SetHPText;
        OnAGIChanged += SetAGIText;
        OnAttackRangeChanged += ChangeAttackRange;
        playerArea = GameObject.FindWithTag("PlayerArea").GetComponent<Collider2D>();
        enemyArea = GameObject.FindWithTag("EnemyArea").GetComponent<Collider2D>();
    }

    public virtual void Update()
    {
        if (isDrag)
        {
            transform.position = Input.mousePosition;
            IsSoldierCanBeDeployed();
        }
        if (Input.GetMouseButtonUp(0))
        {
            DeploySoldier();
        }
    }

    public virtual void IsSoldierCanBeDeployed()
    {
        if (playerArea.OverlapPoint(transform.position))
        {
            soldierImage.color = Color.blue;
            canDeploy = true;
        }
        else if (enemyArea.OverlapPoint(transform.position))
        {
            soldierImage.color = Color.red;
            canDeploy = false;
        }
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (!isDeploy)
        {
            if (collision.CompareTag("BattleField"))
            {
                Destroy(gameObject);
                cardPrefab.SetActive(true);
            }
        }
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

    public void ChangeAttackRange(float scale)
    {
        attackRangeTrans.localScale = new Vector3(scale, scale, scale);
    }

    public virtual void DeploySoldier()
    {
        if (!isDeploy)
        {
            if (canDeploy && cost <= GameManager.instance.player.coins)
            {

                Deploy();
            }
            else
            {
                Destroy(gameObject);
                cardPrefab.SetActive(true);
                cardPrefab.GetComponent<CardBase>().BackToPos();
            }
        }
    }

    public virtual void Deploy()
    {
        isDrag = false;
        isDeploy = true;
        GameManager.instance.player.coins -= cost;
        soldierImage.color = Color.white;
        attackRangeTrans.gameObject.SetActive(false);
        Destroy(cardPrefab);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        attackRangeTrans.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        attackRangeTrans.gameObject.SetActive(false);
    }
}
