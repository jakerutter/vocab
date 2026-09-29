using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControls : MonoBehaviour
{
    public void StartGame_OnClick()
    {
        AudioManager.instance.PlayOneShot("PlayGame");
        StartCoroutine(LoadScene("GameScene"));
    }

    IEnumerator LoadScene(string sceneName)
    {
        yield return new WaitForSeconds(1f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

}
