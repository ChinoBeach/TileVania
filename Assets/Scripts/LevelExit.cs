using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float fltLevelLoadDelay = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if( other.tag == "Player")
        {
            StartCoroutine(LoadNextLevel());
        }
        

    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(fltLevelLoadDelay);
        int intCurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int intNextSceneIndext = intCurrentSceneIndex + 1;

        if (intNextSceneIndext == SceneManager.sceneCountInBuildSettings)
        {
            intNextSceneIndext = 0;
        }
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(intNextSceneIndext);
    }
}
