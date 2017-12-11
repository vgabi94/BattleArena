using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    private static GameObject instance;
    private static LoadingScreen loadingScreenInstance;

    private UnityEngine.UI.Text text;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = gameObject;
            loadingScreenInstance = gameObject.GetComponent<LoadingScreen>();
        }

        text = transform.Find("Text").GetComponent<UnityEngine.UI.Text>();
    }

    public static void Show(string msg = "loading")
    {
        loadingScreenInstance.text.text = msg;
        for (int i = 0; i < instance.transform.childCount; i++)
        {
            instance.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public static void Hide()
    {
        for (int i = 0; i < instance.transform.childCount; i++)
        {
            instance.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
