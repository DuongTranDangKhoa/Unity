using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nija : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    private float scaleX;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("run", false);
        scaleX = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        Vector2 scale = transform.localScale;
        if (Input.GetKey(KeyCode.A))
        {
            position.x -= 1 * Time.deltaTime;
            scale.x = -scaleX;
            animator.SetBool("run", true);

        }
        else
        if (Input.GetKey(KeyCode.D))
        {
            position.x += 1 * Time.deltaTime;
            scale.x = scaleX;
            animator.SetBool("run", true);

        }
        else
        if (Input.GetKey(KeyCode.W))
        {
            position.y += 5 * Time.deltaTime;
            transform.position = position;
            animator.SetBool("run", true);

        }
        else
        {
            animator.SetBool("run", false);
        }

        transform.localScale = scale;
        transform.position = position;
    }
}
