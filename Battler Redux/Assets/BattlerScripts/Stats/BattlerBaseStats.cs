using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum StatsEnum : int { HP, MP, Attack, Defence, MagAttack, MagDefence, Accuracy, Evade, Speed }

[System.Serializable]
public class BattlerBaseStats
{
    [System.Serializable]
    public class BaseStat
    {
        public float flat;
        public float growth;
    }

    /*public BattlerBaseStats()
    {
        for (int i = 0; i < (int)StatsEnum.NumOfStats; i++)
        {
            stats.Add(new BaseStat());
        }
    }*/

    //public List<BaseStat> stats = new List<BaseStat>(9);

    public BaseStat HP;
    public BaseStat HPRegen;
    public BaseStat MP;
    public BaseStat MPRegen;

    public BaseStat Attack;
    public BaseStat Defence;
    public BaseStat MagAttack;
    public BaseStat MagDefence;
    public BaseStat Accuracy;
    public BaseStat Evade;

    public float Speed;


    public BattlerStats Calculate(float _level)
    {
        BattlerStats final = new BattlerStats();

        /*for (int i = 0; i < stats.Count; i++)
        {
            final.stats[i] = Calculator.CalcStat(stats[i], _level);
        }*/

        final.maxHP = Calculator.CalcStatGrowth(HP, _level, 0);
        final.HPregen = Calculator.CalcStatGrowth(HPRegen, _level, 0);
        final.maxMP = Calculator.CalcStatGrowth(MP, _level, 0);
        final.MPregen = Calculator.CalcStatGrowth(MPRegen, _level, 0);

        final.attack = Calculator.CalcStatGrowth(Attack, _level, 0);
        final.defence = Calculator.CalcStatGrowth(Defence, _level, 0);
        final.magAttack = Calculator.CalcStatGrowth(MagAttack, _level, 0);
        final.magDefence = Calculator.CalcStatGrowth(MagDefence, _level, 0);
        final.accuracy = Calculator.CalcStatGrowth(Accuracy, _level, 0);
        final.evade = Calculator.CalcStatGrowth(Evade, _level, 0);

        final.speed = Speed;

        return final;
    }
}
