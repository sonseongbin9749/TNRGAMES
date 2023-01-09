using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMoveX;
    public int nextMoveY;
    public int speed = 10;
    Animator anim;
    SpriteRenderer spriteRenderer;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Think();

        Invoke("Think", 2);
    }


    void FixedUpdate()
    {
        //Move
        rigid.velocity = new Vector2(nextMoveX*speed, rigid.velocity.y);

        //Platform Check
        Vector2 frontVec = new Vector2(rigid.position.x + nextMoveX * 0.5f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null)
        {
            Turn();
        }
    }

    //Àç±Í ÇÔ¼ö
    void Think()
    {
        nextMoveX = Random.Range(-1, 2);

        //Sprite Animation
        //anim.SetInteger("WalkSpeed", nextMoveX); 
        //Flip Sprite
        if (nextMoveX != 0)
            spriteRenderer.flipX = nextMoveX == 1;

        //Recursive
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);

    }

    void Turn()
    {
        nextMoveX *= -1;
        spriteRenderer.flipX = nextMoveX == 1;

        CancelInvoke();
        Invoke("Think", 2);
    }
}
