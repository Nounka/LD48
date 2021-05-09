using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameManager : MonoBehaviour
{
    public List<NameInstance> names;
    [System.Serializable]
    public struct NameInstance
    {
        public string name;
        public int number;

        public NameInstance(string name, int number)
        {
            this.name = name;
            this.number = number;
        }
    }

    public string GetEpithet(string _name)
    {
        int x = GetInstance(_name);

        names[x] = new NameInstance(names[x].name,names[x].number+1);

        switch (names[x].number)
        {
            case 1:
                return " The 1st";

            case 2:
                return " The 2nd";

            case 3:
                return " The 3rd";

            default:
                return " The " + names[x].number + "th";


        }
        

    }

    public int GetInstance(string _name)
    {
        for(int x = 0;x<names.Count;x++) 
        {
            if (names[x].name == _name)
            {
                return x;
            }
        }
        names.Add(new NameInstance(_name, 0));
        return names.Count - 1;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
