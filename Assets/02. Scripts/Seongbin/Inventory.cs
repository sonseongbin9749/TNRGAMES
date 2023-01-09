using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    [SerializeField] private GameObject click1s, click2s, click3s, click4s, click1, click2, click3, key1, key2, key3, door1, door2, door3, door4, door5, password;
    [SerializeField] private Image key1s, key2s, key3s;
    [SerializeField] private Text txt1, txt2, txt3, txt1s, txt2s, txt3s;
    [SerializeField] private AudioSource c;
    [SerializeField] private Animator ani,ani1,ani2; 

    private bool iskey1click, iskey2click, iskey3click, iscanopen1, iscanopen2, iscanopen3 = false;

    private int a = 0, b = 1;
    
    private void Update()
    {
        if (iskey1click == true)
            checksetactive1();

        if (iskey2click == true)
            checksetactive2();
        if (iskey3click == true)
            checksetactive3();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("firstkey"))
        {
            click1.gameObject.SetActive(true);


        }
        else if (collision.gameObject.tag.Equals("secondkey"))
        {
            click2.gameObject.SetActive(true);

        }
        else if (collision.gameObject.tag.Equals("lastkey"))
        {
            click3.gameObject.SetActive(true);  

        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("firstkey"))
        {
            click1.gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag.Equals("secondkey"))
        {
            click2.gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag.Equals("lastkey"))
        {
            click3.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("체크중");
        if (collision.gameObject.tag.Equals("firstdoor"))
        {
            click1s.gameObject.SetActive(true);
        }
        Debug.Log("체크중");
        if (collision.gameObject.tag.Equals("seconddoor"))
        {
            click2s.gameObject.SetActive(true);
        }
        Debug.Log("체크중");
        if (collision.gameObject.tag.Equals("lastdoor"))
        {
            click3s.gameObject.SetActive(true);
        }
        if (collision.gameObject.tag.Equals("finaldoor"))
        {
            click4s.gameObject.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("firstdoor"))
        {
            click1s.gameObject.SetActive(false);
        }
        if (collision.gameObject.tag.Equals("seconddoor"))
        {
            click2s.gameObject.SetActive(false);
        }
        if (collision.gameObject.tag.Equals("lastdoor"))
        {
            click3s.gameObject.SetActive(false);
        }
        if (collision.gameObject.tag.Equals("finaldoor"))
        {
            click4s.gameObject.SetActive(false);
        }
    }

    public void MoonOpen()
    {
        ani2.Play("doornotopen");   
    }

    public void key1click()
    {
        Debug.Log("a");
        iskey1click = true;
        ani1.Play("doornotopen");

    }
    public void key2click()
    {
        Debug.Log("b");
        iskey2click = true;
        ani1.Play("doornotopen");
    }
    public void key3click()
    {
        Debug.Log("c");
        iskey3click = true;
        ani1.Play("doornotopen");
    }
    public void door1dopen()
    {
        
        
            if (iskey1click == true)
            {
                door1.gameObject.SetActive(false);
            ani2.Play("doornotopen");
            c.Play();
                
            }
            else
            {
                Debug.Log("애니메이션 실행");
                ani.Play("doornotopen");
            }
        
    }
    public void door2dopen()
    {


        if (iskey2click == true)
        {
            door2.gameObject.SetActive(false);
            ani2.Play("doornotopen");
            c.Play();
        }
        else
        {
            Debug.Log("애니메이션 실행");
            ani.Play("doornotopen");
        }

    }
    public void door3dopen()
    {


        if (iskey3click == true)
        {
            door3.gameObject.SetActive(false);
            door4.gameObject.SetActive(true);
            ani2.Play("doornotopen");
            c.Play();
        }
        else
        {
            ani.Play("doornotopen");
        }
    }
    public void door4open()
    {
        password.gameObject.SetActive(true);
    }


    private void checksetactive1()
    {
            Debug.Log("checking1");
            click1.gameObject.SetActive(false);
            key1.gameObject.SetActive(false);
            key1s.gameObject.SetActive(true);
        txt1.gameObject.SetActive(false);
        txt1s.gameObject.SetActive(true);
   

    }
    private void checksetactive2()
    {
            Debug.Log("checking2");
            click2.gameObject.SetActive(false);
            key2.gameObject.SetActive(false);
            key2s.gameObject.SetActive(true);
        txt2.gameObject.SetActive(false);
        txt2s.gameObject.SetActive(true);
   
    }
    private void checksetactive3()
    {
            Debug.Log("checking3");
            click3.gameObject.SetActive(false);
            key3.gameObject.SetActive(false);
            key3s.gameObject.SetActive(true);
        txt3.gameObject.SetActive(false);
        txt3s.gameObject.SetActive(true);
     
    }

    
}

