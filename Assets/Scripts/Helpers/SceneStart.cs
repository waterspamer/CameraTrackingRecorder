using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1.2f);
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("MainMenuMaterialUI");
    }
}
