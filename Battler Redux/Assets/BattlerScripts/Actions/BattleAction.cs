using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleAction
{
    //[HideInInspector]
    //public Battler user;
    //public List<Battler> targets;
    public virtual AttackTargeting target
    {
        get { return AttackTargeting.Any; }
    }
    
    public virtual void CommitAction(Battler _user, List<Battler> _targets)
    {

    }

}
