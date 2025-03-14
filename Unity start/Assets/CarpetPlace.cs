using UnityEngine;

public class CarpetPlace : MonoBehaviour
{
    public GameObject Carpet; // 청사진 오브젝트
    public Transform player; // 플레이어 오브젝트 찾기

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Vector3 carpetPosition = player.position + new Vector3(0, 0, 1);
            Carpet.SetActive(true);
            Carpet.transform.position = carpetPosition;
        }
    }
}