using UnityEngine;
using UnityEditor;
using System;

public class TimerDemoExamples : MonoBehaviour
{
    private void Start()
    {
        // 1. Basic timer (simple delay)
        TimerManager.Instance.StartRuntimeTimer(3f, () =>
        {
            Debug.Log("Basic timer finished after 3 seconds");
        });

        // 2. Countdown with ID (cooldown example)
        TimerManager.Instance.SetCountdownWithID(
            TimerID.Combat.BaseAttack,
            5f,
            () => Debug.Log("Attack cooldown finished")
        );

        // 3. Runtime timer with interval (tick)
        TimerManager.Instance.StartRuntimeTimer(
            5f,
            () => Debug.Log("Search finished"),
            1f,
            () => Debug.Log("Enemy is scanning...")
        );

        // 4. Stopwatch (tracking time)
        TimerManager.Instance.StartStopwatch(TimerID.Movement.FallTime);
    }

    private void Update()
    {
        // Example: reading stopwatch time
        if (TimerManager.Instance.IsStopwatchRunning(TimerID.Movement.FallTime))
        {
            float fallTime = TimerManager.Instance.GetStopwatchTime(TimerID.Movement.FallTime);
            Debug.Log($"Fall time: {fallTime:F2}s");
        }
    }
}
