using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int _collectedCoins = 0;

    public int CollectedCoins { get => _collectedCoins; private set => _collectedCoins = value; }

    public void CollectCoin(int collectedCoin)
    {
        CollectedCoins += collectedCoin;
    }
}
