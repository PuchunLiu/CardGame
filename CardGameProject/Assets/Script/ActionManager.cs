using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ActionMark
{
    public int AGI;
    public DateTime timeNow;
    public GameObject obj;
    public event Action turnAction;

    public ActionMark(int AGI, DateTime timeNow, GameObject obj, Action action)
    {
        this.AGI = AGI;
        this.timeNow = timeNow;
        this.obj = obj;
        turnAction += action;
    }

    public void InvokeAction()
    {
        turnAction.Invoke();
    }
}

public class ActionManager : MonoBehaviour
{
    public static ActionManager Instance;
    public Text turnCountText;
    public Text timeText;

    private int _time;
    private int _additionTime;

    public event Action<int> OnTimeChanged;
    public event Action<int> OnAdditonTimeChanged;

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

    public Transform actionContent;
    public GameObject actionPrefab;
    public List<ActionMark> actionList = new List<ActionMark>();
    public bool isActionFinished;
    public float actionActiveTime = 1f;
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
        Cursor.visible = false;
        StartCoroutine(TurnActionActive());
    }
    IEnumerator TurnActionActive()
    {
        for (int i = 0; i < actionList.Count; i++)
        {
            isActionFinished = false;
            bool isActionInvoked = false;
            while (!isActionFinished)
            {
                if (!isActionInvoked)
                {
                    actionList[i].InvokeAction();
                    isActionInvoked = true;
                }
                yield return null;
            }
        }
        Cursor.visible = true;
    }


    public void SetAdditionTimeText(int additionTime)
    {
        timeText.text = "<color=#000000>" + time.ToString() + "</color>" + "<color=#0032FF>+" + additionTime.ToString() + "</color>";
    }

    public void SetTimeText(int time)
    {
        timeText.text = "<color=#000000>" + time.ToString() + "</color>";
    }

    public void AddAction(ActionMark actionMark, bool needRefresh = true)
    {
        actionList.Add(actionMark);
        if(needRefresh)
        {
            RefreshActionList();
        }
    }

    public void RefreshActionList()
    {
        actionList.Sort((x, y) => y.AGI.CompareTo(x.AGI));
        for (int i = 0; i < actionList.Count; i++)
        {
            actionList[i].obj.transform.SetSiblingIndex(i);
        }
    }
}
