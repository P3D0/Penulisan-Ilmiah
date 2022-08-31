using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ProgresBar : MonoBehaviour
{
    public static ProgresBar Instance;
    public Slider slider;

    private void Awake()
    {
        Instance = this;
       
    }
    private void Start()
    {
        slider.maxValue = UICanvas.Instance.maxLivesSaved;
        slider.value = 0;
    }
    public void SetMaxProgress(long proc)
    {
        slider.value = proc;
    }
}
