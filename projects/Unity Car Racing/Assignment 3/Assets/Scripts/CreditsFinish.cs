using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsFinish : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(CreditsEnd());
    }

    IEnumerator CreditsEnd()
    {
        yield return new WaitForSeconds(10.25f);
        SceneManager.LoadScene(1);
    }
}
