using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TimeSettings", menuName = "TimeSettings")]
public class TimeSettings : ScriptableObject
{
    public float timeMultiplier = 2500;
    public float startHour = 12;
    public float sunriseHour = 7;
    public float sunsetHour = 18;
}
