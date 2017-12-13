using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    private RectTransform trans;

    private float time = 2f;
    private float speed;

    private void Start()
    {
        trans = GetComponent<RectTransform>();
        trans.anchoredPosition3D = new Vector3(0f, 200f);

        speed = 100f / time;
    }

    private void Update()
    {
        trans.anchoredPosition3D += speed * Time.deltaTime * Vector3.up;
    }
}
