using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDespawner : MonoBehaviour
{
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
            GameObject.Destroy(this);
        }
    }
}
