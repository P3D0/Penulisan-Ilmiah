using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorumSideBuildingsGenerator : MonoBehaviour
{
    Transform leftParent;
    Transform rightParent;
    List<GameObject> currentObject;

    private void Awake()
    {
        leftParent = new GameObject("Kirigan").transform;
        leftParent.parent = transform;
        leftParent.localPosition = new Vector3(-DecorumSideBuildings.Instance.config.decorumDistanceFromCenter, 0f, 0f);
        rightParent = new GameObject("Kanangan").transform;
        rightParent.parent = transform;
        rightParent.localPosition = new Vector3(DecorumSideBuildings.Instance.config.decorumDistanceFromCenter, 0f, 0f);
        rightParent.transform.localScale = new Vector3(-1f, 1f, 1f);
    }

    public void Initialize()
    {
        currentObject = new List<GameObject>();
        for (float i = 0; i < ChunkManager.Instance.config.chunkSize; i += DecorumSideBuildings.Instance.config.decorumDistanceFromEachOther)
        {
            //buat spawn bagian kiri
            GameObject spawn = DecorumSideBuildings.Instance.RandomDecorum();
            currentObject.Add(spawn);
            spawn.transform.parent = leftParent;
            spawn.transform.localScale = Vector3.one;
            spawn.transform.localPosition = Vector3.zero + new Vector3(0f, 0f, i);
            spawn.gameObject.SetActive(true);
            //buat spawn bagian kanan
            spawn = DecorumSideBuildings.Instance.RandomDecorum();
            currentObject.Add(spawn);
            spawn.transform.parent = rightParent;
            spawn.transform.localScale = Vector3.one;
            spawn.transform.localPosition = Vector3.zero + new Vector3(0f, 0f, i);
            spawn.gameObject.SetActive(true);
        }
    }

    public void Deactivate()
    {
        for (int i = 0; i < currentObject.Count; i++)
        {
            DecorumSideBuildings.Instance.ReturnDecorum(currentObject[i]);
        }
    }
}
