using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ControllerInputConfig", menuName = "Configs/Player Control", order = 1)]
public class ControllerInputConfig : ScriptableObject
{
    public float inputAcceleration;
    public float stunSpeed;
    public float stunSmooth;
    public float stunDuration;
    public KeyCode[] leftInputs;
    public KeyCode[] rightInputs;
    public KeyCode[] upInputs;
    public KeyCode[] downInputs;
}
