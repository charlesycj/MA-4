using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    public Animator panelAnimator; // 패널의 Animator
    private bool isMoving = true;
    

    
    public void OnMoveButtonClick()
    {
        isMoving = !isMoving; // 상태 반전
        panelAnimator.SetBool("isMoving", isMoving); // 애니메이션 트리거
    }
   
}
