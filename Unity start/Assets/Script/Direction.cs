using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Direction : MonoBehaviour
{
    [FormerlySerializedAs("Dir")] public int dir = 0;
    private float _dirspeed = 100f;
    private void Update()
    {
        if ( Input.GetKeyDown(KeyCode.RightArrow))
        {
            TurnRight();
        }
        else if ( Input.GetKeyDown(KeyCode.LeftArrow))
        {
            TurnLeft();
        }
    }

    public void Angle(int dir)
    {
        this.dir = dir;
    }
    private void TurnRight()
    {
        if (dir < 1)
        {
            dir += 1;
            StartCoroutine(Go(90f));
        }
    }

    private void TurnLeft()
    {
        if (dir > -1)
        {
            dir -= 1;
            StartCoroutine(Go(-90f));
        }
    }

    private IEnumerator Go(float angle)
    {
        Quaternion targetRot =transform.rotation *  Quaternion.Euler(0,  angle, 0);
        while (Quaternion.Angle(transform.rotation, targetRot) > 0.1f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, _dirspeed * Time.deltaTime);
            yield return null;
        }
        transform.rotation = targetRot;
    }

}
