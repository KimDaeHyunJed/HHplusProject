using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MapReposition : MonoBehaviour
{
    [SerializeField] private float rePositionX = 12;
    [SerializeField] private float limitPositionX = -12.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.x <= limitPositionX)
        {
            var pos = transform.localPosition;
            pos.x += (Mathf.Abs(rePositionX) + Mathf.Abs(limitPositionX));
            transform.localPosition = pos;
        }
    }
}
