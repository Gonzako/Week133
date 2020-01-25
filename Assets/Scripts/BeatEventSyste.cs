using System;
using System.Timers;
using UnityEngine;

public class BeatEventSyste : MonoBehaviour
{
    public double bpm = 600;
    Timer whatever;
    DateTime previousDate = DateTime.Now;
    // Start is called before the first frame update
    void Start()
    {
        whatever = new Timer(bpm / 60000);

        whatever.Elapsed += logWhatever;
        whatever.Start();
    }

    private void logWhatever(object sender, ElapsedEventArgs e)
    {
        Debug.Log(e.SignalTime.ToOADate() - previousDate.ToOADate());
        previousDate = e.SignalTime;
    }

    private void OnDisable()
    {
        whatever.Elapsed -= logWhatever;
        whatever.Dispose();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
