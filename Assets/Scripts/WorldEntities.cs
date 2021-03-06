using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldEntities : WorldObject
{
    public string nom;

    //public Vector2 position;

    public bool isMoving;
    public bool isCitizen;

    public float healthCurrent;
    public float healthMax;
    public float healRegen;
    public float healSpeed;
    public float healTimer;

    public Tool currentTool;
    public List<Tool> equipements;

    public ResourceStack carrying;
    public int maxCarry;

    public State state;

    public float baseSpeed;

    public AudioSource audiosource;

    public Animator animator;

    public BoxCollider selectCollider;

    public bool isDying;

    public void ClearTask()
    {
        state.orderedTask = null;
        state.decidedTask = null;
        state.actions.Clear();

    }

    public bool CheckTask()
    {
        bool retour = true;
        foreach(Task task in state.actions)
        {
            if (task.TaskDoable() == Task.TaskBlockage.doable)
            {
               
            }
            else
            {
                retour = false;
            }
        }
        return retour;
    }

    public void RemoveTask(Task _task,Task.TaskBlockage _status)
    {
        if(_task == state.orderedTask)
        {
            if (_status == Task.TaskBlockage.done)
            {
                state.orderedTask = null;
                audiosource.clip = null;
            }
            else
            {
                state.orderedTask = null;
                audiosource.clip = null;
            }
        }
        if (_task == state.arrangedTask)
        {
            if (_status == Task.TaskBlockage.done)
            {
                state.arrangedTask = null;
                audiosource.clip = null;
            }
            else
            {
                state.arrangedTask = null;
                audiosource.clip = null;
            }
        }
    }

    public ResourceStack AddRessources(ResourceStack _ajout)
    {
        ResourceStack surplus = carrying.AddWithinCapacity(_ajout, maxCarry);
        return surplus;
        if (surplus.GetSize() > 0)
        {

            //GameState.instance.itemDrop.CreateRessourceStack()
            //return false;
        }
        else
        {
            //return true;
        }

    }

    public void TaskMoveTo(Vector2Int _position)
    {
        Map map = GameState.instance.map;
        if (_position.x >= 0 && _position.x < map.width)
        {
            if (_position.y >= 0 && _position.y < map.length)
            {
                state.orderedTask = new MoveTask(map.GetPath(map.GetTile(position.x, position.y), map.GetTile(_position.x, _position.y)));
                state.orderedTask.type = Task.TaskType.move;
                state.orderedTask.actor = this;
            }
        }
        

        
    }

    public virtual void PlaySound(AudioBank.AudioName _name)
    {
        AudioClip clip = GameState.instance.audioBank.GetSound(_name);
        if (clip != audiosource.clip)
        {
            audiosource.clip = clip;
            try {
                audiosource.volume = PersistentGameState.instance.audioVolume;
            } catch (System.Exception e) {
                // Do nothing
                audiosource.volume = 1;
            }
            audiosource.Play();
        }

    }

    public void SetAnimatorState(bool _isWalking, int _direction) 
    {
        if (_direction != -1)
        {
            animator.SetInteger("Direction", _direction);
        }
        animator.SetBool("IsMoving", _isWalking);
    }

    [System.Serializable]
    public class State
    {
        public Task orderedTask;
        public Task arrangedTask;
        public Task decidedTask;
        public List<Task> actions;

        public StateType type;

        [System.Serializable]
        public enum StateType
        {
            idle,
            military,
            moving,
            fighting,
        }
    }
    
    public Tool GetTool()
    {
        if (currentTool == null)
        {

            Tool retour;
            float value;
            if (equipements.Count > 0)
            {
                retour = equipements[0];
                value = equipements[0].stats.damagePerSec * equipements[0].stats.speedModifier;
                foreach (Tool tool in equipements)
                {
                    float comp = tool.stats.damagePerSec * tool.stats.speedModifier;
                    if (comp > value)
                    {
                        retour = tool;
                        value = comp;
                    }
                }
                return retour;
            }
            else
            {
                return new Tool(ToolType.none, 1, 0.5f, "Fist");
            }
        }
        else
        {
            return currentTool;
        }

        
    }

    public void Live()
    {
        if (state.arrangedTask != null)
        {
            if (state.arrangedTask.type != Task.TaskType.none)
            {
                state.arrangedTask.WorkTask();
            }
            else if (state.orderedTask != null)
            {
                if (state.orderedTask.type != Task.TaskType.none)
                {
                    state.orderedTask.WorkTask();
                }
            }
        }
        else if (state.orderedTask != null)
        {
            if (state.orderedTask.type != Task.TaskType.none)
            {
                state.orderedTask.WorkTask();
            }
        }
    }
    public void TakeDommage(float _dommage, WorldEntities _actor)
    {
        healthCurrent -= _dommage;
        if (state.arrangedTask != null)
        {
            if (state.arrangedTask.type == Task.TaskType.none)
            {
                state.arrangedTask = new FightMTask(this, _actor);
            }
        }
        else
        {
            state.arrangedTask = new FightMTask(this, _actor);
        }
    }

    public void Die()
    {
        GameState.instance.EntityDie(this);
        isDying = true;
        audiosource.loop = false;
        if (isCitizen)
        {
            PlaySound(AudioBank.AudioName.meurt);
        }
        else
        {
            PlaySound(AudioBank.AudioName.robotDeath);
        }
    }
    public void Disappear()
    {
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        state = new State();
    }

    // Update is called once per frame
    protected void Update()
    {
        if (isDying)
        {
            audiosource.loop = false;
            if (!audiosource.isPlaying)
            {
                Disappear();
            }
        }
        else
        {
            if (healthCurrent < 0)
            {
                Die();
            }
            Live();
        }
    }
}
