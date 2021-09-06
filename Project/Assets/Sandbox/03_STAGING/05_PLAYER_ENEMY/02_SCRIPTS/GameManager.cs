using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace SandBox.Staging.PlayerEnemy
{
    public class GameManager : MonoBehaviour
    {
        //[SerializeField] GameObject promptMenu;

        [SerializeField] float splashScreenLoad = 2.0f;
        [SerializeField] Text playerHealth;
        [SerializeField] Text playerScore;


        private void Start()
        {

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
                //promptMenu.SetActive(true);
            }

            //if( Input.GetKeyDown("M") &&
            //    SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(0) &&
            //    SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(1))
            //{
            //    SceneManager.LoadScene(1);
            //    //promptMenu.SetActive(true);
            //}
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
