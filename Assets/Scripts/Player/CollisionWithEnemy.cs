using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithEnemy : MonoBehaviour
{
    private Player _player;

    private void Start()
    {
        _player = GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
            OnGameOver();
    }

    public void OnGameOver()
    {
        Time.timeScale = 0;
        Debug.Log("You died.");
    }
}
