using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Calculator
{

    // 1.047129f
    public const float StatGrowthConstant = 1.047129f;
    public const float BaseCriticalChance = 1f / 16f;
    //public const float RangeFalloffMultiplier = 1.15f;

    public static float CalcStatGrowth(BattlerBaseStats.BaseStat _stat, float _level, float _levelBonus = 0f)
    {
        return Mathf.Floor( _stat.flat + _stat.growth * (_level / 50f)) + (_level * _levelBonus);
    }
    public static float CalcStatExponent(float _base, float _level, float _levelBonus = 0f)
    {
        return Mathf.Floor(_base * Mathf.Pow(Calculator.StatGrowthConstant, _level)) + (_level * _levelBonus);
    }

    /*public static float DamageCalc(Battler _attacker, AttackDamageProperties _attack, Battler _defender, bool _critical, List<float> _addedMods = null)
    {
        float finalDamage = 0;

        //float levelPower = _attacker.profile.level / 25;
        float levelPower = 1;

        float attackPower = _attack.power;
        float matchup = 1;
        switch (_attack.type)
        {
            case DamageType.Physical:
                matchup = _attacker.Stats.Attack / _defender.Stats.Defence;
                break;
            case DamageType.Magic:
                matchup = _attacker.Stats.MagAttack / _defender.Stats.MagDefence;
                break;
            default:
                matchup = 1;
                break;
        }
        
        float criticalMod = 1;
        if (_critical)
        {
            criticalMod = 1.5f;
        }

        float addedMods = 1;
        if (_addedMods != null)
        {
            foreach (float i in _addedMods)
            {
                addedMods *= i;
            }
        }

        finalDamage = attackPower * levelPower * matchup * criticalMod * addedMods;
        //_defender.BurnChill -= effect;
        Debug.Log(_attacker.name + " > " + _defender.name + "  Damage " + attackPower + ", matchup " + matchup + ", Crit! " + criticalMod + "  = " + finalDamage);
        
        //return Mathf.Floor(finalDamage);
        return finalDamage;
    }*/

    public static float DamageCalc(Battler _attacker, AttackDamageProperties _attack, Battler _defender, bool _critical)
    {
        // Attack Power vs. Defence Power
        float matchup = 0;
        switch (_attack.type)
        {
            case DamageType.Physical:
                matchup = _attacker.Stats.Attack - (_defender.Stats.Defence / 2);
                break;
            case DamageType.Magic:
                matchup = _attacker.Stats.MagAttack - (_defender.Stats.MagDefence / 2);
                break;
            case DamageType.Pierce:
            default:
                matchup = 0;
                break;
        }
        // Element Aura
        float auraMod = 1;

        float critMod = 1;
        if (_critical)
        {
            critMod = 1.5f;
        }

        float finalDamage = Mathf.Floor((_attack.power + matchup) * auraMod * critMod);
        Debug.Log(_attacker.name + _defender.name + "  Damage " + _attack.power + ", matchup " + matchup + ", auraMod " + auraMod + ", Crit! " + critMod + "  = " + finalDamage);

        return Mathf.Max(1, finalDamage);
    }

}

public class DamageReturn
{
    public float rawDamage;
    public float elementDamage;
    public Element element;
}