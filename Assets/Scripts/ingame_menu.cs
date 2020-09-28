using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ingame_menu : MonoBehaviour
{
    public GameObject pause_menu;
    GameObject player;
    mainScript mainScript;

    public void Start()
    {
        player = GameObject.Find("ball");
        mainScript = player.GetComponent<mainScript>();
    }
    public void pause()
    {
        mainScript.touchEnabled = false;
        Time.timeScale = 0;
        pause_menu.SetActive(true);
    }
    public void resume()
    {
        Time.timeScale = 1;
        pause_menu.SetActive(false);
        StartCoroutine(Delay());
        IEnumerator Delay()
        {
            yield return new WaitForSeconds(.1f);
            mainScript.touchEnabled = true;
        }
    }
    public void restart()
    {
        mainScript.touchEnabled = true;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("mainLevel", LoadSceneMode.Additive);
    }
    public void exit()
    {
        mainScript.touchEnabled = true;
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
