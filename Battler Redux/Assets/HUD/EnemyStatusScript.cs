using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyStatusScript : MonoBehaviour
{

    [SerializeField]
    Battler focus;

    [SerializeField]
    private Image icon;

    [SerializeField]
    private Image HPBar;

    private Vector3 startPosition;

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

        /*if (focus.rank == 0)
        {
            transform.position = startPosition + new Vector3(-100, 0, 0);
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
