using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator animator;
    protected Collider2D coll;

    [SerializeField] private float knockBackForce;
    [SerializeField] private Vector2 knockBackDir;

    [SerializeField] private int damage;

    [SerializeField] private float maxHealth;
    private float currentHealth;

    [SerializeField] protected Slider healthBarSlider;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();

        currentHealth = maxHealth;

        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = currentHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Player player = collision.collider.GetComponent<Player>();

            if (player != null)
            {
                player.TakeDamage(damage);
            }

        }
    }

    public void OnBeingHitByPlayer(Vector2 attackPosition)
    {
        animator.SetTrigger("hit");

        float facingCollision = attackPosition.x - transform.position.x > 0 ? -1 : 1;

        Vector2 realKnockbackDir = new Vector2(knockBackDir.x * facingCollision, knockBackDir.y);

        rb.AddForce(realKnockbackDir * knockBackForce, ForceMode2D.Impulse);
    }

    public void OnBeingHitByFlyingObject(float velocityX)
    {
        animator.SetTrigger("hit");

        float facingCollision = velocityX > 0 ? 1 : -1;

        Vector2 realKnockbackDir = new Vector2(knockBackDir.x * facingCollision, knockBackDir.y);

        rb.AddForce(realKnockbackDir * knockBackForce, ForceMode2D.Impulse);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBarSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            animator.SetBool("dead", true);
        }

    }

    private void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    private void DeadAnimation()
    {
        rb.AddForce(Vector2.up * 4, ForceMode2D.Impulse);
        coll.enabled = false;
        healthBarSlider.gameObject.SetActive(false);
    }
}
