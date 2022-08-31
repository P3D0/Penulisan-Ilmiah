using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CameraAnim : MonoBehaviour
{
    
    public static CameraAnim Instance;
    private Animator anim;
    private void Awake()
    {
        Instance = this;
        anim = GetComponent<Animator>();
    }

    public void GameIn()
    {
        anim.SetBool("cam_game", true);
    }

    public void GameLose()
    {
        anim.SetBool("cam_lose", true);
    }

    public void GameWin()
    {
        anim.SetBool("cam_win", true);
    }
}
