using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tuto : MonoBehaviour
{
    public Image[] image;
    private int count = 0;
    

    private void OnEnable()
    {
        image = GetComponentsInChildren<Image>();
        count = image.Length - 1;
    }
    public void TutoImage()
    {

       if (gameObject.activeSelf == false)
       {
           gameObject.SetActive(true);
       }
       image[--count].gameObject.SetActive(false);

       if(count == 0)
       {
           for(int i = 0; i < image.Length - 1; i++)
            {
                image[i].gameObject.SetActive(true);
            }
           gameObject.SetActive(false);
           return;
       }
        

    }

}
