using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
   // public GameObject startingContent;
    float baseSpeed;
    public float speedMultiplier;
    //float addedSpeed;
    public bool gameOngoing;
    //float difficultyCount;
    private bool restart;

    private void Awake()
    {
        Instance = this;
        baseSpeed = speedMultiplier;
        speedMultiplier = 0f;
        //addedSpeed = 5 - baseSpeedMult / 2; // 5 dan 2 nanti di rubah

    }
    // Start is called before the first frame update
    void Start()
    {
        UICanvas.Instance.DisplayGame(false);
        UICanvas.Instance.DisplayEnd(false);


        InfectionManager.Instance.Initialize();

        DecorumSideBuildings.Instance.Initialize();
        ChunkManager.Instance.Initialize();
        PlayerController.Instance.RefreshMovementValues();
        //Obstacle[] component = startingContent.GetComponentsInChildren<Obstacle>();
        //for (int i = 0; i < component.Length; i++)
        //{
        //    component[i].Initialize();
        //}
        //startingContent.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOngoing)
        {
            speedMultiplier = 0;
        }
        if (speedMultiplier == 0)
        {
            return;
        }
        //difficultyCount += Time.deltaTime;
        //if (difficultyCount >= 1)
        //{
        //    difficultyCount -= 1f;
        //    speedMultiplier += addedSpeed;
        //}
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        //if (!ChunkManager.Instance.enableStartingChunks)
        //{
        //    ChunkManager.Instance.GenerateNewChunk(2);
        //    this.startingContent.SetActive(true);
        //    this.startingContent.transform.parent = ChunkManager.Instance.chunkParents[1];
        //StartCoroutine(this.DisableStartingContent());
        //}
        gameOngoing = true;
        speedMultiplier = baseSpeed;
        PlayerAnimationRotation.Instance.TurnToStreet();
        UICanvas.Instance.DisplayStart(false);
        UICanvas.Instance.DisplayGame(true);
        CameraAnim.Instance.GameIn();
    }

    //IEnumerator DisableStartingContent()
    //{
    //    yield return new WaitForSeconds(30f);
    //    startingContent.SetActive(false);
    //    yield break;
    //}

    public void WinGame()
    {
        CameraAnim.Instance.GameWin();
        PlayerAnimationRotation.Instance.Win();//buat animasi win
        ChunkManager.Instance.DeactivateAllNearbyObstacles();
        UICanvas.Instance.DisplayGame(false);
        StartCoroutine(ShowEndCanvasDelay());
        gameOngoing = false;
    }

    public void LoseGame()
    {
        CameraAnim.Instance.GameLose();
        PlayerAnimationRotation.Instance.Lose(); //buat animasi lose
        ChunkManager.Instance.DeactivateAllNearbyObstacles();
        StartCoroutine(ShowEndCanvasDelay());
        UICanvas.Instance.DisplayGame(false);
        gameOngoing = false;      
    }

    public void ReStart()
    {
        if (restart)
        {
            return;
        }
        restart = true;
        StartCoroutine(RestartDelay());
    }

    IEnumerator RestartDelay()
    {
        yield return new WaitForSeconds(.2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        yield break;
    }

    IEnumerator ShowEndCanvasDelay()
    {
        yield return new WaitForSeconds(1f);
        UICanvas.Instance.DisplayEnd(true);
    }
}
