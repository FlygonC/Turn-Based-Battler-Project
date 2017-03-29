using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public enum PlayerSelectionStage { NewTurn, SelectAction, SelectTarget, SwapPositions }
public enum BattleState { Idle, Selection, ActionCommand, Animation }

public class BattleManager : MonoBehaviour
{

    public static BattleManager main;

    public BattleState battleState = BattleState.Idle;

    public int turn = 0;
    public float roundTime = 100;
    public float fifthTime = 20;

    public PlayerSelectionStage PSS = PlayerSelectionStage.SelectAction;

    public Battler turnee = null;

    //public int selectedAttackIndexer = 0;
    //public BattleSkill selectedAttack;
    public BattleAction selectedAction;
    //public int targetIndexer = 0;
    public Battler target;

    public bool postActionTrigger = false;

    public List<Battler> allCharacters = new List<Battler>();
        
    private List<Battler> playerCharacters
    {
        get
        {
            return allCharacters.Where(x => x.isAlly == true).ToList();
        }
    }
    private List<Battler> enemyCharacters
    {
        get
        {
            return allCharacters.Where(x => x.isAlly == false).ToList();
        }
    }

    public List<PlayerStatusUI> playerStatusUIs = new List<PlayerStatusUI>();

    public List<EnemyStatusScript> enemyStatusUIs = new List<EnemyStatusScript>();

    //public float animationTime = 0;

    public Canvas mainCanvas;

    public HudTargetor targetorEnemy;
    private List<HudTargetor> activeTargets = new List<HudTargetor>();
    
    public ActionSelector menuPrefab;
    private ActionSelector activeMenu;
    
    public DamageText DTprefab;
    public int DTOffset = 0;
    
    public GameObject turnArrow;
    
    //[SerializeField]
    //private Effects currentEffects;

    public Battler testBattler;
    public Text debugText;

	// Use this for initialization
	void Start ()
    {
        //allCharacters = FindObjectsOfType<Battler>().ToList();
        EnterBattlers();
        //currentEffects = new Effects(this);

        // Make Singleton Manager
        if (main == null)
        {
            DontDestroyOnLoad(gameObject);
            main = this;
        }
        else if (main != this)
        {
            Destroy(gameObject);
        }

    }
	
	// Update is called once per frame
	void Update ()
    {
        /*if (NotAnimating())// While not in animation
        {
            if (turnee != null)// if turnee is found
            {
                if (turnArrow)
                {
                    turnArrow.GetComponent<SpriteRenderer>().enabled = true;
                    turnArrow.transform.position = turnee.transform.position + new Vector3(0, 1.2f, 0);
                }

                ControlsUpdate();

            }
            else//find turnee
            {
                IdleUpdate();
            }

        }
        else
        {
            //animationTime += Time.deltaTime;
            //currentEffects.EffectUpdate();
            if (turnArrow)
            {
                turnArrow.GetComponent<SpriteRenderer>().enabled = false;
            }
        }*/

        switch (battleState)
        {
            case BattleState.Idle:

                if (postActionTrigger)
                {
                    PostActionUpdate();
                    postActionTrigger = false;
                }
                turnee = LookForNextTurn();
                if (turnee != null)
                {
                    // Found Turnee
                    ChangeBattleState(BattleState.Selection);
                    if (turnee.Stats.HPregen > 0)
                    {
                        HealBattler(turnee.Stats.HPregen, turnee);
                    }
                    RestorMPBattler(turnee.Stats.MPregen, turnee);
                    //turnee.MP += turnee.Stats.MPregen;
                    turnee.defending = false;
                }
                else
                {
                    IdleUpdate();
                }

                break;

            case BattleState.Selection:

                if (turnee != null)// if turnee is found
                {
                    if (turnArrow)
                    {
                        turnArrow.GetComponent<SpriteRenderer>().enabled = true;
                        turnArrow.transform.position = turnee.transform.position + new Vector3(0, 1.2f, 0);
                    }

                    ControlsUpdate();
                }
                else
                {
                    ChangeBattleState(BattleState.Idle);
                }

                break;

            case BattleState.ActionCommand:



                break;

            case BattleState.Animation:

                if (NotAnimating())
                {

                    ChangeBattleState(BattleState.Idle);
                }

                break;


            default:
                break;
        }


        // Place battlers in position
        for (int i = 0; i < 4; i++)
        {
            List<Battler> bats = GetPlayersInRank(i);
            if (bats.Any())
            {
                foreach (Battler b in bats)
                {
                    float x = -1f - (1f * i);
                    float z = 1.2f * (bats.Count - 1);
                    z = (z / 2) - 1.2f * bats.IndexOf(b);
                    b.transform.position = new Vector3(x, z, z / 2);
                }
            }
        }
        for (int i = 0; i < 4; i++)
        {
            List<Battler> bats = GetEnemiesInRank(i);
            if (bats.Any())
            {
                foreach (Battler b in bats)
                {
                    float x = 1f + (1f * i);
                    float z = 1.2f * (bats.Count - 1);
                    z = (z / 2) - 1.2f * bats.IndexOf(b);
                    b.transform.position = new Vector3(x, z, z / 2);
                }
            }
        }

        // Update healthbars
        for (int i = 0; i < Mathf.Min(playerCharacters.Count, playerStatusUIs.Count); i++)
        {
            playerStatusUIs[i].SetFocus(playerCharacters[i]);
        }
        for (int i = 0; i < Mathf.Min(enemyCharacters.Count, enemyStatusUIs.Count); i++)
        {
            enemyStatusUIs[i].SetFocus(enemyCharacters[i]);
        }
        
        if (debugText != null)
        {
            //List<Battler> sortedBats = allCharacters;
            //sortedBats.Sort();
            string full = "" + turn + "   " + roundTime + "\n";
            foreach (Battler i in allCharacters)
            {
                full += i.name + " Lvl: " + i.profile.level + "\t HP: " + i.HP + "/" + i.Stats.MaxHP + "\t MP: " + i.MP + "/" + i.Stats.MaxMP + "\t Progress: " + i.turnDelay + " A" + (int)i.Stats.Attack + "/D" + (int)i.Stats.Defence + "/mA" + (int)i.Stats.MagAttack + "/mD" + (int)i.Stats.MagDefence + "/Ac" + (int)i.Stats.Accuracy + "/E" + (int)i.Stats.Evade + "/S" + (int)i.Stats.Speed + "\n";
            }
            //full += Input.mousePosition;
            foreach (string i in CalculateTurnOrder(8))
            {
                full += "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t" + i + "\n";
            }
            debugText.text = full;
        }
	}

    void ChangeBattleState(BattleState _state)
    {
        battleState = _state;
        if (_state == BattleState.Idle)
        {
            turnee = null;
            selectedAction = null;
            target = null;
        }
        if (_state == BattleState.Selection)
        {
            turnArrow.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            turnArrow.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void ControlsUpdate()
    {
        /*while (turnee.isAlive == false)
        {
            turn++;
        }*/
        // if Player Turn
        if (turnee.isAlly)
        {
            if (PSS == PlayerSelectionStage.NewTurn)
            {
                SetupMenu();
            }
            if (PSS == PlayerSelectionStage.SelectAction)
            {
                
            }
            if (PSS == PlayerSelectionStage.SelectTarget)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    RestartSelection();
                }
                foreach (HudTargetor i in activeTargets)
                {
                    if (i.clicked)
                    {
                        target = i.focus;
                        ConfirmAction();
                        break;
                    }
                }
            }
            if (PSS == PlayerSelectionStage.SwapPositions)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    RestartSelection();
                }
                foreach (HudTargetor i in activeTargets)
                {
                    if (i.clicked)
                    {
                        ChangeRanks();
                        RestartSelection();
                        //target = i.focus;
                        //ConfirmAction(false);
                        break;
                    }
                }
            }
        }// if Enemy turn
        else if (turnee.isAlly == false)
        {
            // For now, enemies just use their first attack on the first player
            List<BattleSkill> useableAttacks = turnee.allAttacks.Where(x => x.cost <= turnee.MP).ToList();
            selectedAction = useableAttacks[Random.Range(0, useableAttacks.Count)];
            List<Battler> validTargets = GetValidTargets();
            target = validTargets[Random.Range(0, validTargets.Count)];
            ConfirmAction();
        }
    }

    void IdleUpdate()
    {
        if (roundTime <= 0)
        {
            FullRoundUpdate();
            roundTime += 100;
        }
        if (fifthTime <= 0)
        {
            FifthRoundUpdate();
            fifthTime = 20;
        }

        // regen
        /*foreach (Battler i in allCharacters.Where(x => x.isAlive))
        {
            i.HPRegen += i.HPRegenSpeed;
            while (i.HPRegen >= 1)
            {
                i.HP++;
                i.HPRegen--;
            }
            i.MPRegen += i.MPRegenSpeed;
            while (i.MPRegen >= 1)
            {
                i.MP++;
                i.MPRegen--;
            }
        }*/
        // look for turnee
    }

    Battler LookForNextTurn()
    {
        if (turnee == null)
        {
            bool foundTurnee = false;
            foreach (Battler i in allCharacters.Where(x => x.isAlive))
            {
                if (i.turnDelay <= 0)//Find!
                {
                    //turnee = i;
                    foundTurnee = true;
                    //break;
                    return i;
                }
            }
            if (foundTurnee == false)//Keep looking
            {
                foreach (Battler i in allCharacters.Where(x => x.isAlive))
                {
                    i.turnDelay -= (i.Stats.Speed / 100f);
                }
                roundTime -= 1;
                fifthTime -= 1;
            }
        }
        return null;
    }

    void FullRoundUpdate()
    {
        postActionTrigger = true;
        
    }
    void FifthRoundUpdate()
    {
        foreach (Battler i in allCharacters)
        {
            /*if (i.BurnChill != 0)
            {
                // Chill recovery
                if (i.BurnChill < 0)
                {
                    i.BurnChill += Mathf.Min(1, Mathf.Abs(i.BurnChill));
                }
                else
                {// Burn Damage
                    //float burnThisRound = Mathf.Min(i.BurnChill, Mathf.Max(5, Mathf.Ceil(i.BurnChill * 0.25f)));
                    i.BurnChill -= 1;
                    //i.GetComponent<SpriteAnimator>().Play(i.anims.Hurt);
                    float burnDamage = 2;
                    i.HP -= burnDamage;
                    SpawnDamageText(i.transform.position, burnDamage.ToString(), Color.red);
                }
            }*/
        }
    }
    void PostActionUpdate()
    {
        List<Battler> battlersToDestroy = new List<Battler>();
        foreach (Battler i in allCharacters)
        {
            // KOed
            if (i.HP <= 0)
            {
                BattlerDie(i);
                if (i.persistAfterDeath == false)
                {
                    battlersToDestroy.Add(i);
                    //RemoveBattler(i);
                }
            }
        }
        foreach (Battler i in battlersToDestroy)
        {
            RemoveBattler(i);
        }
        
    }

    void BattlerDie(Battler _subject)
    {
        _subject.HP = 0;
        _subject.isAlive = false;
        _subject.GetComponent<SpriteAnimator>().Play(_subject.anims.Dead);
        _subject.turnDelay = 100;
        //_subject.BurnChill = 0;
        //_subject.aura.Zero();
    }

    // Spawn all Initial Battlers
    private void EnterBattlers()
    {
        //Ally
        for (int i = 0; i < 3; i++)
        {
            Battler newAlly = Instantiate(testBattler);
            newAlly.transform.position = new Vector3(-((1.2f * i) + 1.5f), 0, 0);
            newAlly.isAlly = true;
            newAlly.name = "Ally " + (i + 1).ToString();
            newAlly.rank = Mathf.Min(i, 1);
            //newAlly.profile.level = Random.Range(18, 23);
            allCharacters.Add(newAlly);
            //allCharacters.Add(newAlly);
        }
        
        //Enemy
        for (int i = 0; i < 3; i++)
        {
            Battler newEnemy = Instantiate(testBattler);
            newEnemy.transform.position = new Vector3(((1.2f * i) + 1.5f), 0, 0);
            newEnemy.isAlly = false;
            newEnemy.name = "Enemy " + (i + 1).ToString();
            newEnemy.rank = Mathf.Min(i,1);
            //newEnemy.profile.level = Random.Range(18, 23);
            allCharacters.Add(newEnemy);
            //allCharacters.Add(newEnemy);
        }
        
        foreach (Battler i in allCharacters)
        {
            i.profile.level = Random.Range(45, 50 + 1);
            i.profile.CompileProfile();
            i.turnDelay = Mathf.Floor( 100 - (Random.Range(0f, 50f) * (i.Stats.Speed / 100)) );
            //i.Speed = Random.Range(1f, 2f);
            i.HP = i.Stats.MaxHP;
            i.MP = i.Stats.MaxMP;
        }
    }

    public void RemoveBattler(Battler _toRemove)
    {
        allCharacters.RemoveAt(allCharacters.IndexOf(_toRemove));
        Destroy(_toRemove.gameObject);
    }

    void RestartSelection()
    {
        DestroyTargets();
        PSS = PlayerSelectionStage.SelectAction;
        SetupMenu();
    }
    public void SelectAction(BattleAction _attack)
    {
        selectedAction = _attack;
        SetupTargets();
        PSS = PlayerSelectionStage.SelectTarget;
        DestroyMenu();
    }
    public void RankChange()
    {
        SetupRankTargets();
        DestroyMenu();
        PSS = PlayerSelectionStage.SwapPositions;
    }

    void ChangeRanks()
    {
        if (turnee.rank == 0)
        {
            turnee.rank = 1;
        }
        else
        {
            turnee.rank = 0;
        }
    }

    void SetupMenu()
    {
        ActionSelector newMenu = Instantiate(menuPrefab);
        newMenu.manager = this;
        newMenu.Setup(turnee);
        newMenu.transform.SetParent(mainCanvas.transform);
        newMenu.transform.localScale = new Vector3(1, 1, 1);
        //newMenu.transform.position = Camera.main.WorldToScreenPoint(turnee.transform.position);
        activeMenu = newMenu;

        PSS = PlayerSelectionStage.SelectAction;
    }
    void DestroyMenu()
    {
        Destroy(activeMenu.gameObject);
    }

    void SetupTargets()
    {
        foreach (Battler i in GetValidTargets())
        {
            HudTargetor newTarget = Instantiate(targetorEnemy);
            newTarget.focus = i;
            newTarget.transform.SetParent(mainCanvas.transform);
            activeTargets.Add(newTarget);
        }
    }
    void SetupRankTargets()
    {
        HudTargetor newTarget = Instantiate(targetorEnemy);
        newTarget.focus = turnee;
        newTarget.transform.SetParent(mainCanvas.transform);
        activeTargets.Add(newTarget);
    }
    void DestroyTargets()
    {
        foreach (HudTargetor i in activeTargets)
        {
            Destroy(i.gameObject);
        }
        activeTargets.Clear();
    }

    public void ConfirmAction(bool _isAttack = true)
    {
        List<Battler> targets = new List<Battler>();
        targets.Add(target);
        if (selectedAction.target == AttackTargeting.AllEnemy)
        {
            if (turnee.isAlly)
            {
                targets = enemyCharacters.Where(x => x.isAlive).ToList();
            }
            else
            {
                targets = playerCharacters.Where(x => x.isAlive).ToList();
            }
        }

        CommitAction(targets);

        // Advance the Battle
        turn++;
        //turnee.turnDelay += 100;
        
        DestroyTargets();
        PSS = PlayerSelectionStage.NewTurn;
        postActionTrigger = true;
        turnee.turnDelay += 100;
    }
    void CommitAction(List<Battler> _targets)
    {
        DTOffset = 0;

        //selectedAction.user = turnee;
        selectedAction.CommitAction(turnee, _targets);

        ChangeBattleState(BattleState.Animation);
    }

    public float BattlerDamageOther(Battler _attacker, AttackDamageProperties _skill, Battler _defender)
    {
        float damageDone = 1;
        if (_skill.power > 0)
        {
            //Check Crit & Hit
            if (!CheckHit(_attacker, _skill, _defender))
            {
                SpawnDamageText(_defender.transform.position, "Miss", Color.grey);
                return 0;
            }
            bool crit = CheckCrit(_attacker, _skill, _defender);
            
            //Damage
            damageDone = Mathf.Floor( Calculator.DamageCalc(_attacker, _skill, _defender, crit) );
            if (_defender.defending)
            {
                damageDone = Mathf.Floor(damageDone * 0.25f);
            }
            _defender.GetComponent<SpriteAnimator>().Play(_defender.anims.Hurt, true);
            _defender.HP -= damageDone;
            if (!crit)
            {
                SpawnDamageText(_defender.transform.position, damageDone.ToString(), Color.white);
            }
            else
            {
                SpawnDamageText(_defender.transform.position, "" + (damageDone).ToString(), Color.yellow);
            }
            //_other.aura.Add(_skill.element, damageDone);
        }
        return damageDone;
    }
    public void HurtBattler(float _amount, Battler _target, Element _element = Element.Physical)
    {
        _target.GetComponent<SpriteAnimator>().Play(_target.anims.Hurt);
        _target.HP -= _amount;
        SpawnDamageText(_target.transform.position, _amount.ToString(), Color.white);
        //_target.aura.Add(_element, _amount);
    }
    public float HealBattler(float _amount, Battler _target)
    {
        _target.HP += _amount;
        SpawnDamageText(_target.transform.position, _amount.ToString(), Color.green);
        //_target.aura.Add(_element, _amount);
        return _amount;
    }
    public float RestorMPBattler(float _amount, Battler _target)
    {
        _target.MP += _amount;
        SpawnDamageText(_target.transform.position, _amount.ToString(), Color.blue);
        //_target.aura.Add(_element, _amount);
        return _amount;
    }

    public bool CheckHit(Battler _attacker, AttackDamageProperties _skill, Battler _defender)
    {
        float random = Random.Range(0.0f, 1.0f);
        float chanceToHit = (_attacker.Stats.Accuracy * _skill.accuracyMod) / _defender.Stats.Evade;
        if (chanceToHit < 1)
        {
            if (random > chanceToHit)
            {
                //Miss
                return false;
            }
        }
        return true;
    }
    public bool CheckCrit(Battler _attacker, AttackDamageProperties _skill, Battler _defender)
    {
        float random = Random.Range(0.0f, 1.0f);
        float chanceToCrit = ((_attacker.Stats.Accuracy * _skill.accuracyMod) / _defender.Stats.Evade) - 1;
        if (chanceToCrit > 0)
        {
            if (random < chanceToCrit)
            {
                //Crit
                return true;
            }
        }
        return false;
    }

    List<Battler> GetValidTargets()
    {
        //List<Battler> ret = new List<Battler>();

        switch (selectedAction.target)
        {
            case AttackTargeting.AllEnemy:
            case AttackTargeting.Enemy:

                if (turnee.isAlly)
                {
                    return enemyCharacters.Where(x => x.isAlive).ToList();
                }
                else
                {
                    return playerCharacters.Where(x => x.isAlive).ToList();
                }

                //break;

            case AttackTargeting.Front:

                if (turnee.isAlly)
                {
                    if (GetEnemiesInRank(0).Where(x => x.isAlive).Any())
                    {
                        return GetEnemiesInRank(0).Where(x => x.isAlive).ToList();
                    }
                    else
                    {
                        return GetEnemiesInRank(1).Where(x => x.isAlive).ToList();
                    }
                }
                else
                {
                    if (GetPlayersInRank(0).Where(x => x.isAlive).Any())
                    {
                        return GetPlayersInRank(0).Where(x => x.isAlive).ToList();
                    }
                    else
                    {
                        return GetPlayersInRank(1).Where(x => x.isAlive).ToList();
                    }
                }

                //break;

            case AttackTargeting.AllAlly:
            case AttackTargeting.Ally:

                if (turnee.isAlly)
                {
                    return playerCharacters.Where(x => x.isAlive).ToList();
                }
                else
                {
                    return enemyCharacters.Where(x => x.isAlive).ToList();
                }

            //break;

            case AttackTargeting.Self:
                List<Battler> self = new List<Battler>();
                self.Add(turnee);
                return self;

            case AttackTargeting.Any:
            default:
                return allCharacters;
        }


        //return ret;
    }

    List<string> CalculateTurnOrder(int _time)
    {
        //Debug.Log("CalculateturnOrder");
        List<TurnToken> tokens = new List<TurnToken>();
        foreach (Battler i in allCharacters.Where(x => x.isAlive))
        {
            TurnToken token = new TurnToken();
            token.name = i.name;
            token.turnDelay = i.turnDelay;
            token.speed = i.Stats.Speed;
            tokens.Add(token);
        }

        List<string> final = new List<string>();
        while (final.Count < _time)
        {
            bool hit = false;
            foreach (TurnToken i in tokens)
            {
                if (i.turnDelay <= 0)
                {
                    string name = i.name;
                    //Debug.Log(i.name);
                    final.Add(name);
                    i.turnDelay += 100;
                    hit = true;
                    break;
                }
            }
            if (hit == false)
            {
                foreach (TurnToken i in tokens)
                {
                    i.turnDelay -= (i.speed / 100f);
                }
            }
        }
        //Debug.Log(final);
        return final;
    }

    bool NotAnimating()
    {
        foreach (Battler i in allCharacters)
        {
            if (i.idle == false)
            {
                return false;
            }
        }
        return !FindObjectsOfType<SpecEffect>().Any() && !FindObjectsOfType<BattleEffectsSpawner>().Any();
    }

    public void SpawnDamageText(Vector3 _position, string _string, Color _color)
    {
        DamageText newText = Instantiate(DTprefab);
        newText.show = _string;
        newText.color = _color;

        newText.transform.SetParent(mainCanvas.transform);
        newText.transform.position = Camera.main.WorldToScreenPoint(_position) + new Vector3(0,40 * -(DTOffset - 2),0);
        newText.transform.localScale = new Vector3(1,1,1);

        DTOffset++;
        if (DTOffset >= 4)
        {
            DTOffset -= 4;
        }
    }

    public List<Battler> GetPlayersInRank(int _rank)
    {
        List<Battler> bats = new List<Battler>();
        foreach (Battler i in playerCharacters)
        {
            if (i.rank == _rank)
            {
                bats.Add(i);
            }
        }
        return bats;
    }
    public List<Battler> GetEnemiesInRank(int _rank)
    {
        List<Battler> bats = new List<Battler>();
        foreach (Battler i in enemyCharacters)
        {
            if (i.rank == _rank)
            {
                bats.Add(i);
            }
        }
        return bats;
    }

}

public class TurnToken
{
    public string name;
    public float turnDelay;
    public float speed;
    public float order
    {
        get
        {
            return turnDelay / speed;
        }
    }
}