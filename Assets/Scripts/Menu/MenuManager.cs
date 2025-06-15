using System.Linq;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Onglets")]
    [SerializeField] private GameObject mainmenu;
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject galerie;

    [Header("GameObjects")]
    [SerializeField] private GameObject[] locked;
    [SerializeField] private GameObject[] illustrations; 

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
        CanCheckIllu();
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

    public void CanCheckIllu()
    {
        for (int i = 0; i < GameManager.Instance.illustrationsFins.Length; i++)
        {
            if (!GameManager.Instance.illustrationsFins[i])
            {
                locked[i].SetActive(true);
                illustrations[i].SetActive(false);
            }
            else
            {
                illustrations[i].SetActive(true);
                locked[i].SetActive(false);
            }
        }
    }

    public void RetourIllustration()
    {

    }

    public void CloseUpIllu()
    {

    }

    public void Start()
    {
        mainmenu.SetActive(true);
        options.SetActive(false);
        galerie.SetActive(false);
    }


}
