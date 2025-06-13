using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeService 
{
    readonly TimeSettings settings;
    DateTime currentTime;
    readonly TimeSpan sunriseTime;
    readonly TimeSpan sunsetime;

    public event Action OnSunrise = delegate { };
    public event Action OnSunSet = delegate { };
    public event Action OnHourChange = delegate { };

    Observer<bool> isDayTime;
    Observer<int> currentHour;

    public TimeService(TimeSettings settings)
    {
        this.settings = settings;
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(settings.startHour);
        sunriseTime = TimeSpan.FromHours(settings.sunriseHour);
        sunsetime = TimeSpan.FromHours(settings.sunsetHour);

        isDayTime = new Observer<bool>(IsDayTime());
        currentHour = new Observer<int>(currentTime.Hour);

        isDayTime.onValueChanged.AddListener(day => (day ? OnSunrise : OnSunSet)?.Invoke());
        currentHour.onValueChanged.AddListener(_ => OnHourChange?.Invoke());
    }

    public void UpdateTime(float deltaTime)
    {
        currentTime = currentTime.AddSeconds(deltaTime * settings.timeMultiplier);
        isDayTime.Value = IsDayTime();
        currentHour.Value = currentTime.Hour;
    }

    public float CalculateSunAngle()
    {
        bool isDay = IsDayTime();
        float startDegree = isDay ? 0 : 100;
        TimeSpan start = isDay ? sunriseTime : sunsetime;
        TimeSpan end = isDay ? sunsetime : sunriseTime;

        TimeSpan totalTime = CalculateDifference(start, end);
        TimeSpan elapsedTime = CalculateDifference(start, currentTime.TimeOfDay);

        double percentage = elapsedTime.TotalMinutes / totalTime.TotalMinutes;
        return Mathf.Lerp(startDegree, startDegree + 180, (float)percentage);
    }

    public DateTime CurrentTime => currentTime;

    //if the current time is past the sunrise time but before sunset time, return true
    bool IsDayTime() => currentTime.TimeOfDay > sunriseTime && currentTime.TimeOfDay < sunsetime;

    //if value is negative (from val is later than to val), add 24 hours to account for the time difference being the next day 
    TimeSpan CalculateDifference(TimeSpan from, TimeSpan to)
    {
        TimeSpan difference = to - from;
        return difference.TotalHours < 0 ? difference + TimeSpan.FromHours(24) : difference;
    }
}
