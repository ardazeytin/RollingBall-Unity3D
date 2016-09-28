using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HighScoresMenu : MonoBehaviour
{

    void Start()
    {
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            LoadMenu();
        }
    }
}
