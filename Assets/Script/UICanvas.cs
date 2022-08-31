using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : MonoBehaviour
{
    public static UICanvas Instance;
    public GameObject startCanvas;
    public GameObject gameCanvas;
    public GameObject endCanvas;
    public long liveSaved;
    [HideInInspector]
    public long currentGain;
    //public float resfreshingMultiplier;
    //public int maxGain;
    public long maxLivesSaved;

    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
    }

    public virtual void DisplayGame(bool display)
    {
        gameCanvas.SetActive(display);
    }

    public virtual void DisplayStart(bool display)
    {
        startCanvas.SetActive(display);
    }

    public virtual void DisplayEnd(bool display)
    {
        endCanvas.SetActive(display);
    }

    public void GetLiveSaved(int score)
    {
        liveSaved += currentGain * score;
        if (liveSaved >= maxLivesSaved)
        {
            liveSaved = maxLivesSaved;
            //klo menang
            GameManager.Instance.WinGame();
        }
        //Dispay Score
        //ScoreCounter.Instance.UpdateTargetScore();\
        ProgresBar.Instance.SetMaxProgress(liveSaved);
        //RestLiveSaved();
    }

    public void Loselive(int amount)
    {
        liveSaved -= amount * currentGain;
        if (liveSaved < 0L)
        {
            //klo kalah 
            GameManager.Instance.LoseGame();
            liveSaved = 0L;
            
        }
        //Display Score
        ProgresBar.Instance.SetMaxProgress(liveSaved);
        //ScoreCounter.Instance.UpdateTargetScore();
    }

    //private void RestLiveSaved()
    //{
    //    long num = currentGain;
    //    currentGain = Mathf.RoundToInt(currentGain * resfreshingMultiplier);
    //    if (currentGain <= num)
    //    {
    //        currentGain += 1L;
    //    }
    //    if (currentGain >= maxGain)
    //    {
    //        currentGain = maxGain;
    //    }
    //}

    //public void ResetCurrentGain()
    //{
    //    currentGain = 1L;
    //}
}
