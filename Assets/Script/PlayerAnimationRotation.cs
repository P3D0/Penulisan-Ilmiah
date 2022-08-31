using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationRotation : MonoBehaviour
{
    public static PlayerAnimationRotation Instance;
    public GameObject stunEffect; //stunStars
    public GameObject hitEffect;
    public Texture sickTexture;
    public Texture normalTexture;
    Animator anim;
    public Vector3 targetRot;
    Vector3 lastRot;
    public float rotSpeed = 2f;
    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
        anim = GetComponentInChildren<Animator>();
        if (stunEffect == null)
        {
            Debug.Log("Gak ada stun effect");
            return;
        }
        stunEffect.SetActive(false);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.gameOngoing)
        {
            PlayerRotation();
            if (RotChanged())
            {
                //graphicsParent.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(targetRot), rotSpeed * Time.deltaTime);
                //graphicsParent.DORotate(this.targetRot, this.rotSpeed, RotateMode.Fast);
            }
        }
        ///RefreshAnimationSpeed();
    }

    void PlayerRotation()
    {
        Vector3 currentSpeed = PlayerController.Instance.currentSpeed;
        float y = Mathf.Atan2(currentSpeed.x, currentSpeed.z) * 57.29578f;
        targetRot = new Vector3(0f, y, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(targetRot), rotSpeed * Time.deltaTime); //ini buat rotasi smooth masih bisa dapat berubah karena belum dimasukan karakter nanti kita rubah pas masukin character
    }

    bool RotChanged()
    {
        //akhirnya gak tau gunanya apa
        if (targetRot != lastRot)
        {
            lastRot = targetRot;
            return true;
        }
        return false;
    }

    public void TurnToStreet()
    {
        anim.SetBool("walking", true);
    }

    public void Stun()
    {
        anim.SetBool("stun", true);
    }

    public void Unstun()
    {
        anim.SetBool("stun", false);
    }

    public void Win()
    {
        anim.SetBool("win", true);
    }

    public void Lose()
    {
        anim.SetBool("lose", true);
    }

    //FX HIt
    public void SpawnHit(Vector3 positon, Transform parent)
    {
        Vector3 vec = new Vector3(positon.x, positon.y + 0.5f, positon.z);
        GameObject hit = Instantiate(hitEffect,vec,transform.rotation ,parent);
        hit.transform.localScale = Vector3.one * 2;
        //hit.AddComponent<DoDestroy>();
    }

    public void GetSick()
    {
        StartCoroutine(Sick());
    }

    IEnumerator Sick()
    {
        Stun();
        SickFX.Instance.SetTexture(sickTexture);
        yield return new WaitForSeconds(.4f);
        if (GameManager.Instance.gameOngoing == true)
        {
            Unstun();
            SickFX.Instance.SetTexture(normalTexture);
        }
        SickFX.Instance.SetTexture(normalTexture);
        yield break;
    }
}
