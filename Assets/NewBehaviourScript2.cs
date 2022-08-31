using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class NewBehaviourScript2 : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject coba;
    public Queue<GameObject> antrian;
    // Start is called before the first frame update
    void Start()
    {
        antrian = new Queue<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (antrian.Count < 5)
            {
                GameObject ins = Instantiate(coba, transform);
                text.SetText(ins.name);
                Camera cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
                cam.transform.LookAt(ins.transform);
                antrian.Enqueue(ins);
            }
            
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            Dequququq();
        }
    }
    public void Dequququq()
    {
        Debug.Log("Dequque");
        antrian.Dequeue();
    }
}
