using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardInChoose : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public Text effectText;
    public Text Name;
    public Image image;
    public Text cost;
    public Text ATK;
    public Text DEF;
    public Text HP;
    public Text AGI;
    public Vector3 scale;
    public Card card;

    private void Awake()
    {
        scale = transform.localScale;
    }

    public void SetInform(Card card)
    {
        this.card = card;
        effectText.text = $"{card.AttackAttribute},{card.ActionMode},{card.Effect}";
        Name.text = card.Name;
        cost.text = card.Cost.ToString();
        ATK.text = card.ATK.ToString();
        DEF.text = card.DEF.ToString();
        HP.text = card.HP.ToString();
        AGI.text = card.AGI.ToString();
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
        GameObject prefab = Resources.Load<GameObject>($"Prefab/Cards/Card{card.ID}");
        Instantiate(prefab, transform.position, transform.rotation, HandsCardManager.instance.transform);
        HandsCardManager.instance.MoveCards();
        ChooseManager.instance.finishChoose = true;
        ChooseManager.instance.ContentClear();
    }
}
