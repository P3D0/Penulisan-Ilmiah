using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SickFX : MonoBehaviour
{
    public static SickFX Instance;
    public GameObject obj;
    private SkinnedMeshRenderer body;
    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
        body = obj.GetComponentInChildren<SkinnedMeshRenderer>();
    }

    public void SetTexture(Texture tex)
    {
        body.material.SetTexture("_MainTex", tex);
    }
}
