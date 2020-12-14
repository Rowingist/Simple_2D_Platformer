using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private int _worth = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Player player))
        {
            player.CollectCoin(_worth);
            Dissapear();
        }
    }

    private void Dissapear()
    {
        gameObject.SetActive(false);
    }
}
