using System.Collections;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Transform Player; // 플레이어의 Transform
    public float speed = 0.001f; // 이동 속도
    private bool IsMoving = false; // 이동 중인지 여부
    [SerializeField] private int dice = 1; // 이동할 거리 (z 방향으로 1)

    private void Start()
    {
        move1();
        
    }

    private void move1()
    {
        StartCoroutine(UpMove());
       
        
    }

    private IEnumerator UpMove()
    {
        IsMoving = true;
        
        Vector3 startPos = Player.position;
        Vector3 target1 = startPos + new Vector3(0, dice, 0);
        while (Vector3.Distance(Player.position, target1) > 0.005f)
        {
            Player.position = Vector3.MoveTowards(Player.position, target1, speed); // 이동
            yield return null;
        }
        Player.position = target1;
        StartCoroutine(ExMove());
    }
    private IEnumerator ExMove()
    {
        IsMoving = true;

        Vector3 startPos = Player.position; // 시작 위치
        Vector3 target = startPos + new Vector3(0, 0, dice);

        
        while (Vector3.Distance(Player.position, target) > 0.005f)
        {
            Player.position = Vector3.MoveTowards(Player.position, target, speed); // 이동
            yield return null;
        }

        Player.position = target;
        StartCoroutine(Downmove());
       
    }
    private IEnumerator Downmove()
    {
        IsMoving = true;
        
        Vector3 startPos = Player.position;
        Vector3 target2 = startPos + new Vector3(0, -dice, 0);
        while (Vector3.Distance(Player.position, target2) > 0.005f)
        {
            Player.position = Vector3.MoveTowards(Player.position, target2, speed); // 이동
            yield return null;
        }
        Player.position = target2;
        IsMoving = false;
    }
}