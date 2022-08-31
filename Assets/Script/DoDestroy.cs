using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDestroy : MonoBehaviour
{
    public float duration;

    void Update()
    {
        Destroy(gameObject, duration);
    }
}
