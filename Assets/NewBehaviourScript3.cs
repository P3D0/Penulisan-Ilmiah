using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript3 : MonoBehaviour
{
    public GameObject coba;
    public float currentSpeed = 5;
    public float loliSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //GameObject baru = Instantiate(coba, transform);
        //coba.transform.position += new Vector3(0f, 0f, -currentSpeed * Time.deltaTime);
        //coba.transform.position += new Vector3(0, 0, -currentSpeed * Time.deltaTime * loliSpeed);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(coba, transform);
        }
    }
}
