using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BattleEffectsSpawner))]
public class BattleEffect : MonoBehaviour
{

    public float triggerTime;
    private bool triggered = false;
    public bool Triggered
    {
        get
        {
            return triggered;
        }
        set
        {
            triggered = value;
            /*if (value)
            {
                Trigger();
            }*/
        }
    }

    protected BattleEffectsSpawner spawner;

	protected virtual void Start ()
    {
        spawner = GetComponent<BattleEffectsSpawner>();
	}
    
    /*protected virtual void Update ()
    {
	    
	}*/

    public virtual void Trigger()
    {
        triggered = true;
    }
}
