using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TimeSettings", menuName = "TimeSettings")]
public class TimeSettings : ScriptableObject
{
    public float timeMultiplier;
    public float startHour;
    public float sunriseHour;
    public float sunsetHour;
}
