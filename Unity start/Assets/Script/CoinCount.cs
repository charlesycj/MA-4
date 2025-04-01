using System;
using System.Collections.Generic;
using UnityEngine;

public class CoinCount : MonoBehaviour
{
    public CarpetArrangement carpetArrangement;
    private int gridSize = 7;
    
    private int P1coin = 30;
    private int P2coin = 30;
    private int P3coin = 30;
    private int P4coin = 30;

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
        int currentPlayer = carpetArrangement.currentPlayerIndex;
        switch (playerNumber)
        {
            case 0: P1coin += amount; break;
            case 1: P2coin += amount; break;
            case 2: P3coin += amount; break;
            case 3: P4coin += amount; break;
        }
        // 코인을 지불하는 플레이어
        switch (currentPlayer)
        {
            case 0: P1coin -= amount; break;
            case 1: P2coin -= amount; break;
            case 2: P3coin -= amount; break;
            case 3: P4coin -= amount; break;
        }

        Debug.Log($"Player {playerNumber + 1} receives {amount} coins from Player {currentPlayer + 1}.");
    }
}
