using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingAI : MonoBehaviour
{
    Rigidbody rigidbody;
    public GameObject player;
    private Rigidbody playerRigid;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        playerRigid = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (playerRigid == null) return;
        //rigidbody.position += new Vector3(playerRigid.position.x - rigidbody.position.x, 0, playerRigid.position.z - rigidbody.position.z) * Time.deltaTime;
        rigidbody.position += new Vector3(playerRigid.position.x - rigidbody.position.x, 0, -10) * Time.deltaTime;
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
