using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_On_Map : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

        }
    }
}
