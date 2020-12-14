using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : ObjectPool
{
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private Player _player;
    [SerializeField] private Transform[] _spawnPoints;

    private void Start()
    {
        Initialize(_coinPrefab);
    }

    

    private void Update()
    {
        if (_player.CollectedCoins >= 0)
        {
            if (TryGetObject(out GameObject coin))
            {
                int spawnPointNumber = Random.Range(0, _spawnPoints.Length);

                SetCoin(coin, _spawnPoints[spawnPointNumber].position);
            }
        }
    }

    private void SetCoin(GameObject coin, Vector3 spawnPoint)
    {
        coin.SetActive(true);
        coin.transform.position = spawnPoint;
    }

}
