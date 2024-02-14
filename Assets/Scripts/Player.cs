using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public FloatingJoystick joystick; // FloatingJoystick 참조를 저장하기 위한 변수
    public GameObject followerPrefab; // 플레이어를 따라다니는 오브젝트 프리팹
    public float moveSpeed = 5f; // 플레이어의 이동 속도
    private float followDistance = 0.8f; // 플레이어 뒤를 따라오는 거리
    private List<GameObject> followers = new List<GameObject>(); // 따라오는 오브젝트를 저장하기 위한 리스트
    Animator anim;
    private bool isMove = false; // 플레이어의 이동 여부를 나타내는 변수

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 조이스틱의 입력 값을 얻어오기
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        // 입력 값을 기반으로 플레이어를 이동시키기
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        if (movement != Vector3.zero)
        {
            isMove = true; // 플레이어가 움직이는 중임을 나타냄
            transform.rotation = Quaternion.LookRotation(movement.normalized);        
            transform.position += movement * moveSpeed * Time.deltaTime;
        }
        else
        {
            isMove = false; // 플레이어가 멈춰있음을 나타냄
        }

        // 따라오는 오브젝트들의 위치 및 방향 업데이트
        UpdateFollowers();

        // 움직임 여부에 따라 애니메이션 제어
        anim.SetBool("isMove", isMove);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Human"))
        {
            // 충돌한 Food 태그를 가진 오브젝트가 플레이어 뒤를 따라오게 만들기
            Vector3 followerPosition = transform.position - transform.forward * (followers.Count + 1) * followDistance;
            GameObject follower = Instantiate(followerPrefab, followerPosition, Quaternion.identity);
            followers.Add(follower); // 리스트에 추가
            Destroy(other.gameObject); // 충돌한 Food 오브젝트 제거
        }   
    }
    void UpdateFollowers()
    {
        for (int i = 0; i < followers.Count; i++)
        {
            if (followers[i] != null) // 오브젝트가 존재하는 경우에만 처리
            {
                // 플레이어를 바라보도록 설정
                Vector3 direction = transform.position - followers[i].transform.position;
                followers[i].transform.rotation = Quaternion.LookRotation(direction);

                // 오브젝트들이 겹치지 않게 위치 조정
                Vector3 offset = transform.forward * (i + 1) * followDistance;
                followers[i].transform.position = transform.position - offset;
            }
        }
    }


}