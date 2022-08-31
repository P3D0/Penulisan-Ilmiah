using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public int poolIndex;
    DecorumSideBuildingsGenerator decorum;
    public Obstacle[] obstacles;

    public ChunkType type;
    // Start is called before the first frame update
    private void Awake()
    {
        if (obstacles == null)
        {
            return;
        }
        obstacles = GetComponentsInChildren<Obstacle>();
        decorum = GetComponentInChildren<DecorumSideBuildingsGenerator>();
    }

    public void Initialize()
    {
        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i].gameObject.SetActive(true);
            obstacles[i].Initialize();
        }
        if (decorum == null)
        {
            Debug.Log("Decorum null");
        }
        decorum.Initialize();
    }

    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Deactivate()
    {
        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i].Deactive();
        }
        //for (int j = 0; j < this.collectibles.Length; j++)
        //{
        //    this.collectibles[j].Deactivate();
        //}
        GetComponentInChildren<DecorumSideBuildingsGenerator>().Deactivate();
        Debug.Log("Deactive");
    }
}
