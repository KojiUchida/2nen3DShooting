using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierArea : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        var bullet = other.GetComponent<Bullet>();
        bullet.SetSpeed(-bullet.GetSpeed());
        bullet.gameObject.tag = "ReflectBullet";
        bullet.isReflect = true;
    }
}
