using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField, Header("追従ターゲット")]
    Transform target;
    [SerializeField, Header("追従時間")]
    float smoothTime = 1.0f;
    [SerializeField, Header("カメラの位置")]
    Vector3 camPos;
    [SerializeField, Header("カメラの向き")]
    Vector3 camDir;

    Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = camPos;
        transform.rotation = Quaternion.Euler(camDir);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(camDir);
        Vector3 vec = target.position + camPos;
        vec.y = camPos.y;
        transform.position = Vector3.SmoothDamp(transform.position, vec, ref velocity, smoothTime);
    }
}
