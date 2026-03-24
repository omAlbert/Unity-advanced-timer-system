using System;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance { get; private set; }

    private List<RuntimeTimer> toAdd = new();
    private List<int> toRemove = new();
    private bool isUpdating;

    private class CountdownTimer
    {
        public float Remaining;
        public Action Callback;
    }

    private class StopwatchTimer
    {
        public float Elapsed;
        public bool Reversed;
    }

    private class RuntimeTimer
    {
        public int id;
        public float Time;
        public float? Duration;
        public float? TickInterval;
        public float NextTickTime;
        public Action<float> OnUpdate;
        public Action OnFinished;
        public Action OnTick;
    }

    private readonly Dictionary<string, CountdownTimer> countdownTimers = new();
    private readonly Dictionary<int, RuntimeTimer> runtimeTimers = new();
    private readonly Dictionary<string, StopwatchTimer> stopwatchTimers = new();

    private int nextRuntimeID = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        UpdateCountdowns();
        UpdateRuntimeTimers(Time.deltaTime);
        UpdateStopwatches();
    }

    #region Countdowns
    private void UpdateCountdowns()
    {
        if (countdownTimers.Count == 0)
            return;

        var finished = new List<string>();

        foreach (var pair in countdownTimers)
        {
            pair.Value.Remaining -= Time.deltaTime;

            if (pair.Value.Remaining <= 0)
            {
                finished.Add(pair.Key);
            }
        }

        foreach (var id in finished)
        {
            var timer = countdownTimers[id];
            countdownTimers.Remove(id);

            timer.Callback?.Invoke();
        }
    }

    public void SetCountdownWithID(string id, float duration, Action callback = null)
    {
        if (countdownTimers.ContainsKey(id))
        {
            countdownTimers[id].Remaining = duration;
            countdownTimers[id].Callback = callback;
            return;
        }

        countdownTimers[id] = new CountdownTimer
        {
            Remaining = duration,
            Callback = callback
        };
    }

    public bool IsCountdownActive(string id) => countdownTimers.ContainsKey(id);

    public float GetCountdownRemaining(string id) => countdownTimers.TryGetValue(id, out var t) ? t.Remaining : 0f;

    public void CancelCountdown(string id)
    {
        if (countdownTimers.ContainsKey(id))
            countdownTimers.Remove(id);
    }

    #endregion

    #region RuntimeTimers

    public int StartRuntimeTimer(float duration, Action onFinished, float tickInterval = default, Action onTick = null, Action<float> onUpdate = null)
    {
        int id = nextRuntimeID++;

        var timer = new RuntimeTimer
        {
            id = id,
            Time = 0f,
            Duration = duration,
            OnUpdate = onUpdate,
            OnFinished = onFinished,
            TickInterval = tickInterval,
            NextTickTime = tickInterval,
            OnTick = onTick
        };

        if (isUpdating)
            toAdd.Add(timer);
        else
            runtimeTimers[id] = timer;

        return id;
    }

    public int StartRuntimeStopwatch(System.Action<float> onUpdate)
    {
        int id = nextRuntimeID++;

        runtimeTimers[id] = new RuntimeTimer
        {
            Time = 0f,
            Duration = null,
            OnUpdate = onUpdate
        };

        return id;
    }

    private void UpdateRuntimeTimers(float delta)
    {
        isUpdating = true;

        foreach (var kvp in runtimeTimers)
        {
            var timer = kvp.Value;
            timer.Time += delta;

            timer.OnUpdate?.Invoke(timer.Time);

            if (timer.TickInterval.HasValue)
            {
                if (timer.Time >= timer.NextTickTime)
                {
                    timer.OnTick?.Invoke();
                    timer.NextTickTime += timer.TickInterval.Value;
                }
            }

            if (timer.Duration.HasValue && timer.Time >= timer.Duration.Value)
            {
                timer.OnFinished?.Invoke();
                toRemove.Add(kvp.Key);
            }
        }

        isUpdating = false;

        foreach (var t in toAdd)
            runtimeTimers[t.id] = t;

        foreach (var id in toRemove)
            runtimeTimers.Remove(id);

        toAdd.Clear();
        toRemove.Clear();
    }

    public void StopRuntimeTimer(int id)
    {
        if (isUpdating)
            toRemove.Add(id);
        else
            runtimeTimers.Remove(id);
    }

    #endregion

    #region Stopwaches

    private void UpdateStopwatches()
    {
        if (stopwatchTimers.Count == 0) return;

        foreach (var pair in stopwatchTimers)
        {
            if (!pair.Value.Reversed)
                pair.Value.Elapsed += Time.deltaTime;
            else
                pair.Value.Elapsed -= Time.deltaTime;
        }
    }

    public void StartStopwatch(string id, bool reversed = false)
    {
        if (stopwatchTimers.ContainsKey(id)) return;

        stopwatchTimers[id] = new StopwatchTimer { Elapsed = 0f, Reversed = reversed };
    }

    public void PauseStopwatch(string id)
    {
        if (stopwatchTimers.ContainsKey(id))
            stopwatchTimers[id].Elapsed += 0;
    }

    public void StopStopwatch(string id)
    {
        if (stopwatchTimers.ContainsKey(id))
            stopwatchTimers.Remove(id);
    }

    public float GetStopwatchTime(string id) => stopwatchTimers.TryGetValue(id, out var t) ? t.Elapsed : 0f;

    public bool IsStopwatchRunning(string id) => stopwatchTimers.ContainsKey(id);

    #endregion
}
