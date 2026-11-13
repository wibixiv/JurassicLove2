using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizzManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //retourne au dialogue
        SceneManager.LoadScene("Scene1");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
