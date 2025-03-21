using System;
using UnityEngine;


public class Camera : MonoBehaviour
{
   //카메라 m키를 눌러 탑뷰와 1인칭 정면을 보는 스크립트
   
   [SerializeField] public GameObject TopViewCamera;
   [SerializeField] public GameObject BackViewCamera;

   private void Start()
   {
      BackViewCamera.SetActive(true);
      TopViewCamera.SetActive(false);
   }

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.M))
      {
         if (BackViewCamera.activeSelf && !TopViewCamera.activeSelf)
         {
            BackViewCamera.SetActive(false);
            TopViewCamera.SetActive(true);
         }
         else if (!BackViewCamera.activeSelf && TopViewCamera.activeSelf)
         {
            BackViewCamera.SetActive(true);
            TopViewCamera.SetActive(false);
         }
      }
   }
}
