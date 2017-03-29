using UnityEngine;
using System.Collections;

public class BE_BuffTargetStat : BattleEffect
{
    [Space]
    public StatsEnum stat;
    public float amount;

    public override void Trigger()
    {
        base.Trigger();
        foreach (Battler i in spawner.Targets)
        {
            i.statChanges.Add(stat, amount * spawner.levelMod);
            spawner.Manager.SpawnDamageText(i.transform.position, stat.ToString() + " Up", Color.white);
        }
    }

}
