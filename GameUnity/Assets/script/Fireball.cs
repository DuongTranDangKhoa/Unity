using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private float moveSpeed;
    private Player player;
    private float facingDir;

    private Rigidbody2D rb;

    public void SetUpFireball(Player _player, float _moveSpeed, int _facingDir)
    {
        facingDir = _facingDir;

        transform.localScale = new Vector2(facingDir > 0 ? 1 : -1, transform.localScale.y);
        
        player = _player;
        moveSpeed = _moveSpeed;

        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(transform.right.x * moveSpeed * facingDir, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Enemy enemy;
            if(collision.TryGetComponent(out enemy))
            {
                enemy.OnBeingHitByFlyingObject(rb.velocity.x);
                enemy.TakeDamage(player.fireballDamage);
            }
        }

        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);

        Debug.Log("Collide");
    }


}
