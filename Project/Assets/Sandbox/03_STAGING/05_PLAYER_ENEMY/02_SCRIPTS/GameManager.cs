using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace SandBox.Staging.PlayerEnemy
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] float splashScreenLoad = 2.0f;

        [Header("UI")]
        [SerializeField] Text waveNum;
        [SerializeField] GameObject promptCanvas;
        [SerializeField] GameObject losePrompt;
        [SerializeField] GameObject winPrompt;

        [Header("EnemyWaves")]
        [SerializeField] int numOfWaves;
        [SerializeField] int numOfEnemies;



        private void Start()
        {
            UpdateWaveNumber();
            promptCanvas.SetActive(false);
            losePrompt.SetActive(false);
            winPrompt.SetActive(false);

            numOfWaves = 1;
            numOfEnemies = 1;
        }

        private void Update()
        {
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
            {
                splashScreenLoad -= Time.deltaTime;

                if (splashScreenLoad <= 0)
                {
                    SceneManager.LoadScene(1);
                }
            }

            if (Input.GetKeyDown("m"))
            {
                SceneManager.LoadScene(1);
            }

            if(numOfWaves <= 0)
            {
                promptCanvas.SetActive(true);
                winPrompt.SetActive(true);
            }

        }

        public void EnemyKilled()
        {
            numOfEnemies--;
            if(numOfEnemies <= 0)
            {
                numOfWaves--;
            }
        }

        private void UpdateWaveNumber()
        {
            waveNum.text = "Wave 1";
        }

        public void LosePrompt()
        {
            promptCanvas.SetActive(true);
            losePrompt.SetActive(true);
        }

        public void SplashScene()
        {
            Debug.Log("Splash scene loaded.");
            SceneManager.LoadScene(0);
        }

        public void MenuScene()
        {
            Debug.Log("Menu scene loaded.");
            SceneManager.LoadScene(1);
        }

        public void StagingScene()
        {
            Debug.Log("Staging scene loaded.");
            SceneManager.LoadScene(2);
        }

        public void PlayGameScene()
        {
            Debug.Log("Play game scene loaded.");
            SceneManager.LoadScene(3);
        }

        public void GameOverScene()
        {
            Debug.Log("Game over scene loaded.");
            SceneManager.LoadScene(4);
        }

        public void CreditsScene()
        {
            Debug.Log("Credits scene loaded.");
            SceneManager.LoadScene(5);
        }

        public void PlayerEnemyStaging()
        {
            Debug.Log("Player/Enemy Staging scene loaded.");
            SceneManager.LoadScene(6);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }

}
