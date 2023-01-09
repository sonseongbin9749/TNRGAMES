using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadImageMove : MonoBehaviour
{
    [SerializeField]
    private GameObject cameraObj = null;
    private Vector3 originPos;
    public IEnumerator Shake(float _amount, float _duration)
    {
        originPos = cameraObj.transform.position;
        float timer = 0f;
        while (timer <= _duration)
        {
            cameraObj.transform.position = (Vector3)Random.insideUnitCircle * _amount + originPos;

            timer += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        cameraObj.transform.position = originPos;
    }
}
