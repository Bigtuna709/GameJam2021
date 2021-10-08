using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public PlayerMovement player;
    public GameObject home;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.transform.position = home.transform.position;
        }
    }
}
