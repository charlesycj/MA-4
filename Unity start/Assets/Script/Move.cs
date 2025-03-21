

using System.Collections;
using UnityEngine;


public class Move : MonoBehaviour
{
    public Transform Player; 
    public float speed = 0.001f; // 이동 속도
    
    [SerializeField] private int dice = 1;
    public float dirspeed = 2f;
    public Collider triggerCollider;
    public void OnButtonClick()
    {
        StartCoroutine(move1());
        
    }
    

    private void OnTriggerStay(Collider other)
    {
        if(triggerCollider.isTrigger==false)
            return;
        if (other.CompareTag("RightVConor"))
        {
            Debug.Log("우측");
            triggerCollider.isTrigger = false;
            StartCoroutine(Conormove(90f));
            
            
        }
        if (other.CompareTag("LeftVConor"))
        {
            Debug.Log("좌측");
            triggerCollider.isTrigger = false;
            StartCoroutine(Conormove(-90f));
            
        }
        if (other.CompareTag("RightConor"))
        {
            Debug.Log("좌측");
            triggerCollider.isTrigger = false;
            StartCoroutine(Conormove2(90f));
            
        }
        if (other.CompareTag("LeftConor"))
        {
            Debug.Log("좌측");
            triggerCollider.isTrigger = false;
            StartCoroutine(Conormove2(-90f));
            
        }
        
    }

    private IEnumerator Conormove(float angle)
    {
        
        float Angle = angle;
        for (int i = 0; i < 3; i++)
        {
            Debug.Log(1);
            yield return StartCoroutine(Conor(Angle));
            Debug.Log(3);
            yield return StartCoroutine(move1());
        }
        triggerCollider.isTrigger = true;

    }
    private IEnumerator Conormove2(float angle)
    {
        
        float Angle = angle;
        for (int i = 0; i < 2; i++)
        {
            Debug.Log(1);
            yield return StartCoroutine(Conor(Angle));
            Debug.Log(3);
            yield return StartCoroutine(move1());
        }
        triggerCollider.isTrigger = true;

    }
    private IEnumerator Conor(float angle)
    {
        Debug.Log(2);
        Quaternion target = transform.rotation * Quaternion.Euler(0f, angle, 0f);
        while (Quaternion.Angle(transform.rotation, target) > 0.1f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target, dirspeed * Time.deltaTime);
            yield return null;
        }
        transform.rotation = target;
    }
    
    private IEnumerator move1()
    {
        //Dice 만들기
        //int[] Dice = { 1, 2, 2, 3, 3, 4 };
        //dice = Random.Range(0,6);
        //Debug.Log(Dice[dice]);
        
        for (int i = 0; i < 1/*Dice[dice]*/; i++)
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
        Debug.Log("전진 시작");
        Vector3 startPos = Player.position;
        Vector3 target = startPos + transform.forward * 2;
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