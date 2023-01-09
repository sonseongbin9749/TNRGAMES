using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject Target;             // ����ٴ� Ÿ�� ������Ʈ
    public float follow_speed = 4.0f;    // ���󰡴� �ӵ�
    public float z = -10.0f;            // ������ų ī�޶��� z���� ��

    Transform this_transform;            // ī�޶��� ��ǥ
    Transform Target_transform;         // Ÿ���� ��ǥ

    void Start()
    {
        this_transform = GetComponent<Transform>();
        Target_transform = Target.GetComponent<Transform>();
    }
    void Update()
    {
        this_transform.position = Vector2.Lerp(this_transform.position, Target_transform.position, follow_speed * Time.deltaTime);
        this_transform.Translate(0, 0, z); //ī�޶� ���� z������ �̵�
    }
}
