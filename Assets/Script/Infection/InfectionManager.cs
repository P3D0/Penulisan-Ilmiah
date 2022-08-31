using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectionManager : MonoBehaviour
{

    public static InfectionManager Instance;
    public GameObject zonePrefab;
    public List<ObstacleInfection> currentObstacleInfections;
    private Queue<InfectionZone> zonePool;
    //public bool playerInZone;
    //public Sprite saneWedge;
    //public Sprite infectedWedge;
    public Color normalZoneColor;
    public Color infectedZoneColor;
    public Color disabledZoneColor;
    //public Material normalProjectorMaterial;
    //public Material infectedProjectorMaterial;

    [Header("Infection")]
    public int infection;
    public int maxInfection;
    //public int sneezeInfectionAmount;
    //dibagain bawah mungkin digunain buat nanti textnya
   // public Color infectionMessageColor;
    //public float infectionMessageHeight;
    //public Color saneColor;
    //public Color infectedColor;
    public bool zonesDisabled;
    public bool zonesInvisible;

    private void Awake()
    {
        Instance = this;
    }

    public void GainInfection(int amount)
    {
        if (amount >= 0)
        {
            UICanvas.Instance.Loselive(amount);
            //UICanvas.Instance.ResetCurrentGain();
            //ScoreManager.Instance.LooseLivesSaved(amount);
            //ScoreManager.Instance.ResetCurrentGain();


            PlayerAnimationRotation.Instance.GetSick();
            //SoundManager.Instance.PlaySound(SoundManager.Instance.looseLife, false, false);
        }

        //dari sini ke bawah dia nentuin menang ato kalah 
        infection += amount;
        //if (infection >= maxInfection && GameManager.Instance.gameOngoing)
        //{
        //    infection = maxInfection;
        //    //GameManager.Instance.LooseGame();
        //    //InfectionWheel.Instance.RefreshWedgeInfection();
        //    return;
        //}
        if (infection < 0)
        {
            infection = 0;
        }
        //GameManager.Instance.ResetSpeed();
        //InfectionWheel.Instance.RefreshWedgeInfection();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.U))
        {
            GainInfection(1);
        }
        //bool flag = false;
        for (int i = 0; i < currentObstacleInfections.Count; i++)
        {
            if (currentObstacleInfections[i].zone.playerInside && !currentObstacleInfections[i].zone.infected)
            {
                currentObstacleInfections[i].zone.Infect();
                GainInfection(1);
                //flag = true;
            }
        }
        //playerInZone = flag;
    }
    public void Initialize()
    {
        zonePool = new Queue<InfectionZone>();
        FillZonePool();
    }

    void FillZonePool()
    {
        
        for (int i = 0; i < 10; i++)
        {
            InfectionZone component = Instantiate(zonePrefab, transform).GetComponent<InfectionZone>();
            zonePool.Enqueue(component);
            component.gameObject.SetActive(false);
        }
    }

    public InfectionZone GenerateInfectionZone()
    {
        if (zonePool.Count < 1)
        {
            FillZonePool();
        }
        return zonePool.Dequeue();
    }

    public void RepoolInfectionZone(InfectionZone zone)
    {
        if (zone == null)
        {
            return;
        }
        zone.transform.parent = transform;
        zone.gameObject.SetActive(false);
        zonePool.Enqueue(zone);
    }
}
