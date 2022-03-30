using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashToMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ToMenu());
    }

    IEnumerator ToMenu(){
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(1);
    }
}
