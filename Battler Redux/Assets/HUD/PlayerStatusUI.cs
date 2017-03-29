using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStatusUI : MonoBehaviour
{

    [SerializeField]
    Battler focus;

    [SerializeField]
    private Image icon;

    [SerializeField]
    private Image HPBar;
    [SerializeField]
    private Text HPText;

    [SerializeField]
    private Image MPBar;
    [SerializeField]
    private Text MPText;

    private Vector3 startPosition;

    public void UpdateStatus(Battler _battler)
    {
        HPBar.fillAmount = _battler.HP / _battler.Stats.MaxHP;
        HPText.text = _battler.HP.ToString();
    }

	// Use this for initialization
	void Start ()
    {
        startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        icon.sprite = focus.profile.identity.icon;

        HPBar.fillAmount = focus.HP / focus.Stats.MaxHP;
        HPText.text = focus.HP.ToString() + " /" + focus.Stats.MaxHP;

        MPBar.fillAmount = focus.MP / focus.Stats.MaxMP;
        MPText.text = focus.MP.ToString() + " /" + focus.Stats.MaxMP;

        /*if (focus.rank == 0)
        {
            transform.position = startPosition + new Vector3(100, 0, 0);
        }
        else
        {
            transform.position = startPosition;
        }*/
    }

    public void SetFocus(Battler _battler)
    {
        focus = _battler;
    }

}
