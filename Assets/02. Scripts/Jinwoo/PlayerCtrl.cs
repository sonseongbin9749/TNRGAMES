using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class PlayerCtrl : MonoSingleton<PlayerCtrl>
{
    #region FLOAT , INT

    [SerializeField] private float moveSpeed = 25f;

    private float stamina = 150f;

    private float RecoveryTime = 0;

    private float maxouterline = 5f;

    public float duration = 1.0F;

    private float realSpeed = 0f;

    private int escapeTouch = 0;

    private float StaminaRechargingTime = 0f;

    #endregion
    #region GAMEOBJECT
    [SerializeField] private GameObject cameraObj = null;
    [SerializeField] private GameObject escapeButton = null;
    [SerializeField] private GameObject dontTouchPanel = null;
    #endregion
    #region BOOL

    private bool shaking = false;

    private bool deading = false;

    public bool ismeeting = false;

    private bool isSprint = false;


    #endregion
    [SerializeField] private UnityEngine.Rendering.Universal.Light2D light2d;

    [SerializeField] private Slider mpImage;
    
    [SerializeField] private Sprite[] deadSprite = null;
    
    [SerializeField] private Image deadPanelImage = null;

    [SerializeField] private DeadImageMove imageMove = null;

    [SerializeField] private AudioClip pipi = null;
    
    [SerializeField] private AudioClip srr = null;
    
    private Rigidbody2D rb;

    private Animator anim;

    public Color color = Color.red;
    
    private Vector3 originPos;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        light2d.pointLightInnerAngle = 360;
        
        light2d.pointLightOuterAngle = 360;
        
        light2d.color = Color.white;

        realSpeed = moveSpeed;
    }
    private void FixedUpdate()
    {
        Stamina();

        if (isSprint && stamina > 0 && rb.velocity.x + rb.velocity.y != 0)
        {
            
            RunMove();
            PlayerAnime();
            stamina -= 0.3f;

            if (maxouterline > 1f)
            {
                maxouterline -= 0.01f;
                if (maxouterline <= 1f)
                {
                    maxouterline = 1f;
                }
                light2d.pointLightOuterRadius = maxouterline;
            }

            mpImage.value = Mathf.Lerp(mpImage.value, stamina / 150f, Time.deltaTime * 10);
            if(stamina == 0)
            {
                
            }
        }

        else
        {
            Move();
            PlayerAnime();
            Recovery();
            if (maxouterline <= 5f)
            {
                maxouterline += 0.03f;
                light2d.pointLightOuterRadius = maxouterline;
            }
            mpImage.value = Mathf.Lerp(mpImage.value, stamina / 150f, Time.deltaTime * 10);

        }

        if (ismeeting == true)
        {
            LightColorChange();
            int a = Random.Range(1, 3);
            switch (a)
            {
                case 1:
                    if (maxouterline > 1f)
                    {
                        maxouterline -= 0.5f;
                        light2d.pointLightOuterRadius = maxouterline;
                    }
                    break;
                case 2:
                    if (maxouterline <= 5f)
                    {
                        maxouterline += 0.5f;
                        light2d.pointLightOuterRadius = maxouterline;
                    }
                    break;
                default:
                    break;
            }


        }
        else if (ismeeting == false)
        {
            light2d.color = Color.white;
        }

        if (Input.GetKey(KeyCode.F))
        {
            LightItem();
        }


    }
    public void SprintOn()
    {
        
        isSprint = true;
        StaminaRechargingTime = 0f;
        
    }
    public void SprintOff()
    {
        isSprint = false;
    }


    public void LightItem()
    {
        light2d.pointLightOuterRadius = 20f;
        float t = Mathf.PingPong(Time.time, 0.1f) / 0.1f;
        light2d.intensity = Mathf.PingPong(Time.time, 3f) / t;
    }
    public void Trap()
    {
        StartCoroutine(IsTrap());
    }
    private IEnumerator IsTrap()
    {
        moveSpeed = 0f;
        yield return new WaitForSeconds(2f);
        moveSpeed = realSpeed;
    }
    public void FirstDead()
    {
        ismeeting = true;
        escapeButton.SetActive(true);
        dontTouchPanel.SetActive(true);

        StartCoroutine(Shake(0.3f, 3f));
        StartCoroutine(EscapeCheck());
    }
    public bool GetIsMeeting()
    {
        return ismeeting;
    }
    public IEnumerator Shake(float _amount, float _duration)
    {
        originPos = cameraObj.transform.position;
        shaking = true;
        float timer = 0f;
        while (timer <= _duration)
        {
            cameraObj.transform.position = (Vector3)Random.insideUnitCircle * _amount + originPos;

            timer += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        shaking = false;
        cameraObj.transform.position = originPos;
    }
    public void Dead()
    {
        StartCoroutine(DeadScene());
    }
    private IEnumerator DeadScene()
    {
        SoundManager.instance.Mute();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        deadPanelImage.gameObject.SetActive(true);
        deadPanelImage.sprite = deadSprite[0];
        imageMove.StartCoroutine(Shake(0.5f,1f));
        yield return new WaitForSeconds(1f);
        deadPanelImage.sprite = deadSprite[1];
        SoundManager.instance.SFXPlay("srrr", srr);
        yield return new WaitForSeconds(1f);
        SoundManager.instance.SFXPlay("srrr", pipi);
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("StartScene");


    }
    public void ClickToEscape()
    {
        escapeTouch++;
    }
    [SerializeField]
    private AgentScripts enemy = null;
    private IEnumerator EscapeCheck()
    {
        yield return new WaitForSeconds(3f);
        if (escapeTouch >= 10)
        {
            escapeButton.SetActive(false);
            dontTouchPanel.SetActive(false);
            ismeeting = false;
            yield return new WaitForSeconds(1f);
            deading = false;
            enemy.DeadingChange();

        }
        else
        {
            StartCoroutine(DeadScene());
        }

    }
    private void LightColorChange()
    {
        float t = Mathf.PingPong(Time.time, duration) / duration;
        light2d.color = Color.Lerp(Color.white, color, t);
    }
    void RunMove()
    {
        rb.velocity = new Vector2(SimpleInputNamespace.Joystick.Instance.m_value.x * moveSpeed * 2f,
            SimpleInputNamespace.Joystick.Instance.m_value.y * moveSpeed * 2f);

    }
    void Move()
    {
        rb.velocity = new Vector2(SimpleInputNamespace.Joystick.Instance.m_value.x * moveSpeed,
            SimpleInputNamespace.Joystick.Instance.m_value.y * moveSpeed);

    }
    void PlayerAnime()
    {
        if (Mathf.Approximately(SimpleInputNamespace.Joystick.Instance.m_value.x, 0) &&
            Mathf.Approximately(SimpleInputNamespace.Joystick.Instance.m_value.y, 0))
        {
            anim.SetBool("ismove", false);
        }
        else
        {
            anim.SetBool("ismove", true);
        }
        anim.SetFloat("inputx", SimpleInputNamespace.Joystick.Instance.m_value.x);
        anim.SetFloat("inputy", SimpleInputNamespace.Joystick.Instance.m_value.y);
    }
    private void Stamina()
    {
        if (stamina <= 0)
        {
            stamina = 0;
        }
        if (stamina >= 150)
        {
            stamina = 150;
        }
    }
    private void Recovery()
    {
        if (stamina >= 150)
        {
            RecoveryTime = 0;
            
            return;
        }

        RecoveryTime += Time.fixedDeltaTime;
        StaminaRechargingTime += Time.deltaTime;
        if (RecoveryTime > 0.5f)
        {
            
            if (maxouterline <= 5f)
            {
                maxouterline += 0.03f;
                light2d.pointLightOuterRadius = maxouterline;
            }
               
        }
        if(StaminaRechargingTime >= 5f && !isSprint)
        {
            stamina += 0.2f;
        }

    }
    
    
}
