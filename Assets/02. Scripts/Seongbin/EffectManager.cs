using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] GameObject click, CloseDoor;
    //[SerializeField] Animator ani;

     bool ispick, isopen,isclick = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            isopen = true;
            // ani.Play("Open1");
            click.gameObject.SetActive(true);
        }
        isclick = false;
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            click.gameObject.SetActive(false);
            isclick = false;
            // ani.Play("Open1");

        }
    }
    
    
    public void Btnclick()
    {
        isclick = true;
        isopen = true;
        if (isclick == true)
        {
            click.gameObject.SetActive(false);
            CloseDoor.gameObject.SetActive(false);
        }
    }

}
