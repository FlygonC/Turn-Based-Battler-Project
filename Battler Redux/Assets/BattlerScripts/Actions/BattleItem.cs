using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleItem : BattleAction
{

    public BattlerHeldItem heldItem;

    public BattleItem(BattlerHeldItem _item)
    {
        heldItem = _item;
    }

    public override AttackTargeting target
    {
        get
        {
            return heldItem.useItem.effect.target;
        }
    }

    public override void CommitAction(Battler _user, List<Battler> _targets)
    {
        BattleEffectsSpawner newEffects = GameObject.Instantiate(heldItem.useItem.effect.effects);
        newEffects.Init(BattleManager.main, _user, _targets, heldItem.useItem.effect);
        heldItem.stack--;
    }

}
