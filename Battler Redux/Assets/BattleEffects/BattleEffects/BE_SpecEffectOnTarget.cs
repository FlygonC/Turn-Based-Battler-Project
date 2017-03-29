using UnityEngine;
using System.Collections;

public class BE_SpecEffectOnTarget : BattleEffect
{
    [Space][SerializeField]
    private SpecEffect effect;

    public override void Trigger()
    {
        base.Trigger();
        foreach (Battler b in spawner.Targets)
        {
            SpecEffect newEffect = Instantiate(effect);
            newEffect.transform.position = b.transform.position;
        }
    }

}
