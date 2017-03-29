using UnityEngine;
using System.Collections;

public class ItemButton : ButtonCustom
{

    public BattleItem item;
    public ActionSelector actionselector;

    protected override void Start()
    {
        base.Start();
        actionselector = GetComponentInParent<ActionSelector>();
        text.text = "";
        /*if (item.heldItem.stack > 1)
        {*/
            text.text = item.heldItem.stack.ToString();
        //}
        sprite.sprite = item.heldItem.useItem.icon;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (clicked)
        {
            actionselector.manager.SelectAction(item);
        }
    }
}
