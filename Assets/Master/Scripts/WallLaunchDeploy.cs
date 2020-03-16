using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WallLaunchDeploy : MonoBehaviour
{
    string password;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangePassword(string password)
    {
        this.password = password;
    }

    public void DeployOnWall()
    {
        MasterSetup.Instance.DeployWall(password);
    }

    public void LaunchOnWall()
    {        
        MasterSetup.Instance.LaunchWall(password);
        StartCoroutine(LoadYourAsyncScene());
    }

    IEnumerator LoadYourAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("BaseSetupMaster", LoadSceneMode.Additive);        

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("BaseSetupMaster"));
    }

    public void GoToSetup()
    {
        SceneManager.UnloadSceneAsync("WallLaunch");
        SceneManager.LoadScene("WallSetup", LoadSceneMode.Additive);
    }

    public void GoBack()
    {
        SceneManager.UnloadSceneAsync("WallLaunch");
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
    }
}
