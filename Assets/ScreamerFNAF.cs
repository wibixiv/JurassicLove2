using System.Collections;
using UnityEngine;

public class ScreamerFNAF : MonoBehaviour
{
    public Animation anim;
    void Start()
    {
        StartCoroutine(screamerLauchCo());
    }
    private IEnumerator screamerLauchCo()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5f,15f));
            anim.Play();
        }
    }
    public void scream(float time)
    {
        StartCoroutine(screamerCoroutine(time));
    }

    private IEnumerator screamerCoroutine(float time)
    {
        FNAFGameManager.FNAFManager.screamerOn = true;
        yield return new WaitForSeconds(time);
        FNAFGameManager.FNAFManager.screamerOn = false;
    }
}
