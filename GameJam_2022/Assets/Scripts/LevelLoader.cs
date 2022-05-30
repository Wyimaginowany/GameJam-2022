using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float transitionTime = 1f;

    public void LoadNextLevel()
    {
        //Debug.Log("SCENE INDEX: " + SceneManager.GetActiveScene().buildIndex + "SCENE LENGTH: " + SceneManager.sceneCountInBuildSettings);
        if (SceneManager.GetActiveScene().buildIndex >= SceneManager.sceneCountInBuildSettings - 1)
        {

            StartCoroutine(LoadLevel(0));
        }
        else
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        }
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        animator.SetTrigger("start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
