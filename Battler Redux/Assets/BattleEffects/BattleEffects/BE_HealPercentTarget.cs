using UnityEngine;
using System.Collections;

public class BE_HealPercentTarget : BattleEffect
{
    [Space]
    public float percentToHeal;

    protected override void Start()
    {
        base.Start();
        
    }

    public override void Trigger()
    {
        base.Trigger();
        foreach (Battler b in spawner.Targets)
        {
            float ToHeal = Mathf.Ceil(b.Stats.MaxHP * (percentToHeal / 100));
            spawner.Manager.HealBattler(ToHeal, b);
            //b.aura.Add(spawner.Skill.element, amountToHeal);
        }
    }
}
