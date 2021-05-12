using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager
{
    public List<WorldEntities> idleCitizens;

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
        unassignedTasks.Remove(task);
        assignedTasks.Add(task);
    }
}
