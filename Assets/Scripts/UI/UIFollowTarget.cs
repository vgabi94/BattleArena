using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFollowTarget : MonoBehaviour
{

    public Vector2 offset = new Vector2(0f, 2f);

    GameObject target;
    RectTransform rt;
    Vector3 offset3d;

    Vector3 lastPos;

    // Use this for initialization
    void Start()
    {
        target = transform.parent.parent.gameObject;
        Debug.Assert(target != null, "This component must have at least two parents");
        rt = GetComponent<RectTransform>();
        offset3d = new Vector3(offset.x, offset.y, 0f);
    }
    
    // Update is called once per frame
    void Update()
    {
        var pos = Camera.main.WorldToScreenPoint(target.transform.position + offset3d);
        rt.position = pos;
    }
}
