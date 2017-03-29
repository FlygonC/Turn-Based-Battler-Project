using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HudTargetor : ButtonCustom
{
    
    public Battler focus;

    protected override void Update()
    {
        base.Update();
        transform.position = Camera.main.WorldToScreenPoint(focus.transform.position);
    }
}
