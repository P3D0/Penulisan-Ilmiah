using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChunkManager : MonoBehaviour
{
    public float obstacleActivationDistance;
    public static ChunkManager Instance;
    public ChunkConfigur config;
    ChunkPool[] startingChunkPools;
    ChunkPool[] chunkPools;
    public Transform inactiveChunksParent;
    public float currentSpeed;
    public Transform[] chunkParents;
    Chunk[] currentChunks;
    public int lastChunkGenerated;
    int progression;

    private void Awake()
    {
        Instance = this;
    }
    //buat menang ato kalah 
    public void DeactivateAllObstacles()
    {
        for (int i = 0; i < currentChunks.Length; i++)
        {
            for (int j = 0; j < currentChunks[i].obstacles.Length; j++)
            {
                currentChunks[i].obstacles[j].gameObject.SetActive(false);
            }
        }
    }

    //buat menang ato kalah
    public void DeactivateAllNearbyObstacles()
    {
        for (int i = 0; i < currentChunks.Length; i++)
        {
            for (int j = 0; j < currentChunks[i].obstacles.Length; j++)
            {
                currentChunks[i].obstacles[j].SetDeactiveObs();
            }
        }
    }

    public void Initialize()
    {
        startingChunkPools = new ChunkPool[config.startingChunks.Length];
        chunkPools = new ChunkPool[config.normalChunks.Length];
        for (int i = 0; i < config.startingChunks.Length; i++)
        {
            startingChunkPools[i] = new ChunkPool(config.startingChunks[i]);
        }

        for (int j = 0; j < config.normalChunks.Length; j++)
        {
            chunkPools[j] = new ChunkPool(config.normalChunks[j]);
        }

        chunkParents = new Transform[3];
        currentChunks = new Chunk[3];
        for (int n = 0; n < 3; n++)
        {
            GameObject objBaru = new GameObject("ChunkParent_" + n.ToString());
            objBaru.transform.parent = transform;
            chunkParents[n] = objBaru.transform;
            chunkParents[n].position = new Vector3(0f, 0f, -config.chunkSize + config.chunkSize * n);
            GenerateNewChunk(n);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateChunkMovement();
    }

    void UpdateChunkMovement()
    {
        for (int i = 0; i < chunkParents.Length; i++)
        {
            chunkParents[i].position += new Vector3(0f, 0f, -currentSpeed * Time.deltaTime * GameManager.Instance.speedMultiplier); //buat gerak
            if (chunkParents[i].position.z <= -config.chunkSize * 2f)
            {
                
                float chunkSize = config.chunkSize;
                Vector3 position = chunkParents[i].position;
                if (i == 0)
                {
                    chunkParents[i].position = chunkParents[2].position + new Vector3(0f, 0f, config.chunkSize);
                }
                else
                {
                    chunkParents[i].position = chunkParents[i - 1].position + new Vector3(0f, 0f, config.chunkSize);
                }
                GenerateNewChunk(i);
                lastChunkGenerated = i;
                chunkParents[i].localScale = new Vector3(((Random.Range(0, 2) == 0) ? -1 : 1), 1f, 1f);
            }
        }
    }

    public void GenerateNewChunk(int parentIndex)
    {
        if (currentChunks[parentIndex] != null)
        {
            currentChunks[parentIndex].Deactivate();
            currentChunks[parentIndex].gameObject.SetActive(false);
            currentChunks[parentIndex].transform.parent = inactiveChunksParent;
            switch (currentChunks[parentIndex].type)
            {
                case ChunkType.starting:
                    startingChunkPools[currentChunks[parentIndex].poolIndex].Requeue(currentChunks[parentIndex]);
                    break;
                case ChunkType.normal:
                    chunkPools[currentChunks[parentIndex].poolIndex].Requeue(currentChunks[parentIndex]);
                    break;
            }
        }

        currentChunks[parentIndex] = GetChunk();
        currentChunks[parentIndex].Initialize();
        currentChunks[parentIndex].gameObject.SetActive(true);
        currentChunks[parentIndex].transform.parent = chunkParents[parentIndex];
        currentChunks[parentIndex].transform.localPosition = Vector3.zero;
    }

    public Chunk GetChunk()
    {
        //Disini dimana semua chunknya berubah
        Chunk chunk;
        if (progression < startingChunkPools.Length)
        {
            chunk = startingChunkPools[progression].Dequeue();
            chunk.type = ChunkType.starting;
            chunk.poolIndex = progression;
        }
        else
        {
            int num4 = Random.Range(0, chunkPools.Length);
            chunk = chunkPools[num4].Dequeue();
            chunk.poolIndex = num4;
            chunk.type = ChunkType.normal;
        }
       
        progression++;
        return chunk;
    }
}
