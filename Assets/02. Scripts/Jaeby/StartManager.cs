using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    #region 게임 오브젝트
    [SerializeField] private GameObject optionPanel = null;
    [SerializeField] private GameObject infomationPanel = null;
    [SerializeField] private GameObject noTouchPanel = null;
    [SerializeField] private GameObject characterImage = null;
    [SerializeField] private GameObject lockImage = null;
    #endregion
    #region Sprite, Image
    [SerializeField] private Sprite[] startAnimationSprite = null;
    [SerializeField] private Sprite firstStartSprite = null;
    [SerializeField] private Sprite SecondStartSprite = null;
    [SerializeField] private Sprite[] bgSprite = null;
    [SerializeField] private Sprite[] speakerSprites = null;
    [SerializeField] private Image speakerImage = null;
    [SerializeField] private Image[] startUIImage = null;
    [SerializeField] private Image bgImage = null;
    [SerializeField] private Image startAnimationImage = null;
    [SerializeField] private Image QuitPanel = null;
    #endregion
    #region 오디오
    [SerializeField] private AudioClip[] bgAudio = null;
    [SerializeField] private AudioSource audioSource = null;
    #endregion
    [SerializeField] private int isFirstStart = 0;
    [SerializeField] private Text startAnimationText = null;
    [SerializeField] private string[] startAnimationString = null;

    private bool isStart = false;
    private string firstStartKey = "FirstStart";
    [SerializeField] private Slider volSlider = null;
    [SerializeField] private Text volText = null;
    
    public void Start()
    {
        //PlayerPrefs.DeleteAll();
        isFirstStart = PlayerPrefs.GetInt(firstStartKey);
        if (isFirstStart == 1)
        {
            NextGameSet();
            StartUISet();
        }
        else
        {
            FirstGameSet();
            FirstStartUISet();
        }
        
    }

    
    private void FirstStartUISet()
    {
        for(int i =0;i <startUIImage.Length; i++)
            startUIImage[i].sprite = firstStartSprite;
        lockImage.SetActive(true);
    }
    private void StartUISet()
    {
        for (int i = 0; i < startUIImage.Length; i++)
            startUIImage[i].sprite = SecondStartSprite;
        lockImage.SetActive(false);
    }
    public void SoundOnOff()
    {
        if(audioSource.volume > 0f)
        {
            speakerImage.sprite = speakerSprites[0];
            audioSource.volume = 0f;
            volSlider.value = 0f;
        }
        else
        {
            speakerImage.sprite = speakerSprites[1];
            audioSource.volume = 1f;
            volSlider.value = 1f;
        }
    }
    private void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            GoExit();
        }
        audioSource.volume = volSlider.value;
        if(audioSource.volume == 0f)
            speakerImage.sprite = speakerSprites[0];
        else
            speakerImage.sprite = speakerSprites[1];
        int volume = (int)(audioSource.volume * 100f);
        volText.text = "볼륨\n" + volume.ToString();
    }
    private void FirstGameSet()
    {
        audioSource.clip = bgAudio[0];
        audioSource.Play();
        bgImage.sprite = bgSprite[0];
        bgImage.color = Color.white;
        characterImage.GetComponent<Image>().color = Color.white;
    }
    private void NextGameSet()
    {
        audioSource.clip = bgAudio[1];
        audioSource.Play();
        bgImage.sprite = bgSprite[1];
    }
    
    public void GoStart()
    {
        isStart = true;
        if (isFirstStart == 0)
            StartCoroutine(StartAnimation());
        else
            SceneManager.LoadScene("GameScene");
        isFirstStart = 1;
        PlayerPrefs.SetInt(firstStartKey, isFirstStart);
    }
    public void GoOption()
    {
        optionPanel.SetActive(true);
        noTouchPanel.SetActive(true);
    }
    public void ExitOption()
    {
        optionPanel.SetActive(false);
        noTouchPanel.SetActive(false);
    }
    public void GoInfomation()
    {
        infomationPanel.SetActive(true);
        noTouchPanel.SetActive(true);
    }
    public void Reminisce()
    {
        StartCoroutine(StartAnimation());
    }
    public void ExitInfomation()
    {
        infomationPanel.SetActive(false);
        noTouchPanel.SetActive(false);
    }
    public void GoExit()
    {
        QuitPanel.gameObject.SetActive(true);
    }
    public void ExitYes()
    {
        Application.Quit();
    }
    public void ExitNo()
    {
        QuitPanel.gameObject.SetActive(false);
    }

    private IEnumerator StartAnimation()
    {
        startAnimationImage.gameObject.SetActive(true);
        for(int i =0; i< startAnimationSprite.Length; i++)
        {
            if (i > 0)
            {
                startAnimationImage.sprite =  startAnimationSprite[i - 1];
                startAnimationText.text = startAnimationString[i - 1];
                startAnimationImage.sprite =  startAnimationSprite[i];
                startAnimationText.text = startAnimationString[i];
            }
            else
            {
                startAnimationImage.sprite = startAnimationSprite[i];
                startAnimationText.text = startAnimationString[i];
            }
            yield return new WaitForSeconds(5f);
        }
        startAnimationImage.gameObject.SetActive(false);
        if (isStart)
        SceneManager.LoadScene("GameScene");
    }
}
