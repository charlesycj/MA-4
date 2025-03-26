using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    public Transform Player; 
    public float speed = 0.001f; // 이동 속도
    public float dirspeed = 1000f; // 회전 속도
    public Collider triggerCollider;
    public Button button;
    private int remainCount; //남은 이동
    private Coroutine currentCoroutine; // 현재 실행 중인 코루틴
    private bool isMoving;
    public void DiceInput(int dice)
    {
        button.interactable = false;
        currentCoroutine = StartCoroutine(MovePlayer(dice));
        isMoving = true;
    }
    private IEnumerator MovePlayer(int dices)
    {
        remainCount = dices;
        for (int i = 0; i < dices; i++)
        {
            yield return StartCoroutine(UpMove());
            yield return StartCoroutine(ExMove());
            yield return StartCoroutine(Downmove());
            remainCount--; 
            Debug.Log("남은 주사위 수: "+remainCount);
        }
        isMoving = false;
        button.interactable = true;
    }
    
    private void StartMovement()
    {
        remainCount--;
        if (remainCount >= 0)
        {
            isMoving = true;
            currentCoroutine = StartCoroutine(MovePlayer(remainCount)); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isMoving)
        {
            if (other.CompareTag("RightVConor"))
            {
                triggerCollider.isTrigger = false;
                if (currentCoroutine != null) // currentCoroutine이 null이 아닐 때만 중단
                {
                    StopCoroutine(currentCoroutine);
                }

                currentCoroutine = StartCoroutine(Conormove(90f)); 
            }
            else if (other.CompareTag("LeftVConor"))
            {
                triggerCollider.isTrigger = false;
                if (currentCoroutine != null) 
                {
                    StopCoroutine(currentCoroutine);
                }

                currentCoroutine = StartCoroutine(Conormove(-90f)); 
            }
            else if (other.CompareTag("RightConor"))
            {
                triggerCollider.isTrigger = false;
                if (currentCoroutine != null) 
                {
                    StopCoroutine(currentCoroutine);
                }

                currentCoroutine = StartCoroutine(Conormove2(90f)); 
            }
            else if (other.CompareTag("LeftConor"))
            {
                triggerCollider.isTrigger = false;
                if (currentCoroutine != null) 
                {
                    StopCoroutine(currentCoroutine);
                }

                currentCoroutine = StartCoroutine(Conormove2(-90f)); 
            }
        }
    }
    private IEnumerator Conormove(float angle)
    {
        triggerCollider.isTrigger = false;
        for (int i = 0; i < 3; i++)
        {
            //Debug.Log(1);
            yield return StartCoroutine(Conor(angle));
            //Debug.Log(3);
            yield return new WaitForSeconds(0.1f*Time.deltaTime);
            yield return StartCoroutine(ExMove());
        }
        if (remainCount > 0)
        {
            StartMovement(); // 남아있는 이동이 있으면 다시 이동 시작
        }
        yield return new WaitForSeconds(0.1f);
        triggerCollider.isTrigger = true; // 트리거 재활성화
    }

    private IEnumerator Conormove2(float angle)
    {
        triggerCollider.isTrigger = false;
        for (int i = 0; i < 2; i++)
        {
            // Debug.Log(1);
            yield return StartCoroutine(Conor(angle));
            //Debug.Log(3);
            yield return new WaitForSeconds(0.1f*Time.deltaTime);
            yield return StartCoroutine(ExMove());
        }
        if (remainCount > 0)
        {
            StartMovement(); 
        }
        yield return new WaitForSeconds(0.1f);
        triggerCollider.isTrigger = true; 
    }

    private IEnumerator Conor(float angle)
    {
        Quaternion target = transform.rotation * Quaternion.Euler(0f, angle, 0f);
        while (Quaternion.Angle(transform.rotation, target) > 1f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target, dirspeed * Time.deltaTime);
            yield return null;
        }
        transform.rotation = target; 
    }

    private IEnumerator UpMove()
    {
        
        Vector3 target1 = transform.position + new Vector3(0, 1, 0);
        while (Vector3.Distance(transform.position, target1) > 0.005f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target1, speed * Time.deltaTime);
            yield return null;
        }
        transform.position = target1; // 최종 위치 설정
    }

    private IEnumerator ExMove()
    {
        
        Vector3 target = transform.position + transform.forward;
        while (Vector3.Distance(transform.position, target) > 0.005f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return null;
        }
        transform.position = target; 
    }

    private IEnumerator Downmove()
    {
        
        Vector3 target2 = transform.position + new Vector3(0, -1, 0);
        while (Vector3.Distance(transform.position, target2) > 0.005f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target2, speed * Time.deltaTime);
            yield return null;
        }
        transform.position = target2; 
    }
}
