using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Battler : MonoBehaviour, /*BattlerActiveStats,*/ IComparable
{

    public BattlerProfile profile;

    public bool isAlly;
    public int rank;
    public float turnDelay = 1;
    //public float turnsPerRound;
    public bool isAlive = true;
    public bool persistAfterDeath = false;

    //public Aura aura = new Aura();
    //public float auraCapacity = 150;

    //public float HPRegen;
    //public float MPRegen;
    public BattlerStats statChanges;

    public bool defending = false;

    //public float BurnChill = 0;

    /*public float ChillMod
    {
        get
        {
            if (BurnChill < 0)
            {
                return Mathf.Max(0, 1 - (Mathf.Abs(BurnChill) / 100f));
            }
            else
            {
                return 1;
            }
        }
    }*/

    public float HP
    {
        get
        {
            return profile.CurrentHP;
        }
        set
        {
            profile.CurrentHP = value;
        }
    }
    public float MP
    {
        get
        {
            return profile.CurrentMP;
        }
        set
        {
            profile.CurrentMP = value;
        }
    }

    public List<BattleSkill> attacks
    {
        get
        {
            return profile.attacks;
        }
    }
    public List<BattleSkill> specials
    {
        get
        {
            return profile.specials;
        }
    }
    public List<BattleItem> items
    {
        get
        {
            List<BattleItem> final = new List<BattleItem>();

            foreach (BattlerHeldItem i in profile.items.Where(x => x.stack > 0))
            {
                final.Add(new BattleItem(i));
            }

            return final;
        }
    }

    public List<BattleSkill> allAttacks
    {
        get
        {
            List<BattleSkill> final = attacks;
            final = final.Concat(specials).ToList();
            //final = final.Concat(items).ToList();
            return final;
        }
    }

    public List<BattleAction> allActions
    {
        get
        {
            List<BattleAction> final = new List<BattleAction>();
            
            foreach (BattleAction i in attacks)
            {
                final.Add(i);
            }
            foreach (BattleAction i in specials)
            {
                final.Add(i);
            }
            foreach (BattleAction i in items)
            {
                final.Add(i);
            }

            return final;
        }
    }

    public BattlerAnimationSet anims
    {
        get
        {
            return profile.identity.animation;
        }
    }
    //public float animationTime = 0;

    public bool idle
    {
        get
        {
            return animator.spriteAnimation == anims.Idle || animator.spriteAnimation == anims.Dead;
        }
    }

    public BattlerStats Stats
    {
        get
        {
            return profile.profileStats + statChanges;
        }
    }

    
    private SpriteAnimator animator;
    
	void Start ()
    {
        animator = GetComponent<SpriteAnimator>();
        //profile.CompileProfile();
        GetComponent<SpriteAnimator>().Start();
        animator.Play(profile.identity.animation.Idle, true);
        if (isAlly == false)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        //profile.CompileProfile();
	}
	
	void Update ()
    {
	    if (animator.animationEnded)
        {
            animator.Play(anims.Idle);
        }
        if (isAlive == false && (animator.animationEnded || animator.spriteAnimation == anims.Idle))
        {
            animator.Play(anims.Dead);
        }
        //stats.Calculate();
	}

    /*public void Hurt(float _amount)
    {
        healthPoints -= Mathf.Min(healthPoints, _amount);
    }
    public void Heal(float _amount)
    {
        healthPoints += Mathf.Min(MaxHP - healthPoints, _amount);
    }*/

    public int CompareTo(object obj)
    {
        if (obj == null)
        {
            return 1;
        }
        Battler otherBattler = obj as Battler;
        if (otherBattler != null)
        {
            return (this.turnDelay / this.Stats.Speed).CompareTo(otherBattler.turnDelay / otherBattler.Stats.Speed);
        }
        else
        {
            throw new ArgumentException("obj is not a Battler!");
        }
    }
}
