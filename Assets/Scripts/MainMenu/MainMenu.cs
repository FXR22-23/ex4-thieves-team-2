using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    IEnumerator waiter(bool isClient)
	{
        SceneManager.LoadScene("MainScene", LoadSceneMode.Additive);
        
		yield return new WaitForSeconds(3);

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainScene"));

        SceneManager.UnloadSceneAsync("MainMenu",UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
	}

    public void InitGame(bool param) {
        StartCoroutine(waiter(param));
        
    }

    public void HostBtn() {
        
        InitGame(false);
        
    }

    public void ClientBtn() {
        InitGame(true);
        
    }
}
