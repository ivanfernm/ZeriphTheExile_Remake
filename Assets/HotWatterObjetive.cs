using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotWatterObjetive : MonoBehaviour
{
    public LevelManager lvlmanager;

    public LevelObjective lvlobj;

    public BaseTask task;
    private void Start()
    {
        lvlmanager = LevelManager.Instance;
        
        //lo primero que tendria que hacer es crear el objetico
        lvlobj = new LevelObjective();
        
        //crear la task
        task = new HotWaterTask("water1", 10);
        
        //Agregar la task
        lvlobj.AddTask(task);
        
        //agregar el objetivo al lvl manager
        lvlmanager.AddObjective(lvlobj);
    }
}

public class HotWaterTask: BaseTask
{
    public HotWaterTask(string id, float requiredAmount) : base(id, requiredAmount)
    {
    }

    public override void UpdateProgress(float amount)
    {
        if (!isActive) return;

        if (Input.GetKeyDown(KeyCode.A))
        {
            currentProgress += amount;

            currentProgress = Mathf.Clamp(currentProgress, 0f, requiredProgress);
        }
    }
}