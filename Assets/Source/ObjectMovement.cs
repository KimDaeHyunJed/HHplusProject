
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    void Update()
    {
        var pos = transform.localPosition;
        pos.x += Time.deltaTime * GameManager.Instance.Speed;
        transform.localPosition = pos;
    }
}
