using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Coba : MonoBehaviour
{
    public TextMeshProUGUI test;
    public Queue<GameObject> receptaclePool;
    private GameObject templateReceptacle;
    public float longTime;

    // Start is called before the first frame update
    void Start()
    {
        templateReceptacle = new GameObject("LOLI");
        templateReceptacle.transform.parent = transform;
        templateReceptacle.AddComponent<AudioSource>();
        receptaclePool = new Queue<GameObject>();
    }

    private void Awake()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(TestYield());
        if (test == null)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
           
            
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            test.gameObject.SetActive(true);
            test.fontStyle = FontStyles.Underline;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            InstantiateCoba();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (receptaclePool.Count != 0)
            {
                receptaclePool.Dequeue();
                Debug.Log("yang di dequeue" + receptaclePool);
            }
            else
            {
                Debug.Log("Queue is empty");
            }
        }
    }

    IEnumerator TestYield()
    {
        
        yield return new WaitForSeconds(longTime); test.gameObject.SetActive(false);

    }

    private void InstantiateCoba()
    {
        
        for (int i = 0; i < 10; i++)
        {
            
            GameObject item = Instantiate(templateReceptacle, transform);
            receptaclePool.Enqueue(item);
        }
        for (int i = 0; i < receptaclePool.Count; i++)
        {
            Debug.Log(receptaclePool);
        }
    }
}
