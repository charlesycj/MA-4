using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    

    private void Start()
    {
        //for (var i = 0;  i < 30; ++i)
        //{
        //    spawnCoin();
        //}
        InvokeRepeating(nameof(spawnCoin), 1f, 0.05f);
    }

    public void spawnCoin()
    {
        Instantiate(coinPrefab, transform.position, Quaternion.identity);
    }
}
