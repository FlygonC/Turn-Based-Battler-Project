using UnityEngine;
using System.Collections;

public struct AttackDamageProperties
{

    public AttackDamageProperties(Element _ele, DamageType _type, float _power, float _accuracy)
    {
        element = _ele;
        type = _type;
        power = _power;
        accuracyMod = _accuracy;
    }

    public Element element;
    public DamageType type;
    public float power;
    public float accuracyMod;

}
