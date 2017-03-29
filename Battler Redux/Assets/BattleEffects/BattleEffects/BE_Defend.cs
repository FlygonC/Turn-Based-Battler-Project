using UnityEngine;
using System.Collections;

public class BE_Defend : BattleEffect
{

    public override void Trigger()
    {
        base.Trigger();
        foreach (Battler i in spawner.Targets)
        {
            i.defending = true;
        }
    }

}
