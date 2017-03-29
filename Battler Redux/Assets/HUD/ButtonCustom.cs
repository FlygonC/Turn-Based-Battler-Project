using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class ButtonCustom : MonoBehaviour
{

    private RectTransform trans;
    public Text text;
    protected Image sprite;
    /*private Vector3 center
    {
        get
        {
            return trans.position;
        }
    }*/
    private float width
    {
        get
        {
            return trans.rect.size.x * canvasScale;
        }
    }
    private float height
    {
        get
        {
            return trans.rect.size.y * canvasScale;
        }
    }
    public AABBCollider clickSpace
    {
        get
        {
            AABBCollider ret = new AABBCollider();
            ret.x1 = transform.position.x - width / 2;
            ret.x2 = transform.position.x + width / 2;
            ret.y1 = transform.position.y - height / 2;
            ret.y2 = transform.position.y + height / 2;
            return ret;
        }
    }

    public bool clicked;

    private CanvasScaler scaler;
    private float canvasScale = 1;

	// Use this for initialization
	protected virtual void Start ()
    {
        trans = GetComponent<RectTransform>();
        sprite = GetComponent<Image>();
        scaler = FindObjectOfType<CanvasScaler>();
	}
	
	// Update is called once per frame
	protected virtual void Update ()
    {
        canvasScale = (float)Screen.height / scaler.referenceResolution.y;
        if (clickSpace.Collision(Input.mousePosition))
        {
            transform.localScale = new Vector3(1, 1, 1) * 1.2f;
            if (Input.GetMouseButton(0))
            {
                clicked = true;
            }
            else clicked = false;
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
