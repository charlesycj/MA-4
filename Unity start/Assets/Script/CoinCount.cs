using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class CoinCount : MonoBehaviour
{
    private Move move;
    private CarpetArrangement carpetArrangement;

    void Start()
    {
        // CarpetArrangement를 찾거나 할당
        carpetArrangement = FindObjectOfType<CarpetArrangement>();  // 또는 적절한 방법으로 초기화
        if (carpetArrangement == null)
        {
            Debug.LogError("CarpetArrangement 객체를 찾을 수 없습니다!");
        }
    }
    
    private int P1coin = 30;
    private int P2coin = 30;
    private int P3coin = 30;
    private int P4coin = 30;

    private int accountcoin; //코인 계산 로직
    
    // 카펫별 / 플레이어 별 상호작용
    //P1==0 P2==1 P3==2 P4==3
    public void ArrivedP1() //카펫의 주인 P1
    {
        if (carpetArrangement.currentPlayerIndex == 0)
        {
           
        }

        if (carpetArrangement.currentPlayerIndex == 1)
        {
            P1coin += 3;
            P2coin -= 3;
            Debug.Log($"P1 coin: {P1coin},P2 coin: {P2coin}");
                
        }
        else if (carpetArrangement.currentPlayerIndex == 2)
        {           
            P1coin += 3;
            P3coin -= 3;
            Debug.Log($"P1 coin: {P1coin},P3 coin: {P3coin}");
        }
        else  if (carpetArrangement.currentPlayerIndex == 3)
        {
            P1coin += 3;
            P4coin -= 3;
            Debug.Log($"P1 coin: {P1coin},P4 coin: {P4coin}");
        }
    }
    public void ArrivedP2()//카펫의 주인 P2
    {
        if (carpetArrangement.currentPlayerIndex != 1)
        {
          
        }

        if (carpetArrangement.currentPlayerIndex == 0)
        {
            P2coin += 3;
            P1coin -= 3;
            Debug.Log($"P2 coin: {P2coin},P1 coin: {P1coin}");
        }
        else if (carpetArrangement.currentPlayerIndex == 2)
        {
            P2coin += 3;
            P3coin -= 3;
            Debug.Log($"P2 coin: {P2coin},P3 coin: {P3coin}");
        }
        else if (carpetArrangement.currentPlayerIndex == 3)
        {
            P2coin += 3;
            P4coin -= 3;
            Debug.Log($"P2 coin: {P2coin},P4 coin: {P4coin}");
        }
        
    }
    public void ArrivedP3() //카펫의 주인 P3
    {
        if (carpetArrangement.currentPlayerIndex == 2)
        {
           
        }

        else if (carpetArrangement.currentPlayerIndex == 0)
        {
            P3coin += 3;
            P1coin -= 3;
            Debug.Log($"P3 coin: {P3coin},P1 coin: {P1coin}");
            
        }
        else if (carpetArrangement.currentPlayerIndex == 1)
        {
            P3coin += 3;
            P2coin -= 3;
            Debug.Log($"P3 coin: {P3coin},P2 coin: {P2coin}");
        }
        else if (carpetArrangement.currentPlayerIndex == 3)
        {
            P3coin += 3;
            P4coin -= 3;
            Debug.Log($"P3 coin: {P3coin},P3 coin: {P4coin}");
        }
        
    }
    public void ArrivedP4() //카펫의 주인 P4
    {
        if (carpetArrangement.currentPlayerIndex == 3)
        {
           
        }

        else if (carpetArrangement.currentPlayerIndex == 0)
        {
            P4coin += 3;
            P1coin -= 3;
            Debug.Log($"P4 coin: {P4coin},P1 coin: {P1coin}");
        }
        else if (carpetArrangement.currentPlayerIndex == 1)
        {
            P4coin += 3;
            P2coin -= 3;
            Debug.Log($"P4 coin: {P4coin},P2 coin: {P2coin}");
        }
        else if (carpetArrangement.currentPlayerIndex == 2)
        {
            P4coin += 3;
            P3coin -= 3;
            Debug.Log($"P4 coin: {P4coin},P3 coin: {P4coin}");
        }
        
    }


  
}