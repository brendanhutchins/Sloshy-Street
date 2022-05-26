using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardCar : MonoBehaviour
{
    public float speed = 4.0f;
    private ScrollingBackground background;

    // Start is called before the first frame update
    void Start()
    {
        background = GameObject.Find("Streets").GetComponent<ScrollingBackground>();
    }

    // Update is called once per frame
    void Update()
    {
        if (background.speed > speed)
            transform.Translate(Vector3.left * Time.deltaTime * (background.speed - speed));
        else if (background.speed == speed)
            transform.Translate(Vector3.zero);
        else
            transform.Translate(Vector3.right * Time.deltaTime * (speed - background.speed));
    }
}
