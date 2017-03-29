using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpecEffect : MonoBehaviour
{

    public Battler singleTarget;

    [SerializeField]
    private float lifeTime = 0;
    public float fullLife = 1;

    //public List<float> timeDelay = new List<float>();

    private SpriteAnimator anim;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<SpriteAnimator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (anim != null)
        {
            if (anim.animationEnded)
            {
                GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        if (lifeTime >= fullLife)
        {
            Destroy(gameObject);
        }
        lifeTime += Time.deltaTime;
        /*foreach(float i in timeDelay)
        {
            if (lifeTime >= i)
            {
                singleTarget.GetComponent<SpriteAnimator>().Play(singleTarget.anims.Hurt);
                timeDelay.Remove(i);
                break;
            }
        }*/
	}
}
