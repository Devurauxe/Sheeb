using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    Quaternion initial_Rot;

    // Start is called before the first frame update
    void Start()
    {
        initial_Rot = transform.rotation;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
