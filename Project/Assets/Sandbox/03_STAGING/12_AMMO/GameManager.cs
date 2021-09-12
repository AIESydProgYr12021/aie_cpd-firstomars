using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace SandBox.Staging.Ammo
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] float splashScreenLoad = 2.0f;

        //[Header("UI")]
        //[SerializeField] GameObject promptCanvas;
        //public GameObject losePrompt;
        //public GameObject winPrompt;

        private void Start()
        {
            //promptCanvas.SetActive(true);
            //losePrompt.SetActive(false);
            //winPrompt.SetActive(false);
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

        }

        //public void LosePrompt()
        //{
        //    promptCanvas.SetActive(true);
        //    losePrompt.SetActive(true);
        //}

        //public void WinPrompt()
        //{
        //    //promptCanvas.SetActive(true);
        //    winPrompt.SetActive(true);
        //}

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

        public void StagingPlayScene()
        {
            Debug.Log("Staging play scene loaded.");
            SceneManager.LoadScene(2);
        }


        public void GameOverScene()
        {
            Debug.Log("Game over scene loaded.");
            SceneManager.LoadScene(3);
        }

        //public void PlayGameScene()
        //{
        //    Debug.Log("Play game scene loaded.");
        //    SceneManager.LoadScene(3);
        //}

        //public void StagingScene()
        //{
        //    Debug.Log("Staging scene loaded.");
        //    SceneManager.LoadScene(2);
        //}

        //public void CreditsScene()
        //{
        //    Debug.Log("Credits scene loaded.");
        //    SceneManager.LoadScene(5);
        //}

        //public void PlayerEnemyStaging()
        //{
        //    Debug.Log("Player/Enemy Staging scene loaded.");
        //    SceneManager.LoadScene(6);
        //}

        public void Quit()
        {
            Application.Quit();
        }
    }
}