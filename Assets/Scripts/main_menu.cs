using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class main_menu : MonoBehaviour
{
    private int selectedLevel=1;
    public Text levelText;
    public GameObject leftArrowButton;
    public GameObject rightArrowButton;
    public AudioSource bg_music;

    private void Start()
    {
        bg_music = GameObject.Find("bg_music").GetComponent<AudioSource>();
    }
    private void Update()
    {
        //disable left arrow
        if (selectedLevel <= 1)
        {
            leftArrowButton.SetActive(false);
        }
        else
        {
            leftArrowButton.SetActive(true);
        }
        //disable right arrow
        if (selectedLevel >= 10)
        {
            rightArrowButton.SetActive(false);
        }
        else
        {
            rightArrowButton.SetActive(true);
        }
        levelText.text = "Level " + selectedLevel;
        if (bg_music.isPlaying == false)
        {
            bg_music.Play();
        }
        DontDestroyOnLoad(bg_music);
    }
    public void play()
    {
        SceneManager.LoadScene("level" + selectedLevel);
        SceneManager.LoadScene("mainLevel", LoadSceneMode.Additive);
    }
    public void leftArrow()
    {
        selectedLevel -= 1;
    }
    public void rightArrow()
    {
        selectedLevel += 1;
    }
}
