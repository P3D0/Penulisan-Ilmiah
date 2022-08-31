using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Obstacle : MonoBehaviour
{
    public bool randomizeRotation;
    public Vector2 minMaxRotation;
    public bool active;
	public UnityAction ObstacleUpdate;
    public bool scores;
    bool getSroce;
    ObstacleMovement movement;
    ObstacleInfection infection;
    Vector3 basePos;
    // Start is called before the first frame update\

    

    private void Awake()
    {
        movement = GetComponent<ObstacleMovement>();
        infection = GetComponent<ObstacleInfection>();
        ObstacleUpdate = (UnityAction)Delegate.Combine(ObstacleUpdate, new UnityAction(MainUpdate));
        basePos = transform.localPosition;
        //GetBaseInfo();
    }
    //public void GetBaseInfo()
    //{
    //    basePos = transform.localPosition;
    //}

    public void Initialize()
    {
        if (scores)
        {
            getSroce = false;
        }
        transform.localPosition = basePos;
        active = false;
        if (randomizeRotation)
        {
            transform.eulerAngles = new Vector3(0f, Random.Range(minMaxRotation.x, minMaxRotation.y), 0f);
        }
       
        if (movement)
        {
            movement.Initialize(this);
        }

        if (infection)
        {
            infection.Initialize();
        }
    }

    public void Deactive()
    {
        if (movement)
        {
            movement.Deactive();
        }
        if (infection)
        {
            infection.Deactivate();
        }
    }
    

    public void SetDeactiveObs()
    {
        if (Mathf.Abs(transform.position.z - PlayerController.Instance.transform.position.z) <= 10f)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Collide(collision.GetContact(0));
            //buat klo semisalnya character
            if (infection)
            {
                 infection.GetHit();
            }
        }
    }

    public void Collide(ContactPoint point)
    {
        if (PlayerController.Instance.stunCount > 0)
        {
            return;
        }
        PlayerController.Instance.Stun();
        //print("colliding !");
        //Vector3 vector = point.normal * -9f;
        //float num = 2.3f - Mathf.Abs(point.normal.x);
        //Mathf.Clamp(num, 1f, 2.3f);
        //vector.x *= num;
        //if (vector.x == 0f)
        //{
        //    vector.x = Random.Range(1f, -1f);
        //}
        //Debug.Log("ini vectornya" + vector);
        //PlayerController.Instance.currentVel = vector;
        PlayerAnimationRotation.Instance.SpawnHit(point.point, transform);
        if (infection)
        {
            InfectionManager.Instance.GainInfection(1);
        }
    }

    void Update()
    {
        ObstacleUpdate();
        //if (transform.position.z <= PlayerController.Instance.transform.position.z + 3)
        //{
        //    //nilai += 1;
        //    //text.SetText(nilai.ToString());
        //}
    }

    public void MainUpdate()
    {
        if (!active)
        {
            if (transform.position.z <= ChunkManager.Instance.obstacleActivationDistance)
            {
                active = true;
            }
            return;
        }
        
        if (scores == false)
        {
            return;
        }
       
        if (infection && infection.zone.infected)
        {
            return;
        }
       
        if (getSroce)
        {
            return;
        }
        //#buat nambah scorenya di sini gan 
        if (transform.position.z <= PlayerController.Instance.transform.position.z + 2f)
        {
            
            //    FXPlayer.Instance.PlayParentedFX("LifeSaved", base.transform.position + new Vector3(0f, 2f, 0f), base.transform);
            //    SoundManager.Instance.PlaySound(SoundManager.Instance.lifeSaved, false, false);
            getSroce = true;
            UICanvas.Instance.GetLiveSaved(1);
            Debug.Log("Dapet Score");
            //    ScoreManager.Instance.GainLivesSaved(1);
        }
    }
}


