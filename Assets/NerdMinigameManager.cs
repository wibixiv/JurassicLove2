using System.Drawing;
using TMPro;
using UnityEngine;

public class NerdMinigameManager : MonoBehaviour
{
    public static NerdMinigameManager nerdManager;
    int point;
    float timeLeft;
    public TextMeshProUGUI pointCounter, timeCounter;

    void Start() 
    {
        timeLeft = 30f;
        if(nerdManager !=null) Destroy(gameObject);
        nerdManager = this;
    }

    public static void NerdPointChange(int value)
    {
        nerdManager.point += value;
    }

    void Update()
    {
        timeLeft-= Time.deltaTime;
        pointCounter.text = $"{point}/20"; 
        timeCounter.text = Mathf.CeilToInt(timeLeft).ToString();

        if(timeLeft<0f)
        {
            End(false);
        }
        else if(point>=20)
        {
            End(true);
        }
    }

    void End(bool won)
    {
        if(won)
        {
            
        }
        else
        {

        }
    }
}
