using System.Collections;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Transform Player; 
    public float speed = 0.001f; // 이동 속도
    
    [SerializeField] private int dice = 1;
    
   
    void Update()
    {
  
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            StartCoroutine(move1());
        }
    }
    

    private IEnumerator move1()
    {
        //Dice 만들기
        int[] Dice = { 1, 2, 2, 3, 3, 4 };
        dice = Random.Range(0,6);
        Debug.Log("나온 주사위 수 :" + Dice[dice]);
        
        for (int i = 0; i < Dice[dice]; i++)
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
            Player.position = Vector3.MoveTowards(Player.position, target1, speed * Time.deltaTime);
            yield return  null;
        }
        Player.position = target1;
       
    }
    private IEnumerator ExMove()
    {
        Vector3 startPos = Player.position;
        Vector3 target = startPos + Player.forward;
        while (Vector3.Distance(Player.position, target) > 0.005f)
        {
            Player.position = Vector3.MoveTowards(Player.position, target, speed * Time.deltaTime);
            yield return  null;
        }
        Player.position = target;
    }
    private IEnumerator Downmove()
    {
        Vector3 startPos = Player.position;
        Vector3 target2 = startPos + new Vector3(0, -1, 0);
        while (Vector3.Distance(Player.position, target2) > 0.005f)
        {
            Player.position = Vector3.MoveTowards(Player.position, target2, speed * Time.deltaTime);
            yield return null;
        }
        Player.position = target2;
       
    }
}