using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorumSideBuildings : MonoBehaviour
{
    public static DecorumSideBuildings Instance;
    List<GameObject> decorumList;
    public DecorumConfig config;

    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }

    public void Initialize()
    {
        decorumList = new List<GameObject>();
        FillDecorumPool();
    }

    public void FillDecorumPool()
    {
        for (int i = 0; i < config.poolAmount; i++)
        {
            GameObject buildings = Instantiate(config.decorumObjects[Random.Range(0, config.decorumObjects.Length)], transform);
            decorumList.Add(buildings);
            buildings.gameObject.SetActive(false);
        }
    }

    public GameObject RandomDecorum()
    {
        if (decorumList.Count < 1)
        {
            FillDecorumPool();
        }
        int index = Random.Range(0, decorumList.Count);
        GameObject result = decorumList[index];
        decorumList.RemoveAt(index);
        return result;
    }

    public void ReturnDecorum(GameObject decorum)
    {
        decorum.SetActive(false);
        decorumList.Add(decorum);
    }
}
