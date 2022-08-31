using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Decorum Config", menuName = "Configs/Decorum", order = 2)]
public class DecorumConfig : ScriptableObject
{
    public GameObject[] decorumObjects;
    public float decorumDistanceFromCenter;
    public float decorumDistanceFromEachOther;
    public int poolAmount;
}
