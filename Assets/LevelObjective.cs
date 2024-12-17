using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class LevelObjective
{
    // List of tasks required to complete this objective
    public List<BaseTask> requiredTasks = new List<BaseTask>();

    // Check if all required tasks are completed
    public bool IsObjectiveCompleted()
    {
        foreach (var task in requiredTasks)
        {
            if (!task.isCompleted)
                return false;
        }
        return true;
    }

    // Add a task to the objective
    public void AddTask(BaseTask task)
    {
        requiredTasks.Add(task);
    }

    // Remove a specific task
    public void RemoveTask(BaseTask task)
    {
        requiredTasks.Remove(task);
    }

    // Reset all tasks in the objective
    public void ResetObjective()
    {
        foreach (var task in requiredTasks)
        {
            task.ResetTask();
        }
    }
}