using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum LaguageType
{
    Korea = 0,
    English = 1
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public Fade fade;
    public GameObject[] sprites;

    public LaguageType laguageType;

    public float settingTime = 20f;

    private void Awake()
    {
        laguageType = LaguageType.Korea;
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void setImagePaths(string _path)
    {
        sprites = Resources.LoadAll<GameObject>("Prefab/" + _path);
    }
}
