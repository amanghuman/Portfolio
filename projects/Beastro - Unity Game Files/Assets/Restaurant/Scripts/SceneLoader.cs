using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Interactive
{
    public string sceneName1, sceneName2;
    PauseGameManager pauser;
    public GameObject sceneMenu;

    void Start(){
        pauser = GameObject.Find("GamePauseManager").GetComponent<PauseGameManager>();
    }

    protected override void OnInteract(){//open up the scene menu, pause
        sceneMenu.SetActive(true);
        pauser.PauseGame();
    }

    public void LoadScene1(){//unpause, load scene
        pauser.UnPauseGame();
        SceneManager.LoadScene(sceneName1);
    }
    public void LoadScecne2(){//unpause, load scene
        pauser.UnPauseGame();
        SceneManager.LoadScene(sceneName2);
    }

    public void CloseMenu(){
        sceneMenu.SetActive(false);
        pauser.UnPauseGame();
    }
}
