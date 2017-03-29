using UnityEngine;
using System.Collections;

public class BE_DamageTarget : BattleEffect
{
    [Space]
    public Element element;
    public DamageType type;
    public float power;
    public float accuracyMod;

    private AttackDamageProperties props
    {
        get
        {
            return new AttackDamageProperties(element, type, power * spawner.levelMod, accuracyMod);
        }
    }

    protected override void Start()
    {
        base.Start();
        /*element = spawner.Skill.element;
        type = spawner.Skill.type;
        power = spawner.Skill.power;*/
    }

    public override void Trigger()
    {
        base.Trigger();
        foreach (Battler b in spawner.Targets)
        {
            spawner.Manager.BattlerDamageOther(spawner.Source, props, b);
        }
    }
}
