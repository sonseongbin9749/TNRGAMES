using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTeleport : MonoBehaviour
{
    [SerializeField]
    private GameObject toObject;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.position = toObject.transform.position;
    }
}
