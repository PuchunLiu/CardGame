using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Building : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public string buildingType = "1";
    public bool isActive = true;
    public Vector3 scale;
    public GameObject chooseUI;
    public GameObject tip;
    public Text tipText;
    private int _tipNum;
    public event System.Action<int> NeedTip;
    public int chooseNum = 3;
    public int tipNum
    {
        get => _tipNum;
        set
        {
            
            if (_tipNum != value)
            {
                _tipNum = value;
                NeedTip?.Invoke(_tipNum);
            }
        }
    }

    private void Awake()
    {
        scale = transform.localScale;
        NeedTip += ShowTip;
    }

    private void Start()
    {
        gameObject.SetActive(isActive);
    }



    public void OnPointerDown(PointerEventData eventData)
    {
        transform.localScale = new Vector3(0.95f, 0.95f, 0.95f); ;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1.05f, 1.05f, 1.05f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = scale;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale = scale;
        if(tipNum > 0)
        {
            DealTips();  
        }
        

    }

    public void DealTips()
    {
        StartCoroutine(DealTip());
    }

    IEnumerator DealTip()
    {
        while (tipNum > 0)
        {
            tipNum -= 1;
            chooseUI.SetActive(true);
            ChooseManager.instance.finishChoose = false;
            ChooseManager.instance.RandomGetChooseCard(chooseNum);
            while (!ChooseManager.instance.finishChoose)
            {
                yield return null;
            }
            chooseUI.SetActive(false);
        }
    }

    public void ShowTip(int num)
    {
        if(num > 0)
        {
            tip.SetActive(true);
            tipText.text = num.ToString();
        }
        else
        {
            tip.SetActive(false);
        }
    }
}
