using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    public string[] scenes;

    private void Awake()
    {
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

    public void SetScene(int idx)
    {
        StartCoroutine(LoadScene(scenes[idx]));
    }

    IEnumerator LoadScene(string sceneName)
    {
        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene(sceneName);
    }
}
