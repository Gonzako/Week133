using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevelOnPlayerDeath : MonoBehaviour
{

    private void OnEnable()
    {
        killPlayerOnTouch.onPlayerKill += restartLevel;
    }

    void restartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    private void OnDisable()
    {
        killPlayerOnTouch.onPlayerKill -= restartLevel;
    }
}
