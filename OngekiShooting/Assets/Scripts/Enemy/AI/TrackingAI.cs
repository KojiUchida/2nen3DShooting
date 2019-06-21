using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingAI : AI
{
    public GameObject player;

    private Rigidbody rigid;
    private Rigidbody playerRigid;

    // Start is called before the first frame update
    public override void Init()
    {
        base.Init();
        rigid = GetComponent<Rigidbody>();
        playerRigid = player.GetComponent<Rigidbody>();
    }

    public override void Death()
    {
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Death();
    }
}
