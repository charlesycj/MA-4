using System;
using System.Collections.Generic;
using UnityEngine;

public class CoinCount : MonoBehaviour
{
    public CarpetArrangement carpetArrangement;
    public TurnPhase turnPhase;
    private int gridSize = 7;
    
    public int[] coin ={ 1, 1, 1, 1 } ; //플레이어의 배열
    public bool[] isBankrupt = new bool[4]; //각 플레이어의 파산 여부
    int bankruptCount = 0; //몇 번째 파산인지 추적
    void Start()
    {
        carpetArrangement = FindObjectOfType<CarpetArrangement>();
        if (carpetArrangement == null)
        {
            Debug.LogError("CarpetArrangement 객체를 찾을 수 없습니다!");
        }
    }

    public void Arrived(int playerNumber, int playerX, int playerZ)
    {
        int coins = GetConnectedCarpetCount(playerX, playerZ);
        GiveCoins(playerNumber, coins);
    }

    private int GetConnectedCarpetCount(int playerX, int playerZ)
    {
        int startX = playerX + 3;
        int startZ = playerZ + 3;
        if (!IsValid(startX, startZ)) return 0;
        
        int owner = carpetArrangement.whosground[startX, startZ] % 10;
        bool[,] visited = new bool[gridSize, gridSize];
        return CountConnectedCarpets(startX, startZ, owner, visited);
    }

    private int CountConnectedCarpets(int x, int z, int owner, bool[,] visited)
    {
        int count = 0;
        Queue<(int, int)> queue = new Queue<(int, int)>();
        queue.Enqueue((x, z));
        visited[x, z] = true;
        
        int[] dx = { 1, -1, 0, 0 };
        int[] dz = { 0, 0, 1, -1 };
        
        while (queue.Count > 0)
        {
            var (curX, curZ) = queue.Dequeue();
            count++;
            
            for (int i = 0; i < 4; i++)
            {
                int nextX = curX + dx[i];
                int nextZ = curZ + dz[i];
                
                if (IsValid(nextX, nextZ) && !visited[nextX, nextZ] && carpetArrangement.whosground[nextX, nextZ] % 10 == owner)
                {
                    queue.Enqueue((nextX, nextZ));
                    visited[nextX, nextZ] = true;
                }
            }
        }
        return count;
    }

    private bool IsValid(int x, int z)
    {
        return x >= 0 && x < gridSize && z >= 0 && z < gridSize;
    }

    private void GiveCoins(int playerNumber, int amount)
    {
        int currentPlayer = turnPhase.CurrentPlayerIndex;
    

        // 밟은 카펫의 주인이 현재 플레이어와 같다면 실행하지 않음
        if (playerNumber == currentPlayer)
        {
            Debug.Log($"플레이어P{currentPlayer + 1}은(는) 자신의 카펫을 밟았으므로 코인을 지급하지 않습니다.");
            return;
        }

        // 이미 파산한 플레이어라면 아무 일도 하지 않음
        if (isBankrupt[currentPlayer])
        {
            Debug.Log($"플레이어P{currentPlayer + 1}은(는) 이미 파산하여 코인을 지급할 수 없습니다.");
            return;
        }

        // 현재 플레이어가 코인을 지급해야 하는데, 코인이 부족한 경우
        int payment = amount;
        if (coin[currentPlayer] < amount)
        {
            payment = coin[currentPlayer]; // 가진 코인만큼만 지급
            isBankrupt[currentPlayer] = true; // 파산 처리
            Debug.Log($"플레이어P{currentPlayer + 1} 파산! 남은 {payment}코인만 지급하고 게임에서 제외됩니다.");
        }

        // 현재 플레이어의 코인 차감
        coin[currentPlayer] -= payment;

        // 코인을 받는 플레이어 증가
        coin[playerNumber] += payment;

        Debug.Log($"플레이어P{currentPlayer + 1} → 플레이어P{playerNumber + 1}에게 {payment}코인 지급");
        Debug.Log($"플레이어P{currentPlayer + 1}의 현재 코인: {coin[currentPlayer]} | 플레이어P{playerNumber + 1}의 현재 코인: {coin[playerNumber]}");
        
        if (isBankrupt[currentPlayer])
        {
            // 첫 번째 파산: -3, 두 번째 파산: -2, 세 번째 파산: -1
            int[] bankruptCoinValues = { -3, -2, -1 };

            if (bankruptCount < 3) 
            {
                coin[currentPlayer] = bankruptCoinValues[bankruptCount]; // 코인 값 변경
                bankruptCount++; // 파산 횟수 증가
            }

            HandleBankruptPlayer(currentPlayer);
            carpetArrangement.RemoveAllCarpetsOfPlayer(currentPlayer);
        }
    }

    private void HandleBankruptPlayer(int playerIndex)
    {
        Debug.Log($"플레이어P{playerIndex + 1}은(는) 파산하여 게임에서 더 이상 진행할 수 없습니다.");
        turnPhase.PlayerCheck[playerIndex] = true;
        
        // 만약 현재 플레이어가 파산한 경우, 즉시 다음 턴으로 넘어감
        if (playerIndex == turnPhase.CurrentPlayerIndex)
        {
            Debug.Log($"플레이어P{playerIndex + 1}의 턴을 건너뜁니다.");
            turnPhase.NextTurn(); // 턴을 넘김
        }
    }
}
