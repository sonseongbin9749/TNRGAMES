    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LayerSetting : MonoBehaviour
{

    Tilemap tilemap;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        //spriteRenderer.sortingOrder = (Mathf.RoundToInt(transform.position.y)*-1);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy")|| collision.CompareTag("Col"))
        {
            tilemap.color = new Color(1, 1, 1, 0.5f);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")|| collision.CompareTag("Col"))
        {
            tilemap.color = new Color(1, 1, 1, 1f);
        }
    }
   
}
