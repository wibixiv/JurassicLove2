using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class MiniGameLauncher : MonoBehaviour
{
    [YarnCommand("LaunchMinigame")]
    public void LauchMinigame(string name)
    {
        SceneManager.LoadScene(name); 
    }
}
