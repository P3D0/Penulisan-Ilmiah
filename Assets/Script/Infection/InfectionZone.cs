using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectionZone : MonoBehaviour
{
    public SpriteRenderer groundSR;
    public CapsuleCollider col;
    public float size;
    public bool infected;
    public GameObject normalFX;
    public GameObject infectedFX;
    public bool playerInside;


    public void Initialize(float _size)
    {
        infected = false;
        size = _size;
        transform.localScale = Vector3.one * size;
    }

   

    void Update()
    {
        ResetZoneColor();
    }

    public void ResetZoneColor()
    {
        if (InfectionManager.Instance.zonesDisabled)
        {
            normalFX.SetActive(false);
            infectedFX.SetActive(false);
            if (InfectionManager.Instance.zonesInvisible)
            {
                groundSR.color = new Color(0f, 0f, 0f, 0f);
                return;
            }
            groundSR.color = InfectionManager.Instance.disabledZoneColor;
            return;
        }
        else
        {
            if (infected)
            {
                normalFX.SetActive(false);
                infectedFX.SetActive(true);
                groundSR.color = InfectionManager.Instance.infectedZoneColor;
                return;
            }
            normalFX.SetActive(true);
            infectedFX.SetActive(false);
            groundSR.color = InfectionManager.Instance.normalZoneColor;
            return;
        }
    }

    public void Infect()
    {
        infected = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (InfectionManager.Instance.zonesDisabled)
        {
            return;
        }
        if (other.tag == "Player")
        {
            playerInside = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (InfectionManager.Instance.zonesDisabled)
        {
            return;
        }
        if (other.tag == "Player")
        {
            playerInside = false;
        }
    }
}
