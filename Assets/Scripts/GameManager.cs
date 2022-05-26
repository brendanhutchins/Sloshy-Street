using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] ScrollingBackground background;
    [SerializeField] SpawnManager spawnManager;
    [SerializeField] TextMeshProUGUI speedText;
    [SerializeField] TextMeshProUGUI bacText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject gameOverbg;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] Image darkeningPanel;
    [SerializeField] GameObject[] bacScreens;
    [SerializeField] AudioSource bgMusic;
    [SerializeField] AudioSource bottleSound;
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject nonPixelCamera;
    private int score;
    private int speedMPH;
    private int bottles;

    private void Start()
    {
        speedMPH = 0;
        darkeningPanel.color = new Color(0, 0, 0, 0);
        bacScreens[7].SetActive(true);
    }

    public void UpdateBAC()
    {
        if (bottles < 8)
        {
            bottles++;
            // Update BAC text
            switch (bottles)
            {
                case 1:
                    bacText.text = "BAC: .02";
                    bacScreens[7].SetActive(false);
                    bacScreens[0].SetActive(true);
                    bgMusic.Play();
                    spawnManager.SpawnBeer();
                    // 70 alpha
                    StartCoroutine(FadeTo(new Color(0, 0, 0, 0.275f)));
                    break;
                case 2:
                    bacText.color = Color.yellow;
                    bacText.text = "BAC: .05";
                    bacScreens[0].SetActive(false);
                    bacScreens[1].SetActive(true);
                    // Reduced response in driving
                    player.speed -= 2f;
                    break;
                case 3:
                    bacText.text = "BAC: .07";
                    bacScreens[0].SetActive(false);
                    bacScreens[1].SetActive(true);
                    break;
                case 4:
                    bacText.color = Color.red;
                    bacText.text = "BAC: .09";
                    bacScreens[1].SetActive(false);
                    bacScreens[2].SetActive(true);
                    player.speed -= 2f;
                    // Darken the screen a bit; 115f alpha
                    StartCoroutine(FadeTo(new Color(0, 0, 0, 0.45f)));
                    break;
                case 5:
                    bacText.text = "BAC: .12";
                    bacScreens[2].SetActive(false);
                    bacScreens[3].SetActive(true);
                    // Ice physics/random inputs activated
                    player.swayDelay = 0.7f;
                    player.DrunkCar();
                    break;
                case 6:
                    bacText.text = "BAC: .14";
                    bacScreens[3].SetActive(false);
                    bacScreens[4].SetActive(true);
                    break;
                case 7:
                    bacText.text = "BAC: .16";
                    bacScreens[4].SetActive(false);
                    bacScreens[5].SetActive(true);
                    player.speed -= 1f;
                    // Black in and out
                    InvokeRepeating("StartFade", 0.0f, 10.0f);
                    break;
                case 8:
                    bacText.text = "BAC: .19";
                    bacScreens[5].SetActive(false);
                    bacScreens[6].SetActive(true);
                    player.swayDelay = 0.5f;
                    break;
            }
            background.speed += 3.75f;
            speedMPH += 20;
            speedText.text = speedMPH + " MPH";
        }
    }

    public void Crash()
    {
        spawnManager.StopAllCoroutines();
        spawnManager.CancelInvoke();
        StopAllCoroutines();
        background.speed = 0;
        player.isCrashed = true;
        player.StopAllCoroutines();
        GameObject[] otherCars = GameObject.FindGameObjectsWithTag("Other Car");
        bgMusic.Stop();
        bottleSound.Stop();
        mainCamera.SetActive(false);
        nonPixelCamera.SetActive(true);
        foreach (GameObject car in otherCars)
        {
            if (car.TryGetComponent(out ForwardCar forwardCar))
            {
                forwardCar.speed = 0;
            }
            else if (car.TryGetComponent(out IncomingCar incomingCar))
            {
                incomingCar.speed = 0;
            }
        }
        StartCoroutine(StopGame());
    }

    public void StartFade()
    {
        StartCoroutine(Fade());
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator FadeTo(Color newColor)
    {
        Color currentColor = darkeningPanel.color;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 2.5f)
        {
            Color newColor2 = Color.Lerp(currentColor, newColor, t);
            darkeningPanel.color = newColor2;
            yield return null;
        }
    }

    IEnumerator StopGame()
    {
        yield return new WaitForSeconds(2.0f);
        gameOverbg.SetActive(true);
        speedText.gameObject.SetActive(false);
        yield return new WaitForSeconds(2.0f);
        
        gameOverScreen.SetActive(true);
    }

    IEnumerator Fade()
    {
        // 210 alpha
        Color col = new Color(0, 0, 0, 0.9f);
        Color currentColor = darkeningPanel.color;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 3.0f)
        {
            Color newColor = Color.Lerp(currentColor, col, t);
            darkeningPanel.color = newColor;
            yield return null;
        }
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 1.0f)
        {
            Color newColor2 = Color.Lerp(col, currentColor, t);
            darkeningPanel.color = newColor2;
            yield return null;
        }
    }
}
