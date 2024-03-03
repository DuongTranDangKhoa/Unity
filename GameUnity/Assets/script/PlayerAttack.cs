using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hit something");
        if(collision.CompareTag("Enemy"))
        {
            Debug.Log("Hit");
        }
    }
}
