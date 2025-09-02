using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HandsCardManager : MonoBehaviour
{
    public static HandsCardManager instance;
    public RectTransform rt;
    public float initialSpacing = 250f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        MoveCards();
    }

    public List<Vector3> SetCardsPos(int childCount)
    {
        List<Vector3> posList = new List<Vector3>();
        float parentWidth = rt.rect.width;

        // 计算间隔：如果超宽，就缩小间隔，否则用初始间隔
        float spacing = initialSpacing;
        float totalWidth = (childCount - 1) * spacing;

        if (totalWidth > parentWidth)
        {
            spacing = parentWidth / (childCount - 1);
            totalWidth = parentWidth;
        }

        // 起始 X（让整体居中）
        float startX = -totalWidth / 2f;

        for (int i = 0; i < childCount; i++)
        {
            Vector3 pos = new Vector3(startX + i * spacing, -100f, 0f); // X 按照间隔排布
            posList.Add(pos);
        }
        return posList;
    }

    public void MoveCards()
    {
        List<Vector3> posList = SetCardsPos(transform.childCount);
        for (int i = 0;i < posList.Count;i++)
        {
            StartCoroutine(MoveCardToPos(posList[i], transform.GetChild(i))); 
        }
    }

    IEnumerator MoveCardToPos(Vector3 pos, Transform Child, float moveTime = 0.2f)
    {
        GameManager.instance.BlockClicks();
        Vector3 startPos = Child.localPosition;
        float elapsed = 0;
        while (elapsed < moveTime)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / moveTime);
            Child.localPosition = Vector3.Lerp(startPos, pos, t);
            yield return null;
        }
        Child.localPosition = pos;
        Child.GetComponent<CardBase>().lastPos = pos;
        GameManager.instance.UnblockClicks();
    }
}
