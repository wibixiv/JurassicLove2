using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity; 

public class BackToMenu : MonoBehaviour
{
    [YarnCommand("backToMenu")]
    public void GoingBackToMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}
