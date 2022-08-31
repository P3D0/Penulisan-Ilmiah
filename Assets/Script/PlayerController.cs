using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public ControllerInputConfig config;
    public Joystick joystick;
    public float smoothTime;
    float stunSmooth;
    [HideInInspector]
    public Vector3 currentVel;
    Vector3 velRef;
    [HideInInspector]
    public Rigidbody rb;
    public Vector3 currentSpeed;
    public Vector3 currentAcceleration;
    public float accelerationSpeed;
    float stunSpeed;
    Vector3 targetVel;
    public bool moving;
    bool leftHeld;
    bool rightHeld;
    bool upHeld;
    bool downHeld;
    public float stunCount;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.gameOngoing)
        {
            currentSpeed = Vector3.zero;
            currentAcceleration = Vector3.zero;
            currentVel = Vector3.zero;
            targetVel = Vector3.zero;
            rb.velocity = Vector3.zero;
        }

        if (GameManager.Instance.speedMultiplier <= 0f)
        {
            return;
        }

        if (stunCount > 0)
        {
            stunCount -= Time.deltaTime;
            if (stunCount <= 0)
            {
                Unstun();
            }
        }

        InputUpdate();
        InputMovement();

        targetVel = currentSpeed;
        currentVel = Vector3.SmoothDamp(currentVel, targetVel, ref velRef, smoothTime + stunSmooth);
        rb.velocity = currentVel;

        if (transform.position.z < -1.901f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -1.901f);
        }
    }

    public void RefreshMovementValues()
    {
        accelerationSpeed = config.inputAcceleration;
    }

    void InputMovement()
    {
        currentAcceleration = Vector3.zero;
        if (leftHeld)
        {
            currentAcceleration.x = currentAcceleration.x - 1f;
        }
        if (rightHeld)
        {
            currentAcceleration.x = currentAcceleration.x + 1f;
        }
        if (upHeld)
        {
            currentAcceleration.z = currentAcceleration.z + 1f;
        }
        if (downHeld)
        {
            currentAcceleration.z = currentAcceleration.z - 1f;
        }


        if (joystick.Horizontal >= 0.2f)
        {
            currentAcceleration.x = currentAcceleration.x + 1f;
        }
        else if (joystick.Horizontal <= -0.2f)
        {
            currentAcceleration.x = currentAcceleration.x - 1f;
        }

        if (joystick.Vertical >= 0.2f)
        {
            currentAcceleration.z = currentAcceleration.z + 1f;
        }
        else if (joystick.Vertical <= -0.2f)
        {
            currentAcceleration.z = currentAcceleration.z - 1f;
        }



        currentSpeed = currentAcceleration;
        currentSpeed.Normalize();
        currentSpeed *= accelerationSpeed + stunSpeed;
        if (currentSpeed.x < 0f)
        {
            currentSpeed.x = currentSpeed.x - GameManager.Instance.speedMultiplier;
        }
        else if (currentSpeed.x > 0f)
        {
            currentSpeed.x = currentSpeed.x + GameManager.Instance.speedMultiplier;
        }
        if (currentSpeed.z < 0f)
        {
            currentSpeed.z = currentSpeed.z - GameManager.Instance.speedMultiplier;
        }
    }

    void InputUpdate()
    {
        //input buat character bergerak ke kiri kanan atas bawah
        moving = false;
        leftHeld = false;
        for (int i = 0; i < config.leftInputs.Length; i++)
        {
            if (Input.GetKey(config.leftInputs[i]))
            {
                moving = true;
                leftHeld = true;
                break;
            }
        }
        rightHeld = false;
        for (int j = 0; j < config.rightInputs.Length; j++)
        {
            if (Input.GetKey(config.rightInputs[j]))
            {
                moving = true;
                rightHeld = true;
                break;
            }
        }
        upHeld = false;
        for (int k = 0; k < config.upInputs.Length; k++)
        {
            if (Input.GetKey(config.upInputs[k]))
            {
                moving = true;
                upHeld = true;
                break;
            }
        }
        downHeld = false;
        for (int l = 0; l < config.downInputs.Length; l++)
        {
            if (Input.GetKey(config.downInputs[l]))
            {
                moving = true;
                downHeld = true;
                return;
            }
        }
    }

    public void Stun()
    {
        if (stunCount > 0f)
        {
            return;
        }
        //SoundManager.Instance.PlaySound(SoundManager.Instance.stun, false, false); ini buat suara
        stunSpeed = config.stunSpeed;
        stunSmooth = config.stunSmooth;
        stunCount = config.stunDuration;
        PlayerAnimationRotation.Instance.stunEffect.SetActive(true);// ini buat FX
        PlayerAnimationRotation.Instance.Stun(); //ini Buat Animasi
    }

    public void Unstun()
    {
        stunSpeed = 0f;
        stunSmooth = 0f;
        PlayerAnimationRotation.Instance.stunEffect.SetActive(false); 
        PlayerAnimationRotation.Instance.Unstun();
    }
}
