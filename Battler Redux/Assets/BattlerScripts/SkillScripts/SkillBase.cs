using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public enum AttackTargeting { Enemy, Front, Ally, Any, AllEnemy, AllAlly, Self };
public enum SkillType { WeaponSkill, SpecialSkill }
public enum DamageType { Physical, Magic, Pierce };
public enum SkillNature { Damage, Heal, }

[CreateAssetMenu(fileName = "NewSkill", menuName = "New Skill", order = 1)]
public class SkillBase : ScriptableObject
{
    new public string name;
    public Sprite icon;

    public BattleEffectsSpawner effects;

    //public bool isWeaponSkill;

    public AttackTargeting target;

    //public Element element;

    public SkillNature nature;

    public float cost;
    public float setBack = 100;

    //public AttackUseType useAnimation;
    
    
    //public float power = 5;
    //public DamageType damageType;
    //public float accuracyMod = 1;
}
