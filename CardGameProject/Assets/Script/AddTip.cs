using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddTip : MonoBehaviour
{
    public Text tipText;
    public float moveTime = 0.3f;
    public float upDistance = 50f;
    public void ShowTip()
    {
        gameObject.SetActive(true);
        StartCoroutine(TipAction());
    }

    IEnumerator TipAction()
    {
        Vector3 pos = transform.position;
        Color textColor = tipText.color;
        float elapsed = 0;
        while(elapsed < moveTime)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / moveTime);
            transform.position = Vector3.Lerp(pos, pos+new Vector3(0, upDistance, 0), t);
            Color c = tipText.color;
            c.a = Mathf.Lerp(1f, 0f, t);
            tipText.color = c;
            yield return null;
        }
        gameObject.SetActive(false);
        tipText.color = textColor;
        transform.position = pos;
    }

}
