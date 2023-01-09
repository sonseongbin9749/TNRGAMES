using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAni : MonoBehaviour
{
    public Animator ani;
    void Start()
    {
        Debug.Log("Ω√¿€");
        ani = GetComponent<Animator>();
        ani.enabled = true;
    }

}
