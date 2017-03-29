using UnityEngine;
using System.Collections;

public class BE_HealTargetHP : BattleEffect
{
    [Space]
    public float amountToHeal;

    protected override void Start()
    {
        base.Start();
        //amountToHeal = spawner.Skill.power;
    }

    public override void Trigger()
    {
        base.Trigger();
        foreach (Battler b in spawner.Targets)
        {
            spawner.Manager.HealBattler(amountToHeal * spawner.levelMod, b);
            //b.aura.Add(spawner.Skill.element, amountToHeal);
        }
    }

}
