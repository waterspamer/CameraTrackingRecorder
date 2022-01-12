using UnityEngine;

public class SceneManager : MonoBehaviour
{
    void Awake()=> Application.targetFrameRate = -1;

    public void LoadScene(string name)=>
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    
}
