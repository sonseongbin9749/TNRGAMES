using UnityEngine;
using UnityEngine.UI;
public class InGameStart : MonoBehaviour
{
    [SerializeField]
    private Text animationText = null;
    [SerializeField]
    private Sprite dogSprite = null;
    [SerializeField]
    private Sprite nurseSprite = null;
    [SerializeField]
    private Image spriteImage = null;
    [SerializeField]
    private Text nameText = null;
    [SerializeField]
    private string[] texts = null;
    [SerializeField]
    private bool[] isNurse;
    [SerializeField]
    private GameObject animationUISet = null;
    [SerializeField]
    private GameObject donTouchPanel = null;
    int index = 0;
    private void Start()
    {
        NextAnimation();
    }
    private void AlphaZero()
    {
        spriteImage.color = new Color(0f, 0f, 0f, 0f);
    }
    private void AlphaOn()
    {
        spriteImage.color = new Color(1f, 1f, 1f, 1f);
    }
    public void NextAnimation()
    {
        donTouchPanel.SetActive(true);
        Time.timeScale = 0f;
        if (texts.Length > index)
            index++;
        else
        {
            animationUISet.SetActive(false);
            donTouchPanel.SetActive(false);
            Time.timeScale = 1f;
        }
        if (isNurse[index - 1])
        {
            spriteImage.sprite = nurseSprite;
            AlphaZero();
            nameText.text = "간호사";
        }
        else
        {
            spriteImage.sprite = dogSprite;
            AlphaOn();
            nameText.text = "강아지";
        }
        animationText.text = texts[index - 1];

    }


}
