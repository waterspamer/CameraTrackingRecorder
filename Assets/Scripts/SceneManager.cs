using UnityEngine;

public class SceneManager : MonoBehaviour
{
    void Awake()=> Application.targetFrameRate = 120;

    public void LoadScene(string name)=>
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    
}
