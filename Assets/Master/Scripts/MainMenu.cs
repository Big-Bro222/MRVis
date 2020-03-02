using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToHololens()
    {
        SceneManager.UnloadSceneAsync("MainMenu");
        SceneManager.LoadScene("HololensSetup", LoadSceneMode.Additive);
    }

    public void GoToWall()
    {
        SceneManager.UnloadSceneAsync("MainMenu");
        SceneManager.LoadScene("WallLaunch", LoadSceneMode.Additive);
    }

    public void GoToMonitoring()
    {

    }
}
