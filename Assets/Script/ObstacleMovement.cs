using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObstacleMovement : MonoBehaviour
{
    public float baseSpeed;
    public float currentSpeed;
    Obstacle obstacle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(Obstacle _obstacle)
    {
        obstacle = _obstacle;
        currentSpeed = baseSpeed * GameManager.Instance.speedMultiplier;
        Obstacle obs = obstacle;
        obs.ObstacleUpdate = (UnityAction)Delegate.Combine(obstacle.ObstacleUpdate, new UnityAction(Move));

    }

    public void Move()
    {
        if (!obstacle.active)
        {
            return;
        }
        currentSpeed = baseSpeed * GameManager.Instance.speedMultiplier;
        transform.position += transform.forward * currentSpeed * Time.deltaTime;
    }

    public void Deactive()
    {
        Obstacle _obstacle = obstacle;
        _obstacle.ObstacleUpdate = (UnityAction)Delegate.Remove(_obstacle.ObstacleUpdate, new UnityAction(Move));
    }
}
