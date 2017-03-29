using UnityEngine;
using System.Collections;

public class StrategyButton : ButtonCustom
{

    public PlayerSelectionStage strategy = PlayerSelectionStage.SwapPositions;
    public ActionSelector actionselector;

    
    protected override void Update()
    {
        base.Update();
        if (clicked)
        {
            switch (strategy)
            {
                case PlayerSelectionStage.SwapPositions:
                    actionselector.manager.RankChange();
                    break;
            }
        }
    }
}
