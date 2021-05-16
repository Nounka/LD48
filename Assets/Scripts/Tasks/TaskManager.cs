using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager
{
    public List<Task> unassignedTasks;
    public List<Task> assignedTasks;

    public Task GetTask(WorldEntities entity)
    {
        foreach(Task t in unassignedTasks)
        {
            if (t.TaskDoable() == Task.TaskBlockage.doable) // TODO : Sort task by priority / distance / whatever ...
            {
                unassignedTasks.Remove(t);
                assignedTasks.Add(t);
                return t;
            }
        }

        return null;
    }

    public void ReleaseTask(Task task)
    {
        assignedTasks.Remove(task);
        unassignedTasks.Add(task);
    }

    public void EndTask(Task task)
    {
        assignedTasks.Remove(task);
    }

    public void RegisterTask(Task task)
    {
        unassignedTasks.Add(task);
    }
}
