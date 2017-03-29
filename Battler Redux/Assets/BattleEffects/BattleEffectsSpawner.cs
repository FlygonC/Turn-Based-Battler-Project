using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum AttackUseType { Attack1, Spell1, Item };

public class BattleEffectsSpawner : MonoBehaviour
{
    
    private Battler source;
    public Battler Source
    {
        get
        {
            return source;
        }
    }

    /*private BattleSkill skill;
    public BattleSkill Skill
    {
        get
        {
            return skill;
        }
    }*/

    private List<Battler> targets = new List<Battler>();
    public List<Battler> Targets
    {
        get
        {
            return targets;
        }
    }

    public int levelMod = 1;

    public AttackUseType userAnimToPlay;

    private List<BattleEffect> effects = new List<BattleEffect>();
    
    private float life = 0;
    private bool initalized = false;

    private BattleManager manager;
    public BattleManager Manager
    {
        get
        {
            return manager;
        }
    }
    
    public void Init(BattleManager _manager, Battler _source, List<Battler> _targets, BattleSkill _skill)
    {
        manager = _manager;
        source = _source;
        targets = _targets;
        //skill = _skill;

        effects = GetComponents<BattleEffect>().ToList();

        initalized = true;
    }
    
	void Update ()
    {
        if (initalized)
        {
            if (life == 0)
            {
                source.GetComponent<SpriteAnimator>().Play(source.anims.getAnimation(userAnimToPlay));
            }

            foreach (BattleEffect i in effects.Where(x => x.Triggered == false))
            {
                if (life >= i.triggerTime)
                {
                    i.Trigger();
                    i.Triggered = true;
                }
            }
            

            if (Done())
            {
                Destroy(gameObject);
            }
            life += Time.deltaTime;
        }
	}

    bool Done()
    {
        foreach (BattleEffect i in effects)
        {
            if (i.Triggered == false)
            {
                return false;
            }
        }
        return true;
    }
    
}
