using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseByEsc : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            HandleClick();
    }

    void HandleClick()
    {
        gameObject.SetActive(false);
    }
}
