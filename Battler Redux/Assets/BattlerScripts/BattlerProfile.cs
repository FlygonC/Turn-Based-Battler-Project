using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
public class BattlerProfile
{

    public BattlerIdentity identity;
    // Level Ups
    public int level;
    //Equips
    public Weapon weapon;
    public Armor armor;
    //other things
    public BattlerStats profileStats;

    public List<BattleSkill> attacks = new List<BattleSkill>();
    public List<BattleSkill> specials = new List<BattleSkill>();

    public List<BattlerHeldItem> items = new List<BattlerHeldItem>();

    public float MaxHP
    {
        get
        {
            return profileStats.maxHP;
        }
    }
    [SerializeField]
    float currentHP;
    public float CurrentHP
    {
        get
        {
            return currentHP;
        }
        set
        {
            currentHP = Mathf.Clamp(value, 0, MaxHP);
        }
    }

    public float MaxMP
    {
        get
        {
            return profileStats.maxMP;
        }
    }
    [SerializeField]
    float currentMP;
    public float CurrentMP
    {
        get
        {
            return currentMP;
        }
        set
        {
            currentMP = Mathf.Clamp(value, 0, MaxMP);
        }
    }

    public void CompileProfile()
    {
        foreach (BattleSkill i in attacks)
        {
            i.weaponElement = weapon.weaponElement;
            i.weaponPower = weapon.weaponPower;
        }
        foreach (BattleSkill i in specials)
        {
            i.weaponElement = weapon.weaponElement;
            i.weaponPower = weapon.weaponPower;
        }

        profileStats = identity.baseStats.Calculate(level) + weapon.statEffects + armor.statEffects;
    }

}
