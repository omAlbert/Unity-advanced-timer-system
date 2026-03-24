# 🧩 Advanced Timer & Scheduling System (Unity)

A modular and centralized timer system designed to handle time-based logic in Unity without relying on scattered `Update` methods.

---

## 🧠 Problem

Managing timers directly inside multiple `Update()` methods leads to:

* Scattered and hard-to-maintain logic
* Tight coupling between systems
* Increased risk of bugs (missed conditions, race issues)
* Difficult debugging and scaling

---

## 💡 Solution

A centralized **Timer Manager** using a callback-based architecture that allows scheduling and controlling all time-based behaviors from a single system.

---

## 🚀 Impact

* Cleaner and more maintainable code
* Reduced reliance on `Update()` across scripts
* Centralized control of time-based logic
* Improved debugging and scalability

---

## ⏱️ Timer Types

### 1. Countdown Timers (ID-based)

Timers identified by a string ID. Ideal for cooldowns and state-driven logic.

### 2. Runtime Timers

Flexible timers with support for:

* Duration
* Callbacks on completion
* Interval ticks
* Continuous updates

### 3. Stopwatches

Track elapsed time with optional reverse mode.

---

## ⚙️ Features

* Callback-based architecture:

  * `OnUpdate`
  * `OnTick`
  * `OnFinished`
* Interval (tick) support for repeated logic
* Unique ID system (string and int-based)
* Decoupled time management from gameplay logic
* Persistent timers independent from caller lifecycle
* Safe collection handling during iteration (no runtime errors)

---

## 🛡️ Safe Update Handling

Runtime timers are managed using a deferred add/remove system to avoid modifying collections during iteration.

This ensures:

* Stability during updates
* No invalid operation exceptions
* Safe dynamic timer creation/removal

---

## ❓ Why not use Update?

Using `Update()` for timers leads to:

* Repetitive and duplicated logic
* Harder debugging
* Increased coupling between systems

This system centralizes all time-based behavior into a reusable manager.

---

## 🔍 Example (Before / After)

### ❌ Before (Manual Update logic)

```csharp
void Update() {
    if (cooldown > 0) {
        cooldown -= Time.deltaTime;
        if (cooldown <= 0) ExecuteAbility();
    }
}
```

### ✅ After (Using Timer Manager)

```csharp
TimersManager.Instance.StartRuntimeTimer(5f, () => ExecuteAbility());
```

---

## 🔁 Example (With Interval / Tick)

```csharp
TimersManager.Instance.StartRuntimeTimer(
    5f,
    () => Debug.Log("Finished"),
    1f,
    () => Debug.Log("Tick every second")
);
```

---

## ⏱️ Countdown Example

```csharp
TimersManager.Instance.SetCountdownWithID(
    "AttackCooldown",
    5f,
    () => ExecuteAbility()
);
```

---

## 🕒 Stopwatch Example

```csharp
TimersManager.Instance.StartStopwatch("FallTime");
```

---

## 🧠 Architecture Overview

TimersManager
├── Countdown Timers (string ID)
├── Runtime Timers (int ID)
│   ├── OnUpdate
│   ├── OnTick
│   └── OnFinished
└── Stopwatches

---

## 🛠️ Technologies

* Unity
* C#

---

## 📌 Notes

This system was designed to be reusable across multiple projects and scalable for complex gameplay scenarios such as:

* Ability cooldowns
* AI behaviors
* Animation timing
* State transitions
* Event scheduling

---

## 📷 (Optional)

Add screenshots or GIFs here showing the system in action.
