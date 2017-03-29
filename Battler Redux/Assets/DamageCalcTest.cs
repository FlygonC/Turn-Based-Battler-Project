using UnityEngine;
using System.Collections;

public class DamageCalcTest : MonoBehaviour
{
    public float attackPower;
    [Header("Attacker")]
    public float attack;
    public float accuracy;
    [Range(0,1)]
    public float elemDegree = 0;
    //public int attackHits = 1;

    [Header("Defender")]
    public float armoreResist = 1;
    public float evade = 1;
    [Range(0,3)]
    public float elemResist = 1;

    public float baseDamage;
    public float elemMod;
    public float finalDamage;

    public float chanceToCrit;

    public float effectMod;
    public float effectAmount;
    //public float chanceToMiss;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        baseDamage = attack * attackPower / armoreResist;
        chanceToCrit = (accuracy / evade) * Calculator.BaseCriticalChance;

        elemMod = 1 + ((1 - elemResist) * elemDegree);
        effectMod = 1 + (1 - elemResist);
        effectAmount = (baseDamage * elemDegree) * effectMod;

        finalDamage = baseDamage * elemMod /* (1 + (chanceToCrit + chanceToMiss))*/;
        
    }
}
