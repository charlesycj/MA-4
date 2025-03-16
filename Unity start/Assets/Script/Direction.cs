using System.Collections;
using UnityEngine;

public class Direction : MonoBehaviour
{
    public int dir = 0;
    private float dirspeed = 100f;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            turnRight();
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            turnLeft();
        }
    }

    private void turnRight()
    {
        if (dir < 1)
        {
            dir += 1;
            StartCoroutine(GO());
        }
    }

    private void turnLeft()
    {
        if (dir > -1)
        {
            dir -= 1;
            StartCoroutine(GO());
        }
    }

    private IEnumerator GO()
    {
        Quaternion targetRot =  Quaternion.Euler(0, dir * 90, 0);
        while (Quaternion.Angle(transform.rotation, targetRot) > 0.1f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, dirspeed * Time.deltaTime);
            yield return null;
        }
        transform.rotation = targetRot;
    }

}
