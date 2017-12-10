using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    private static GameObject instance;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
            instance = gameObject;
    }

    public static void Show()
    {
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
