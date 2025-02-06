using System;
//using System.Diagnostics;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField] Transform hoursPivot, secondsPivot, minutesPivot;
    const float _hoursToDegress = -30f, _minutesToDegress = 6f, _secondsToDegrees = -6f;

    public void Update()
    {
        TimeSpan _time= DateTime.Now.TimeOfDay;
        //Debug.Log(DateTime.Now.Hour);
        hoursPivot.localRotation = Quaternion.Euler(0f, 0f, _hoursToDegress * (float)_time.TotalHours);
        //Debug.Log(DateTime.Now.Minute);
        minutesPivot.localRotation = Quaternion.Euler(0f, 0f, _minutesToDegress * (float)_time.TotalMinutes);
        //Debug.Log(DateTime.Now.Second);
        secondsPivot.localRotation = Quaternion.Euler(0f,0f,_secondsToDegrees * (float)_time.TotalSeconds);
    }


}
