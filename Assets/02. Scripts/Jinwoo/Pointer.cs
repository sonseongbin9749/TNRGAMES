using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{

    public Transform m_pTarget;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        
    }

    void Update()
    {
        ItemPointer();
    }
    private void ItemPointer()
    {
        //spriteRenderer.sortingOrder = (Mathf.RoundToInt(transform.position.y) * -1);
        float width = m_pTarget.position.x - transform.position.x;
        float height = m_pTarget.position.y - transform.position.y;

        float angle = Mathf.Atan2(height, width) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle - 90);
    }

    public void ang()
    {
        gameObject.SetActive(true);
        Invoke("ang1", 4);
        
    }
    private void ang1()
    {
        gameObject.SetActive(false);
    }
}
