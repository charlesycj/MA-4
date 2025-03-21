using System.Collections;
using UnityEngine;

public class TestDir : MonoBehaviour
{
    private float dirspeed = 100f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            turnRight();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            turnLeft();
        }
    }

    private void turnRight()
    {
        StartCoroutine(GO(90f)); // 오른쪽으로 90도 회전
    }

    private void turnLeft()
    {
        StartCoroutine(GO(-90f)); // 왼쪽으로 90도 회전
    }

    private IEnumerator GO(float angle)
    {
        Quaternion targetRot = transform.rotation * Quaternion.Euler(0, angle, 0);
        while (Quaternion.Angle(transform.rotation, targetRot) > 0.1f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, dirspeed * Time.deltaTime);
            yield return null;
        }
        transform.rotation = targetRot;
    }
}