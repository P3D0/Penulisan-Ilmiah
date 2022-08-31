using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleInfection : MonoBehaviour
{
    public float infectionDiameter;
    public bool startsInfected;
    public bool infected;
    public InfectionZone zone;

    public void Refresh()
    {
        if (infected)
        {
            zone.gameObject.SetActive(true);
            return;
        }
        zone.gameObject.SetActive(false);
    }

    public void GetHit()
    {
        if (InfectionManager.Instance.infection >= 1 && !infected)
        {
            GetInfected();
        }
    }

    public void GetInfected()
    {
        UICanvas.Instance.Loselive(2);
        //UICanvas.Instance.ResetCurrentGain();
        //ScoreManager.Instance.LooseLivesSaved(2);
        //ScoreManager.Instance.ResetCurrentGain();
        //FXPlayer.Instance.PlayTextMessage(base.transform, InfectionManager.Instance.infectionMessageColor, "Infected !!", InfectionManager.Instance.infectionMessageHeight);
        infected = true;
    }

    public void Initialize()
    {
        zone = InfectionManager.Instance.GenerateInfectionZone();
        zone.transform.parent = transform;
        zone.transform.position = transform.position;
        InfectionManager.Instance.currentObstacleInfections.Add(this);
        zone.Initialize(infectionDiameter);
        if (startsInfected)
        {
            infected = true;
        }
        Refresh();
    }

    public void Deactivate()
    {
        InfectionManager.Instance.currentObstacleInfections.Remove(this);
        if (zone == null)
        {
            return;
        }
        InfectionManager.Instance.RepoolInfectionZone(zone);
        zone = null;
    }
}
