using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct MapInfo
{
    public Collider2D col;
    public string name;
}

public class MapName : MonoBehaviour
{
    public static MapName instance;
    [SerializeField]
    Text mapNameText = null;
    [SerializeField]
    List<MapInfo> mapList = new List<MapInfo>();
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void MapNameChange(Collider2D col)
    {
        for(int i = 0; i< mapList.Count; i++)
        {
           if(mapList[i].col == col)
            {
                mapNameText.text = mapList[i].name;
                break;
            }
        }
    }
}
