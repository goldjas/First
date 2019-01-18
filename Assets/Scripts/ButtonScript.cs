using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public GameObject escapeMenus;
    public GameObject theCharacter;

    public void Resume()
    {
        Time.timeScale = 1;
        escapeMenus.SetActive(false);
        theCharacter.GetComponent<PlayerController>().isPaus = false;
    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowStatus()
    {

    }
}