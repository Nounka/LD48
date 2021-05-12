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

    public TaskManager taskManager;
    public AIState currentState;
    public Task currentTask;

    public float baseSpeed;

    public AudioSource audiosource;

    public Animator animator;

    public BoxCollider selectCollider;

    public bool isDying;

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

    public void TakeDommage(float _dommage, WorldEntities _actor)
    {
        healthCurrent -= _dommage;
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
        currentState = new IdleState(this);
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
            AIState nextState;
            do
            {
                nextState = currentState.DoTransition();
                if(nextState != null)
                {
                    currentState.Exit();
                    currentState = nextState;
                    currentState.Enter();
                }
            } while (nextState != null);
            currentState.Execute();
        }
    }
}
