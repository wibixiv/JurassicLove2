using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool[] illuUnlocked;


    public void Awake()
    {
        if (Instance)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
    }

}
