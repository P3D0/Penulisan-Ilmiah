using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject obj;
    public Texture m_MainTexture;
    private SkinnedMeshRenderer body;
    // Start is called before the first frame update
    void Start()
    {
        body = obj.GetComponentInChildren<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // coba.Dequququq();
        if (Input.GetKeyDown(KeyCode.A))
        {
            body.material.SetTexture("_MainTex", m_MainTexture);
        }
    }
}
