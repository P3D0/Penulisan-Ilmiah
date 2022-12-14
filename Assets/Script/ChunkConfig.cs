using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chunks Config", menuName = "Configs/Chunks", order = 3)]
public class ChunksConfig : ScriptableObject
{
    public float chunkSize;
    public float baseSpeed;
    public int dropChunkDelayCount;
    public int collectibleChunkDelayCount;
    public int collectibleChunkOdds;
    public GameObject[] startingChunks;
    public GameObject[] normalChunks;
}
