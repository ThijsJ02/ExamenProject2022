using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : MonoBehaviour
{
    private Animator animator;
    [HideInInspector]public bool isInShell = false;

    public LayerMask playerLayer;

    private bool isFacingLeft = true;

    public float waypointLeft;
    public float waypointRight;
    public float speed;

    private void Start()
    {
        animator = GetComponent<Animator>();    
    }

    private void Update()
    {
        if (isFacingLeft && !ShouldGoInShell())
        {
            if (transform.position.x <= waypointLeft)
            {
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
                isFacingLeft = false;
            }
            else
            {
                transform.Translate(-transform.right * speed * Time.deltaTime);
            }
        }
        else if (!isFacingLeft && !ShouldGoInShell())
        {
            if (transform.position.x >= waypointRight)
            {
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
                isFacingLeft = true;
            }
            else
            {
                transform.Translate(transform.right * speed * Time.deltaTime);
            }
        }

        if (ShouldGoInShell())
        {
            isInShell = true;
            animator.SetBool("Shell", true);
        }
        else
        {
            isInShell = true;
            animator.SetBool("Shell", false);
        }
    }

    private bool ShouldGoInShell()
    {
        return Physics2D.OverlapCircle(transform.position, 4f, playerLayer);
    }
}
