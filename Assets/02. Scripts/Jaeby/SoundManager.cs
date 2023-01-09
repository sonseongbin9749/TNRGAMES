using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource bgSound;
    [SerializeField]
    private AudioClip[] bglist;

    [SerializeField]
    private AudioSource aiAudioSource; 
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        BgSoundPlay();
    }
    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        for(int i =0; i< bglist.Length; i++)
        {
            if(arg0.name == bglist[i].name)
                BgSoundPlay();
        }
    }
    public void SFXPlay(string sfxName, AudioClip audioClip)
    {
        GameObject go = new GameObject( sfxName+"Sound");
        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play();

        Destroy(go,audioClip.length);
    }
    public void Mute()
    {
        Debug.Log("¹ÂÆ®½ÇÇà");
        aiAudioSource.volume = 0f;
        bgSound.volume = 0f;
        aiAudioSource.mute = true;
        bgSound.mute = true;
    }
    public void BgSoundPlay()
    {
        bgSound.mute = false;
        aiAudioSource.mute = true;
        
        
    }
    public void AIBgSoundPlay()
    {
        bgSound.mute = true;
        aiAudioSource.mute = false;
        
    }

    public void AllSoundPlay()
    {
        bgSound.mute = true;
        aiAudioSource.mute = true;
    }
}
