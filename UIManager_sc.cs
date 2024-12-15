using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIManager_sc : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI scoreTMP;

    [SerializeField]
    Sprite[] livesSprites;

    [SerializeField]
    UnityEngine.UI.Image liveImg;

    [SerializeField]
    TextMeshProUGUI gameOverTMP;
    
    [SerializeField]
    TextMeshProUGUI restartTMP;

    GameManager_sc gamemanager_sc;
    
    void Start()
    {
        scoreTMP.text = "Score: "+0;
        liveImg.sprite = livesSprites[3];

        gameOverTMP.gameObject.SetActive(false);
        restartTMP.gameObject.SetActive(false);

        gamemanager_sc = GameObject.Find("GameManager").GetComponent<GameManager_sc>();
    }

    public void UpdateScoreTMP(int score)
    {
        scoreTMP.text = "Score: "+score;
    }

    public void UpdateLivesImg(int lives)
    {
        liveImg.sprite = livesSprites[lives];

        if(lives == 0)
        {
            if(gamemanager_sc != null)
            {
                gamemanager_sc.GameOver();
            }

            GameOverSequence();
        }
    }

    void GameOverSequence()
    {
        
        gameOverTMP.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());

        restartTMP.gameObject.SetActive(true);
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while(true)
        {
            gameOverTMP.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            gameOverTMP.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
