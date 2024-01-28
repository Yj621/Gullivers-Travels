using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public FloatingJoystick joystick; // FloatingJoystick 참조를 저장하기 위한 변수
    public float moveSpeed = 5f; // 플레이어의 이동 속도
    public float growthRate = 0.1f; // 플레이어 성장 속도

    // Update is called once per frame
    void Update()
    {
        // 조이스틱의 입력 값을 얻어오기
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        // 입력 값을 기반으로 플레이어를 이동시키기
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        transform.position += movement * moveSpeed * Time.deltaTime;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food") && other.transform.localScale.x < transform.localScale.x)
        {
            // 작은 물체만 흡수
            AbsorbObject(other.gameObject);
        }
    }

    void AbsorbObject(GameObject objectToAbsorb)
    {
        // 물체를 흡수하고 크기를 키움
        transform.localScale += new Vector3(growthRate, growthRate, growthRate);

        // 흡수한 물체 제거
        Destroy(objectToAbsorb);
    }

}
