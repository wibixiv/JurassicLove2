using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainmenu;
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject galerie;

    //Jouer !
    public void Launch()
    {
        SceneManager.LoadScene(1);
    }

    //Activer menu Options
    public void Options()
    {
        mainmenu.SetActive(false);
        options.SetActive(true);

    }
    
    //Activer menu Galerie
    public void Galerie()
    {
        mainmenu.SetActive(false);
        galerie.SetActive(true);

    }

    //Retour depuis menu Options ou Galerie
    public void Return()
    {
        options.SetActive(false);
        galerie.SetActive(false);
        mainmenu.SetActive(true);
    }

    //Quitter le jeu
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
