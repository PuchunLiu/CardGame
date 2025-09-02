using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ChooseManager : MonoBehaviour
{
    public static ChooseManager instance;
    public GameObject cardPrefab;
    public Transform content;
    public Transform backGround;
    public RectTransform backGroundrt;
    public RectTransform contentrt;
    public GridLayoutGroup layoutGroup;
    public float widthOffset = 50f;
    public float heightOffset = 20f;

    public bool finishChoose = false;

    private void Awake()
    {
        instance = this;
    }

    public void RandomGetChooseCard(int num)
    {
        var randomKeys = GetRandomKeys(GameManager.instance.SoldierCards, num);
        foreach (var key in randomKeys)
        {
            Card card = GameManager.instance.SoldierCards[key];
            GameObject obj = Instantiate(cardPrefab, content);
            CardInChoose cardinchoose = obj.transform.GetComponent<CardInChoose>();
            cardinchoose.SetInform(card);
        }

        backGroundrt.sizeDelta = new Vector2(layoutGroup.cellSize.x * num + layoutGroup.spacing.x * (num - 1) + widthOffset, layoutGroup.cellSize.y + heightOffset);
    }

    public void ContentClear()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject); // 在运行时销毁
        }
    }

    public List<TKey> GetRandomKeys<TKey, TValue>(Dictionary<TKey, TValue> dict, int n)
    {
        // 获取所有键
        var keys = new List<TKey>(dict.Keys);
        System.Random rand = new System.Random();

        // Fisher–Yates 洗牌
        for (int i = keys.Count - 1; i > 0; i--)
        {
            int j = rand.Next(0, i + 1);
            (keys[i], keys[j]) = (keys[j], keys[i]);
        }

        // 取前 n 个
        return keys.GetRange(0, n);
    }

}
