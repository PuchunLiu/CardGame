using UnityEngine;

public class Enemy1 : EnemyBase
{
    //public bool isEmbattled = false;
    //public GameObject attackAim;
    //public float aimDistance;
    //public Vector3 aimPointPos;
    //private void Update()
    //{
    //    if (!GameManager.instance.embattle)
    //    {
    //        isEmbattled = false;
    //    }
    //    if (GameManager.instance.embattle && !isEmbattled)
    //    {
    //        Embattle();
    //        isEmbattled = true;
    //    }
    //}

    //public void FindAim()
    //{
    //    GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("PlayerSoldier");
    //    GameObject player = GameManager.instance.player.gameObject;
    //    Collider2D playerCollider = player.GetComponent<Collider2D>();

    //    aimPointPos = playerCollider.ClosestPoint(transform.position);
    //    aimDistance = Vector3.Distance(transform.position, aimPointPos);
    //    attackAim = player;

    //    foreach (GameObject obj in taggedObjects)
    //    {
    //        Vector3 aimPos = obj.transform.GetChild(0).GetComponent<Collider2D>().ClosestPoint(transform.position);
    //        float distance = Vector3.Distance(transform.position, aimPos);
    //        if (distance < aimDistance)
    //        {
    //            aimPointPos = aimPos;
    //            aimDistance = distance;
    //            attackAim = obj;
    //        }
    //    }
    //}

    //public void Embattle()
    //{
        
    //}
}
