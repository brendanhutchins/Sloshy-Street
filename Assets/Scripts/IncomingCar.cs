using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomingCar : MonoBehaviour
{
    public float speed = 2.0f;
    private ScrollingBackground background;

    // Start is called before the first frame update
    void Start()
    {
        speed += Random.Range(-1.5f, 2.0f);
        background = GameObject.Find("Streets").GetComponent<ScrollingBackground>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * (speed + background.speed));
    }
}
