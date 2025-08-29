using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ActionManager : MonoBehaviour
{
    public static ActionManager Instance;
    public Text turnCountText;
    public Text timeText;
    public Transform actionContent;

    private int _time;
    private int _additionTime;

    public event System.Action<int> OnTimeChanged;
    public event System.Action<int> OnAdditonTimeChanged;

    public int time
    {
        get => _time;
        set
        {
            if (_time != value)
            {
                _time = value;
                OnTimeChanged?.Invoke(_time); 
            }
        }
    }
    public int additionTime
    {
        get => _additionTime;
        set
        {
            if (_additionTime != value)
            {
                _additionTime = value;
                OnAdditonTimeChanged?.Invoke(_additionTime);
            }
        }
    }

    private void Awake()
    {
        Instance = this;
        OnTimeChanged += SetTimeText;
        OnAdditonTimeChanged += SetAdditionTimeText;
    }

    private void Start()
    {
        time = 20;
        additionTime = 10;
        StartCoroutine(CountdownAdditionTimeCoroutine());
    }

    IEnumerator CountdownAdditionTimeCoroutine()
    {
        while (additionTime > 0)
        {
            additionTime -= 1;
            yield return new WaitForSeconds(1f);
        }

        OnCountdownAdditionTimeFinished();
    }

    IEnumerator CountdownTimeCoroutine()
    {
        while (time > 0)
        {
            time -= 1;  
            yield return new WaitForSeconds(1f);
        }
        OnCountdownTimeFinished();
    }

    void OnCountdownAdditionTimeFinished()
    {
        StartCoroutine(CountdownTimeCoroutine());
    }

    void OnCountdownTimeFinished()
    {
        FinishOnClick();
    }

    public void FinishOnClick()
    {
        Debug.Log("Finished");
    }

    public void SetAdditionTimeText(int additionTime)
    {
        timeText.text = "<color=#000000>" + time.ToString() + "</color>" + "<color=#0032FF>+" + additionTime.ToString() + "</color>";
    }

    public void SetTimeText(int time)
    {
        timeText.text = "<color=#000000>" + time.ToString() + "</color>";
    }
}
