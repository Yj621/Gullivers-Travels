using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] public Transform target;
    [SerializeField] private float distance =  10.0f;
    [SerializeField] private float height = 5.0f;
    [SerializeField] private float rotationDamping;
    [SerializeField] private float heightDamping;

    void LateUpdate()
    {
        if (!target)
            return;

        var wantedRotationAngle = target.eulerAngles.y;
        var wantedHeight = target.position.y + height;

        var currentRotationAngle = transform.eulerAngles.y;
        var currentHeight = transform.position.y;

        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        transform.position = target.position;
        transform.position -= currentRotation * Vector3.forward * distance;

        // 고정된 y축 값 설정
        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

        // 플레이어를 바라보기만 하도록 변경
        transform.LookAt(target);
    }
}
