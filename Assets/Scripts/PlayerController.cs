using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public float swayDelay;
    public bool isCrashed;

    [SerializeField] float xRange = 8.0f;
    [SerializeField] float yRange = 5.0f;

    private PlayerControls playerControls;
    private GameManager gameManager;

    private void Awake()
    {
        playerControls = new PlayerControls();
        
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        isCrashed = false;
    }

    private void Update()
    {
        // Keep Player in bounds
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector2(-xRange, transform.position.y);
        }
        if (transform.position.x > xRange)
        {
            transform.position = new Vector2(xRange, transform.position.y);
        }
        if (transform.position.y < -yRange)
        {
            transform.position = new Vector2(transform.position.x, -yRange);
        }
        if (transform.position.y > yRange)
        {
            transform.position = new Vector2(transform.position.x, yRange);
        }

        Vector2 move = playerControls.Default.Move.ReadValue<Vector2>();

        if (!isCrashed)
        {
            transform.Translate(Vector3.right * move.x * Time.deltaTime * speed);
            transform.Translate(Vector3.up * move.y * Time.deltaTime * speed);
        }
    }

    public void DrunkCar()
    {
        StartCoroutine(SwayCar());
    }

    IEnumerator SwayCar()
    {
        yield return new WaitForSeconds(swayDelay);
        int randomNum = Random.Range(0, 2);

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 0.5f)
        {
            if (randomNum == 0)
            {
                transform.Translate(Vector3.up * Time.deltaTime * 3);
            }
            else
            {
                transform.Translate(Vector3.down * Time.deltaTime * 3);
            }
            yield return null;
        }
        StartCoroutine(SwayCar());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Other Car")
        {
            gameManager.Crash();
        }
    }
}
