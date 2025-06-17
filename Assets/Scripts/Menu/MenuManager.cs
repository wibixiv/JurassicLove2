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
    [SerializeField] private RectTransform pageGalerie;

    [Header("GameObjects")]
    [SerializeField] private GameObject[] locked;
    [SerializeField] private GameObject[] illustrations;
    [SerializeField] private GameObject boutonRetourIllu;
    [SerializeField] private GameObject boutonPageDroite;
    [SerializeField] private GameObject boutonPageGauche;

    [Header("Animators")]
    [SerializeField] private Animator[] animatorIllu;

    public Animator activeAnimator;

    public PlayerData playerData;
    public DataManager dataManager;

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
        dataManager.SaveGame();
        Application.Quit();
    }

    public void CanCheckIllu()
    {
        for (int i = 0; i < GameManager.Instance.illuUnlocked.Length; i++)
        {
            if (!GameManager.Instance.illuUnlocked[i])
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
        activeAnimator.SetBool("Open", false);
        boutonRetourIllu.SetActive(false);

    }

    public void PageVersDroite()
    {
        pageGalerie.localPosition = new Vector3(-116,0, 0);
        boutonPageDroite.SetActive(false);
        boutonPageGauche.SetActive(true);
    }
    
    public void PageVersGauche()
    {
        pageGalerie.localPosition = new Vector3(614,0, 0);
        boutonPageDroite.SetActive(true);
        boutonPageGauche.SetActive(false);
    }

    public void Start()
    {
        dataManager.LoadGame();
        mainmenu.SetActive(true);
        options.SetActive(false);
        galerie.SetActive(false);
        boutonRetourIllu.SetActive(false);
        boutonPageGauche.SetActive(false);
    }


}
