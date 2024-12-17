using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
      // Singleton instance
    public static LevelManager Instance { get; private set; }

    // Current level's objectives
    public List<LevelObjective> currentObjectives = new List<LevelObjective>();

    // Event triggered when all objectives are completed
    public event Action OnLevelCompleted;

    // Event triggered when a task is updated
    public event Action<BaseTask> OnTaskUpdated;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Add an objective to the current level
    public void AddObjective(LevelObjective objective)
    {
        currentObjectives.Add(objective);
    }

    // Check task progress and update
    public void CheckTask(string taskId, float progressAmount)
    {
        bool taskFound = false;

        // Search through all objectives and their tasks
        foreach (var objective in currentObjectives)
        {
            foreach (var task in objective.requiredTasks)
            {
                if (task.taskId == taskId)
                {
                    task.UpdateProgress(progressAmount);
                    taskFound = true;

                    // Notify listeners that task was updated
                    OnTaskUpdated?.Invoke(task);

                    // Check if this objective is now complete
                    if (objective.IsObjectiveCompleted())
                    {
                        CheckLevelCompletion();
                    }
                    break;
                }
            }

            if (taskFound) break;
        }

        if (!taskFound)
        {
            Debug.LogWarning($"Task with ID {taskId} not found!");
        }
    }

    // Check if all objectives are completed
    private void CheckLevelCompletion()
    {
        foreach (var objective in currentObjectives)
        {
            if (!objective.IsObjectiveCompleted())
            {
                return; // Level not complete yet
            }
        }

        // All objectives completed
        LevelCompleted();
    }

    // Handle level completion
    private void LevelCompleted()
    {
        Debug.Log("Level Completed!");
        OnLevelCompleted?.Invoke();
    }

    // Reset the entire level
    public void ResetLevel()
    {
        foreach (var objective in currentObjectives)
        {
            objective.ResetObjective();
        }
    }
}





