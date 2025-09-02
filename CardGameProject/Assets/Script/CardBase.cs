using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static UnityEditor.PlayerSettings;
public class CardBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public int currentSiblingIndex;
    public int lastSiblingIndex;
    public Vector3 scale;
    public float upOffset;
    public Vector3 lastPos;
    public Vector3 lastScale;
    public bool isDrag = false;
    public Collider2D battleField;
    public Text effectText;
    public Text Name;
    public bool isDown = false;
    public virtual void Start()
    {
        //lastPos = transform.position;
        lastScale = transform.localScale;
        battleField = GameObject.FindWithTag("BattleField").GetComponent<Collider2D>();
        currentSiblingIndex = transform.GetSiblingIndex();
    }

    public virtual void Update()
    {
        if (isDrag)
        {
            transform.position = Input.mousePosition;
        }
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        lastSiblingIndex = currentSiblingIndex;
        transform.SetAsLastSibling();
        transform.localScale = scale;
        transform.localPosition += new Vector3(0f, upOffset, 0f);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        transform.SetSiblingIndex(lastSiblingIndex);
        transform.localPosition = lastPos;
        transform.localScale = lastScale;
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        isDown = false;
        BackToPos();
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        isDown = true;
        isDrag = true;
    }

    public virtual void BackToPos()
    {
        isDrag = false;
        transform.localPosition = lastPos;
        transform.localScale = lastScale;
        transform.SetSiblingIndex(lastSiblingIndex);
    }
}
