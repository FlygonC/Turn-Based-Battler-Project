using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{

    //private static int offsetBuffer = 0;

    public string show;

    public Vector3 velocity = new Vector3(0.2f, 1, 0);
    public float speed = 0.1f;
    public float dampen = 0.9f;

    public Vector3 worldPos = new Vector3();

    private float life = 0;

    public Text text;
    public Color color;

	// Use this for initialization
	void Start ()
    {
        text = GetComponent<Text>();
        text.text = show;
        text.color = color;
        //transform.position += new Vector3(0, 46 * offsetBuffer, 0);
        worldPos = Camera.main.ScreenToWorldPoint(transform.position);
	}
	
	// Update is called once per frame
	void Update ()
    {
        worldPos += velocity * speed;
        speed = Mathf.Max(0.005f, speed * dampen);

        transform.position = Camera.main.WorldToScreenPoint(worldPos);

        life += Time.deltaTime;
        if (life >= 1.5f)
        {
            Destroy(gameObject);
        }
	}
}
