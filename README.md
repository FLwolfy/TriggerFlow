# TriggerFlow

> A modular trigger flow system package for Unity to easily customize and reuse trigger actions.

**TriggerFlow** is a flexible and reusable trigger system built for Unity using `ScriptableObject`s. It allows you to define trigger behavior through modular constraint and action blocks, making your gameplay logic clean, configurable, and easy to extend.

---

## Features

- Modular trigger flow architecture
- Event-based logic: OnTriggerEnter, OnTriggerStay, OnTriggerExit
- Customizable constraint and action system
- Easily composable and reusable trigger assets
- Editor extension for intuitive workflow

---

## Installation

To install via Git URL:

1. Open Unity → **Window > Package Manager**
2. Click the `+` button → **Add package from git URL...**
3. Paste the URL: `https://github.com/FLwolfy/TriggerFlow.git`
4. Click **Add** and wait for the package to import.

---

## Getting Started

### 1. Add a TriggerController

- Attach a `TriggerController` component to any GameObject with a `Collider` set to `IsTrigger = true`.
- Assign one or more `TriggerFlow` assets to it.

### 2. Create a TriggerFlow Asset

- Right-click in the **Project** panel → `Create > Trigger > TriggerFlow`
- Choose the trigger event:  
`OnTriggerEnter`, `OnTriggerStay`, or `OnTriggerExit`
- Add constraints and actions using the inspector

The system works like this:

> When the trigger event occurs:
> 1. All `TriggerConstraint` objects are evaluated via `OnCheck(Collider other)`
> 2. If all constraints return `true`, all `TriggerAction`s are executed via `OnPerform(Collider other)`

---

## Extending the System

You can create your own custom constraints and actions by inheriting from the following abstract classes:

### Trigger Constraint

```csharp
public abstract class TriggerConstraint
{
   // Return true to pass the constraint
   public abstract bool OnCheck(Collider other);
}
```

### Trigger Action

```csharp
public abstract class TriggerAction
{
    // Execute the action in detail
    public abstract void OnPerform(Collider other);
}
```

---

## Samples

To try out an example setup:

1. Open Unity → Package Manager
2. Find TriggerFlow in the list
3. Expand the Samples section
4. Click Import on the "Basic Trigger Example"

The sample includes a test scene, tag constraints, and debug actions.

---

## Notes

This is just a simple basic trigger system inspired by the behavior system in my javafx engine development. If you find this useful, please leave a ⭐, and feel free to extend more!
