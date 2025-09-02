using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Soldier3 : SoldierBase
{
    public GameObject attackAim;
    public float aimDistance;
    public Vector3 aimPointPos;
    public GameObject attackBox;
    private Vector3 velocity = Vector3.zero;
    public override void Deploy()
    {
        base.Deploy();
        GameObject obj = Instantiate(ActionManager.Instance.actionPrefab, ActionManager.Instance.actionContent);
        obj.GetComponent<Image>().sprite = sprite;
        ActionMark actionMark = new ActionMark(AGI, DateTime.UtcNow, obj, TurnAction);
        ActionManager.Instance.AddAction(actionMark);
    }

    public void FindAttackAim()
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("EnemySoldier");
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        Collider2D enemyCollider = enemy.GetComponent<Collider2D>();

        aimPointPos = enemyCollider.ClosestPoint(transform.position);
        aimDistance = Vector3.Distance(transform.position, aimPointPos);
        attackAim = enemy;

        foreach (GameObject obj in taggedObjects)
        {
            Vector3 aimPos = obj.transform.GetChild(0).GetComponent<Collider2D>().ClosestPoint(transform.position);
            float distance = Vector3.Distance(transform.position, aimPos);
            if (distance < aimDistance)
            {
                aimPointPos = aimPos;
                aimDistance = distance;
                attackAim = obj;
            }
        }
    }

    public void TurnAction()
    {
        FindAttackAim();
        Debug.Log(attackRange);
        if (aimDistance < attackRange)
        {

            StartCoroutine(AttackActive());
        }
        else
        {

            StartCoroutine(PursuitActive());
        }
        Debug.Log("Soldier1Action");
    }

    IEnumerator AttackActive()
    {
        attackBox.SetActive(true);

        // 1. 计算目标方向
        Vector3 direction = (aimPointPos - attackBox.transform.position).normalized;
        // 2. 旋转物体，使物体的正上方朝向目标方向
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        attackBox.transform.rotation = Quaternion.Euler(0f, 0f, targetAngle);
        while (Vector3.Distance(attackBox.transform.position, aimPointPos) > 0.1f)
        {
            attackBox.transform.position = Vector3.SmoothDamp(attackBox.transform.position, aimPointPos, ref velocity, 0.3f);
            yield return null;  // 等待下一帧
        }
        Attack();
        yield return new WaitForSeconds(0.1f);
        attackBox.SetActive(false);
        attackBox.transform.localPosition = Vector3.zero;
        ActionManager.Instance.isActionFinished = true;
    }

    IEnumerator PursuitActive()
    {
        bool canAttack = false;
        while (!canAttack)
        {
            transform.position = Vector3.SmoothDamp(transform.position, aimPointPos, ref velocity, 0.3f);
            aimDistance = Vector3.Distance(transform.position, aimPointPos);
            if (aimDistance < attackRange * 50f)
            {
                canAttack = true;
            }
            yield return null;  // 等待下一帧
        }
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(AttackActive());
    }

    public void Attack()
    {
        if (attackAim.CompareTag("Enemy"))
        {
            attackAim.GetComponent<EnemyBase>().GetHit(ATK);
        }
        else if (attackAim.CompareTag("EnemySoldier"))
        {

        }

    }
}
