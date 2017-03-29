using UnityEngine;
using System.Collections;

public class SkillButton : ButtonCustom
{

    public BattleSkill attack;
    public ActionSelector actionselector;

    protected override void Start()
    {
        base.Start();
        actionselector = GetComponentInParent<ActionSelector>();
        text.text = "";
        if (attack.cost > 0)
        {
            text.text = attack.cost + "";
        }
        sprite.sprite = attack.icon;
        if (attack.cost > actionselector.focus.MP)
        {
            sprite.color = Color.grey;
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (clicked)
        {
            if (actionselector.focus.MP >= attack.cost)
            {
                actionselector.manager.SelectAction(attack);
            }
        }
    }
}
