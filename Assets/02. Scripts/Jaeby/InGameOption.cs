using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class InGameOption : MonoBehaviour
{
    [SerializeField]
    private GameObject optionPanel = null;
    [SerializeField]
    private GameObject noTouchPanel = null;
    [SerializeField]
    private GameObject dooropen;
    [SerializeField]
    private SpriteRenderer playerSprite = null;
    [SerializeField]
    private GameObject[] numberPadPanel = null;
    [SerializeField]
    private Text numberText = null;
    [SerializeField]
    private AudioSource c;

    
    [SerializeField] private Sprite[] speakerSprites = null;
    [SerializeField] private Image speakerImage = null;

    private Inventory inven;

    private string passwordNumber = null;
    [SerializeField]
    private string[] password = null;
    [SerializeField]
    private Slider volSlider = null;
    [SerializeField]
    private AudioSource bgAudio = null;
    [SerializeField]
    private AudioSource aiBgAudio = null;
    [SerializeField]
    private GameObject[] checkPanel = null;
    [SerializeField]
    private AudioClip buttonClickSound = null;
    private bool isOption = false;
    public void CheckPanelOn(int i)
    {
        checkPanel[i].SetActive(true);
    }
    public void CheckPanelOff(int i)
    {
        checkPanel[i].SetActive(false);
    }
    private void Start()
    {
        inven = FindObjectOfType<Inventory>();
    }
    private void Update()
    {
        
        if (isOption)
        {
            bgAudio.volume = volSlider.value;
            aiBgAudio.volume = volSlider.value;
        }
        if (bgAudio.volume == 0f && aiBgAudio.volume == 0f)
            speakerImage.sprite = speakerSprites[0];
        else
            speakerImage.sprite = speakerSprites[1];
        if (Input.GetKey(KeyCode.Escape))
        {
            OptionPanelOn();
        }
    }
    public void SoundOnOff()
    {
        if (bgAudio.volume > 0f && aiBgAudio.volume > 0f )
        {
            speakerImage.sprite = speakerSprites[0];
            bgAudio.volume = 0f;
            aiBgAudio.volume = 0f;
            volSlider.value = 0f;
        }
        else 
        {
            speakerImage.sprite = speakerSprites[1];
            bgAudio.volume = 1f;
            aiBgAudio.volume = 1f;
            volSlider.value = 1f;
        }
    }
    public void GameQuit()
    {
        Application.Quit();
    }
    public void GoStartScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScene");
    }
    public void NumberPanelOn(int num)
    {
        numberPadPanel[num].SetActive(true);    
        Time.timeScale = 0f;
    }
    public void NumberPanelOff(int num)
    {
        passwordNumber = "";
        numberText.text = "";
        numberPadPanel[num].SetActive(false);
        Time.timeScale = 1f;
    }
    
    public void NumberClick(int num)
    {
        SoundManager.instance.SFXPlay("ButtonClick", buttonClickSound);
        passwordNumber = passwordNumber += num.ToString();
        numberText.text = passwordNumber;
    }
    public void PasswordCheck(int num)
    {
        if(password[num] == passwordNumber)
        {
            Debug.Log("¼º°ø");
            dooropen.gameObject.SetActive(false);
            inven.MoonOpen();
            c.Play();

        }
    }
    public void OptionPanelOn()
    {
        isOption = true;
        optionPanel.SetActive(true);
        noTouchPanel.SetActive(true);
        playerSprite.enabled = false;
        Time.timeScale = 0f;
    }
    public void OptionPanelDown()
    {
        isOption = false;
        optionPanel.SetActive(false);
        noTouchPanel.SetActive(false);
        playerSprite.enabled = true;
        Time.timeScale = 1f;
    }
}
