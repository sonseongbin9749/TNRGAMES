using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator animator;
    public Transform player;
    public float speed;
    public Vector2 home;

    public float atkCooltime = 4;
    public float atkDelay;

    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        home = transform.position;
    }
    private void Update()
    {
        spriteRenderer.sortingOrder = (Mathf.RoundToInt(transform.position.y) * -1);
        if (atkDelay>=0)
        {
            atkDelay -= Time.deltaTime;
        }
    }
    public void DirectionEnemy(float target, float baseobj)
    {
        if(target < baseobj)
        {
            animator.SetFloat("Direction", -1);
        }
        else
        {
            animator.SetFloat("Direction", 1);
        }
    }

    
}
