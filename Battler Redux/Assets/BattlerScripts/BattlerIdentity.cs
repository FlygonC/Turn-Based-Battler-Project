using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Newidentity", menuName = "New Battler Identity", order = 2)]
public class BattlerIdentity : ScriptableObject
{

    new public string name;
    public BattlerBaseStats baseStats;
    //public float speed = 1;
    //public List<BattlerAttack> baseAttacks;
    public BattlerAnimationSet animation;
    public Sprite icon;
	
}
