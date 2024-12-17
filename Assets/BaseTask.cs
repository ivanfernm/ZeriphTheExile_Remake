using UnityEngine;
using System;
using System.Collections.Generic;
[Serializable] public abstract class BaseTask
{
    // Unique identifier for the task
    public string taskId;

    // Current progress of the task
    [SerializeField] protected float currentProgress = 0f;

    // Maximum progress needed to complete the task
    [SerializeField] protected float requiredProgress = 1f;

    // Is the task currently active
    public bool isActive = true;

    // Has the task been completed
    public bool isCompleted => currentProgress >= requiredProgress;

    // Constructor
    public BaseTask(string id, float requiredAmount)
    {
        taskId = id;
        requiredProgress = requiredAmount;
    }

    // Abstract method to update task progress
    public abstract void UpdateProgress(float amount);

    // Reset the task
    public virtual void ResetTask()
    {
        currentProgress = 0f;
        isActive = true;
    }

    // Get current progress
    public float GetProgress() => currentProgress;
    public float GetRequiredProgress() => requiredProgress;
}