using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class BattleSkill : BattleAction
{
    [SerializeField]
    private SkillBase baseSkill;
    // Skill mods
    public int skillLevel = 1;
    [HideInInspector]
    public float weaponPower = 0;
    [HideInInspector]
    public Element weaponElement;

    // Skill properties
    public string name
    {
        get
        {
            return baseSkill.name;
        }
    }
    public Sprite icon
    {
        get
        {
            return baseSkill.icon;
        }
    }
    
    public float cost
    {
        get
        {
            return baseSkill.cost * skillLevel;
        }
    }

    public float setBack
    {
        get
        {
            return baseSkill.setBack;
        }
    }
    

    public BattleEffectsSpawner effects
    {
        get
        {
            return baseSkill.effects;
        }
    }
    

    public override AttackTargeting target
    {
        get
        {
            return baseSkill.target;
        }
    }

    public override void CommitAction(Battler _user, List<Battler> _targets)
    {
        BattleEffectsSpawner newEffects = GameObject.Instantiate(effects);
        newEffects.Init(BattleManager.main, _user, _targets, this);
        newEffects.levelMod = skillLevel;
        _user.MP -= cost;
    }

}
