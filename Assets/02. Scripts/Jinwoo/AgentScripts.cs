using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentScripts : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    private NavMeshAgent agent;
    [SerializeField]
    private UnityEngine.Rendering.Universal.Light2D light2d;

    public Transform enemyTransform;
    SpriteRenderer spriteRenderer;

    [SerializeField]
    private PlayerCtrl player = null;

    private bool islightitem = false;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = 1f;

        enemyTransform = GetComponent<Transform>();

        InvokeRepeating("flood",2f, 10f);
    }
    [SerializeField]
    private AudioClip floodAudio = null;
    private void flood()
    {
        if (player.GetIsMeeting() == false)
        StartCoroutine(Flood());
    }
    private IEnumerator Flood()
    {
        float realSpeed = agent.speed;
        SoundManager.instance.SFXPlay("Flood", floodAudio);
        agent.speed = 0f;
        yield return new WaitForSeconds(floodAudio.length);
        agent.speed = realSpeed * 3f;
        yield return new WaitForSeconds(5f);
        agent.speed = realSpeed;
    }
    private bool deading = false;
    int deadCount = 0;
    public void DeadingChange()
    {
        deading = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (deading) return;
        deading = true;
        //moveSpeed = 0f;
        if (collision.CompareTag("Player"))
        {
            Debug.Log("meeting");
            if (deadCount == 0)
            {
                player.FirstDead();
            }
            else
            {
                Debug.Log("DDDmeeting");
                player.Dead();
            }
            deadCount++;
        }
    }
    public void LightItem()
    {
        islightitem = true;
        Debug.Log("����" + islightitem);
        StartCoroutine(ItemLi(0.7f));

    }
    IEnumerator ItemLi(float _duration)
    {

        float timer = 0f;
        while (timer <= _duration)
        {
            Debug.Log("���� ����");
            timer += 0.2f;
            light2d.pointLightOuterRadius += 2f;
            //float t = Mathf.PingPong(Time.time, 0.1f) / 0.1f;
            //light2d.intensity = Mathf.PingPong(Time.time, 3f) / t;
            light2d.intensity += 2f;
            yield return new WaitForSeconds(0.1f);
        }
        islightitem = false;
        Debug.Log(islightitem);
        light2d.pointLightOuterRadius = 5f;
        light2d.intensity = 1f;

    }
    float enemytime = 0;
    void Update()
    {

        //spriteRenderer.sortingOrder = (Mathf.RoundToInt(transform.position.y) * -1);
        if (islightitem == false && enemytime == 0 &&Ending.Instance.isending == false)
        {
            agent.SetDestination(target.position);
        }
        else
        {
            agent.ResetPath();
            enemytime += 0.01f;
            //Debug.Log("���ʹ� ����");
            //Debug.Log(enemytime);
            if (enemytime > 5f)
            {
                enemytime = 0;
            }
        }
        if(PlayerCtrl.Instance.ismeeting == true)
        {
            agent.ResetPath();
            enemytime += 0.01f;
            //Debug.Log("���ʹ� ����");
            //Debug.Log(enemytime);
            if (enemytime > 5f)
            {
                enemytime = 0;
                Debug.Log("��ȯ");
            }
        }

        if (Vector2.Distance(enemyTransform.position, target.position) <= 300f && Vector2.Distance(enemyTransform.position, target.position) > 10f)
        {
            SoundManager.instance.BgSoundPlay();
            agent.speed = 2f;
        }
        else if (Vector2.Distance(enemyTransform.position, target.position) <= 10f)
        {
            SoundManager.instance.AIBgSoundPlay();
            agent.speed = 5f;
        }
        else if (Vector2.Distance(enemyTransform.position, target.position) > 10f)
        {
            SoundManager.instance.BgSoundPlay();
            agent.ResetPath();
        }

        if (transform.position.x > target.position.x)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;
        //if (Input.GetKey(KeyCode.F))
        //{
        //    light2d.pointLightOuterRadius = 10f;
        //    light2d.intensity = 5f;

        //}
    }
}
