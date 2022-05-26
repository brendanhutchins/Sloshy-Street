using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerBottle : MonoBehaviour
{
    private AudioSource beerAudio;
    private GameManager gameManager;

    private ScrollingBackground background;
    private void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        beerAudio = GameObject.Find("DrinkBottle").GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        background = GameObject.Find("Streets").GetComponent<ScrollingBackground>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * background.speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            beerAudio.Play();
            gameManager.UpdateBAC();
            Destroy(gameObject);
        }
    }
}
