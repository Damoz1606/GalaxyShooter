using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadSingleMode()
    {
        SceneManager.LoadScene("SinglePlayer");
    }

    public void LoadCoopMode()
    {
        SceneManager.LoadScene("Co-OpMode");
    }
}
