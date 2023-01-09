using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ending : MonoSingleton<Ending>
{
    [SerializeField]
    private Image endingImage = null;
    [SerializeField]
    private Sprite[] endingSprite = null;
    [SerializeField]
    private Text endingText = null;
    [SerializeField]
    private string[] endingString = null;
    public bool isending = false;
    private void Start()
    {
        isending = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isending = true;
            StartCoroutine(StartEnding());

        }
    }
    public IEnumerator StartEnding()
    {
        endingImage.gameObject.SetActive(true);
        for(int i =0; i<2;i++)
        {
            endingImage.sprite = endingSprite[i];
            endingText.text = endingString[i];
            yield return new WaitForSeconds(5f);
        }
        isending = false;
        SceneManager.LoadScene("StartScene");
    }
}
