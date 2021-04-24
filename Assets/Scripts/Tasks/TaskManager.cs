using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public List<Citizen> idleCitizens;

    public List<Task> unassignedTasks;

    public void AttributeTasks()
    {
        for(int x = 0; x < unassignedTasks.Count;)
        {

        }
    }

    /*public bool AttributeTask(Task _task)
    {

    }

    public Citizen GetCloser(Task _task)
    {
        Citizen retour = null;
        for(int x = 0; x < idleCitizens.Count; x++)
        {
            if(idleCitizens[x].role)
        }
        return retour;
    }

    public void ArrangeList()
    {

    }*/
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
