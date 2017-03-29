using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
public class BattlerStats
{

    /*public BattlerSimpleStats()
    {
        for (int i = 0; i < (int)StatsEnum.NumOfStats; i++)
        {
            stats.Add(new float());
        }
    }*/

    //public List<float> stats = new List<float>(9);

    public float maxHP;
    public float HPregen;
    public float maxMP;
    public float MPregen;

    public float attack;
    public float defence;
    public float magAttack;
    public float magDefence;
    public float accuracy;
    public float evade;

    public float speed;

    public float MaxHP
    {
        get
        {
            return maxHP;
        }
    }

    public float MaxMP
    {
        get
        {
            return maxMP;
        }
    }

    public float Attack
    {
        get
        {
            return attack;
        }
    }

    public float Defence
    {
        get
        {
            return defence;
        }
    }

    public float MagAttack
    {
        get
        {
            return magAttack;
        }
    }

    public float MagDefence
    {
        get
        {
            return magDefence;
        }
    }

    public float Accuracy
    {
        get
        {
            return accuracy;
        }
    }

    public float Evade
    {
        get
        {
            return evade;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }
    }

    public void Add(StatsEnum _stat, float _amount)
    {
        switch (_stat)
        {
            case StatsEnum.HP:
                break;
            case StatsEnum.MP:
                break;

            case StatsEnum.Attack:
                attack += _amount;
                break;

            case StatsEnum.Defence:
                defence += _amount;
                break;

            case StatsEnum.MagAttack:
                magAttack += _amount;
                break;

            case StatsEnum.MagDefence:
                magDefence += _amount;
                break;

            case StatsEnum.Accuracy:
                accuracy += _amount;
                break;

            case StatsEnum.Evade:
                evade += _amount;
                break;

            case StatsEnum.Speed:
                attack += _amount;
                break;

            default:
                break;
        }
    }

    public BattlerStats PercentPlus(BattlerStats _multi)
    {
        BattlerStats final = new BattlerStats();

        final.maxHP = maxHP;
        final.HPregen = HPregen;
        final.maxMP = maxMP;
        final.MPregen = MPregen;

        final.attack = attack * (1 + (_multi.attack / 100f));
        final.defence = defence * (1 + (_multi.defence / 100f));
        final.magAttack = magAttack * (1 + (_multi.magAttack / 100f));
        final.magDefence = magDefence * (1 + (_multi.magDefence / 100f));
        final.accuracy = accuracy * (1 + (_multi.accuracy / 100f));
        final.evade = evade * (1 + (_multi.evade / 100f));

        final.speed = speed * (1 + (_multi.speed / 100f));

        return final;
    }

    public static BattlerStats operator +(BattlerStats c1, BattlerStats c2)
    {
        BattlerStats final = new BattlerStats();

        /*for (int i = 0; i < Mathf.Min(c1.stats.Count, c2.stats.Count); i++)
        {
            final.stats[i] = c1.stats[i] + c2.stats[i];
        }*/

        final.maxHP =       c1.maxHP +      c2.maxHP;
        final.HPregen =     c1.HPregen +    c2.HPregen;
        final.maxMP =       c1.maxMP +      c2.maxMP;
        final.MPregen =     c1.MPregen +    c2.MPregen;

        final.attack =      c1.attack +     c2.attack;
        final.defence =     c1.defence +    c2.defence;
        final.magAttack =   c1.magAttack +  c2.magAttack;
        final.magDefence =  c1.magDefence + c2.magDefence;
        final.accuracy =    c1.accuracy +   c2.accuracy;
        final.evade =       c1.evade +      c2.evade;

        final.speed =       c1.speed +      c2.speed;

        return final;
    }

}
