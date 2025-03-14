using System.Collections;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Transform Player; // 플레이어의 Transform
    public float speed = 0.001f; // 이동 속도
    
    [SerializeField] private int dice = 1; // 이동할 거리 (z 방향으로 1)

     void Start()
    {
            StartCoroutine(move1());
    }

    private IEnumerator move1()
    {
        for (int i = 0; i < dice; i++)
        {
            yield return StartCoroutine(UpMove());
            yield return StartCoroutine(ExMove());
            yield return StartCoroutine(Downmove());
        }
    }

    private IEnumerator UpMove()
    {
        Vector3 startPos = Player.position;
        Vector3 target1 = startPos + new Vector3(0, 1, 0);
        while (Vector3.Distance(Player.position, target1) > 0.005f)
        {
            Player.position = Vector3.MoveTowards(Player.position, target1, speed); // 이동
            yield return  null;
        }
        Player.position = target1;
       
    }
    private IEnumerator ExMove()
    {
        Vector3 startPos = Player.position; // 시작 위치
        Vector3 target = startPos + new Vector3(0, 0, 1);
        while (Vector3.Distance(Player.position, target) > 0.005f)
        {
            Player.position = Vector3.MoveTowards(Player.position, target, speed); // 이동
            yield return null;
        }
        Player.position = target;
    }
    private IEnumerator Downmove()
    {
        Vector3 startPos = Player.position;
        Vector3 target2 = startPos + new Vector3(0, -1, 0);
        while (Vector3.Distance(Player.position, target2) > 0.005f)
        {
            Player.position = Vector3.MoveTowards(Player.position, target2, speed); // 이동
            yield return null;
        }
        Player.position = target2;
       
    }
}