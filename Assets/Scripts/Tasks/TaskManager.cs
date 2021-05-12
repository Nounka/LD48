using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager
{
    public List<WorldEntities> idleCitizens;

    public List<Task> unassignedTasks;
    public List<WorkPair> unreachablePairs; 
    public List<Task> assignedTasks;

    public Task GetTask(WorldEntities owner)
    {
        foreach(Task t in unassignedTasks)
        {
            if (t.TaskDoable() == Task.TaskBlockage.doable && )
            {

            }
        }

        return null;
    }

    public class WorkPair
    {
        WorkPair(WorldEntities _worker, Task _task)
        {
            worker = _worker;

        }

        public WorldEntities worker;
        public Task task;
    }
}
