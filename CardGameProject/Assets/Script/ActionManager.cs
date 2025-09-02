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
    private int _turn;

    public event Action<int> OnTimeChanged;
    public event Action<int> OnAdditonTimeChanged;
    public event Action<int> OnTurnChanged;

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

    public int turn
    {
        get => _turn;
        set
        {
            if (_turn != value)
            {
                _turn = value;
                OnTurnChanged?.Invoke(_turn);
            }
        }
    }

    public Transform actionContent;
    public GameObject actionPrefab;
    public List<ActionMark> actionList = new List<ActionMark>();
    public bool isActionFinished;
    public float actionActiveTime = 1f;
    Coroutine additionTimeCoroutine;
    Coroutine timeCoroutine;
    public int eachTurnAddTime = 10;

    private void Awake()
    {
        Instance = this;
        OnTimeChanged += SetTimeText;
        OnAdditonTimeChanged += SetAdditionTimeText;
    }

    private void Start()
    {
        time = 20;
        NewTurnStart();
    }

    IEnumerator CountdownAdditionTimeCoroutine()
    {
        while (additionTime > 0)
        {
            additionTime -= 1;
            yield return new WaitForSeconds(1f);
        }
        additionTimeCoroutine = null;
        OnCountdownAdditionTimeFinished();
    }

    IEnumerator CountdownTimeCoroutine()
    {
        while (time > 0)
        {
            time -= 1;  
            yield return new WaitForSeconds(1f);
        }
        timeCoroutine = null;
        OnCountdownTimeFinished();
    }

    void OnCountdownAdditionTimeFinished()
    {
        timeCoroutine = StartCoroutine(CountdownTimeCoroutine());
    }

    void OnCountdownTimeFinished()
    {
        FinishOnClick();
    }

    public void FinishOnClick()
    {
        GameManager.instance.BlockClicks();
        GameManager.instance.embattle = false;
        if (additionTime == 0 && timeCoroutine != null)
        {
            StopCoroutine(timeCoroutine);
        }
        else if(additionTimeCoroutine != null)
        {
            StopCoroutine(additionTimeCoroutine);
        }
        StartCoroutine(TurnActionActive());
    }
    IEnumerator TurnActionActive()
    {
        yield return new WaitForSeconds(0.1f);
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
        GameManager.instance.UnblockClicks();
        NewTurnStart();
    }

    public void NewTurnStart()
    {
        turn += 1;
        if (!GameManager.instance.isSolo)
        {
            additionTime = eachTurnAddTime;
            additionTimeCoroutine = StartCoroutine(CountdownAdditionTimeCoroutine());
        }
        else
        {
            timeText.text = "¡Þ";
        }
        GameManager.instance.player.TurnStart();
        GameManager.instance.embattle = true;
    }


    public void SetAdditionTimeText(int additionTime)
    {
        timeText.text = "<color=#000000>" + time.ToString() + "</color>" + "<color=#0032FF>+" + additionTime.ToString() + "</color>";
    }

    public void SetTimeText(int time)
    {
        timeText.text = "<color=#000000>" + time.ToString() + "</color>";
    }

    public void SetTurnText(int turn)
    {
        turnCountText.text = "Turn" + turn.ToString();
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
