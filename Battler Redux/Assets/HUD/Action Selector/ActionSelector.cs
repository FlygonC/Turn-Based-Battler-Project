using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class ActionSelector : MonoBehaviour
{

    public Battler focus;

    [SerializeField]
    private GridLayoutGroup attacks;
    [SerializeField]
    private GridLayoutGroup specials;
    [SerializeField]
    private GridLayoutGroup items;
    [SerializeField]
    private GridLayoutGroup strategy;
    [SerializeField]
    private Text nameTag;
    
    public SkillButton skillButtonPrefab;
    public ItemButton itemButtonPrefab;
    
    //public List<StrategyButton> strategyButtons = new List<StrategyButton>();

    public BattleManager manager;

	// Use this for initialization
	void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    public void Setup(Battler _focus)
    {
        focus = _focus;
        nameTag.text = focus.name;
        //Attacks
        foreach (BattleSkill i in focus.attacks)
        {
            SkillButton newButton = Instantiate(skillButtonPrefab);
            newButton.transform.SetParent(attacks.transform);
            newButton.attack = i;
            //attackButtons.Add(newButton);
        }
        //Specials
        foreach (BattleSkill i in focus.specials)
        {
            SkillButton newButton = Instantiate(skillButtonPrefab);
            newButton.transform.SetParent(specials.transform);
            newButton.attack = i;
            //attackButtons.Add(newButton);
        }
        //Items
        foreach (BattleItem i in focus.items)
        {
            ItemButton newButton = Instantiate(itemButtonPrefab);
            newButton.transform.SetParent(items.transform);
            
            newButton.item = i;
            //attackButtons.Add(newButton);
        }
        // Test
        /*foreach (BattleAction i in focus.allActions.Where(x => x is BattleSkill))
        {
            SkillButton newButton = Instantiate(skillButtonPrefab);
            newButton.transform.SetParent(items.transform);

            newButton.attack = (i as BattleSkill);
            //attackButtons.Add(newButton);
        }*/

        /*if (focus.rank == 0 && manager.GetPlayersInRank(0).Count <= 1)
        {
            strategyButtons[0].GetComponent<Image>().color = Color.grey;
        }*/
    }
}
