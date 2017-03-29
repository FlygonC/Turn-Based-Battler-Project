using UnityEngine;
using System.Collections;

public class MouseTests : MonoBehaviour
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 mouse = Input.mousePosition;
        mouse.z = 5;
        Vector3 start = Camera.main.ScreenToWorldPoint(mouse);
        //Vector3 final = start;


        transform.position = start;
	}
}
