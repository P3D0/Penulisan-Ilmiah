using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ChunkConfigur", menuName = "Configs/Chunk Control", order = 4)]
public class ChunkConfigur : ScriptableObject
{
    public float chunkSize;
    public float baseSpeed;
    public int dropChunkDelayCount;
    public int collectibleChunkDelayCount;
    public int collectibleChunkOdds;
    public GameObject[] startingChunks;
    public GameObject[] normalChunks;
}
