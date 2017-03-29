using UnityEngine;
using System.Collections;

[System.Serializable]
public class BattlerAnimationSet
{

    public SpriteAnimation Idle;

    public SpriteAnimation Attack1;

    public SpriteAnimation Spell;
    public float timeToCast = 0.25f;

    public SpriteAnimation ItemUse;

    public SpriteAnimation Hurt;

    public SpriteAnimation Dead;
	
    public SpriteAnimation getAnimation(AttackUseType _anim)
    {
        switch (_anim)
        {
            case AttackUseType.Attack1:
                return Attack1;

            case AttackUseType.Spell1:
                return Spell;

            case AttackUseType.Item:
                return ItemUse;
                
        }
        return null;
    }

}
