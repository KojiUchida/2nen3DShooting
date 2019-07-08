using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField, Header("追従ターゲット")]
    Transform target;
    [SerializeField, Header("追従時間")]
    float smoothTime = 1.0f;
    [SerializeField, Header("タイトルのカメラの位置")]
    Vector3 titleCamPos;
    [SerializeField, Header("タイトルのカメラの向き")]
    Vector3 titleCamDir;
    [SerializeField, Header("プレイ中のカメラの位置")]
    Vector3 playCamPos;
    [SerializeField, Header("プレイ中のカメラの向き")]
    Vector3 playCamDir;

    Vector3 camPos;
    Vector3 velocity;

    bool titleToPlay;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = playCamPos;
        transform.rotation = Quaternion.Euler(playCamDir);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(playCamDir);
        Vector3 vec = target.position + playCamPos;
        vec.y = playCamPos.y;
        transform.position = Vector3.SmoothDamp(transform.position, vec, ref velocity, smoothTime);
    }
}
