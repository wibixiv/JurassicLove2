using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainmenu;
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject galerie;

    public void Launch()
    {
        SceneManager.LoadScene(1);
    }

    public void Options()
    {
        mainmenu.SetActive(false);
        options.SetActive(true);

    }
    
    public void Galerie()
    {
        mainmenu.SetActive(false);
        galerie.SetActive(true);

    }

    public void Return()
    {
        options.SetActive(false);
        galerie.SetActive(false);
        mainmenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Start()
    {
        mainmenu.SetActive(true);
        options.SetActive(false);
        galerie.SetActive(false);
    }


}
