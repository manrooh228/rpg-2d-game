using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Services
{
    public class HandlePauseMenu : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private GameObject pauseUi;

        private bool isPaused = false;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }

        private void Pause()
        {
            pauseUi.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void Resume()
        {
            pauseUi.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }
    }
}