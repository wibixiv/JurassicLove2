using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

namespace MiniJeux.Call911
{
    public class SpyingZonesManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> spyingZones;

        [SerializeField] private float minWaitTime;
        [SerializeField] private float maxWaitTime;
        [SerializeField] private float minSpyingTime;
        [SerializeField] private float maxSpyingTime;
        [SerializeField] private float marginTime;
        [SerializeField] private PhoneManager phoneManager;

        void Start()
        {
            StartCoroutine(WaitTillSpy());
        }
    
        // ReSharper disable Unity.PerformanceAnalysis
        IEnumerator WaitTillSpy()
        {
            foreach (GameObject spying in spyingZones) spying.SetActive(false);
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            
            StartCoroutine(Spying(spyingZones[Random.Range(0, spyingZones.Count)]));
        }
    
        // ReSharper disable Unity.PerformanceAnalysis
        IEnumerator Spying(GameObject spying)
        {
            spying.SetActive(true);

            switch (spying.name)
            {
                case "Mirror":
                    Vector3 originalPos = spying.transform.position;
                    spying.transform.DOMove(new Vector3(4.08f, -3.1f, 0), marginTime);
                    yield return new WaitForSeconds(marginTime);
                    if (phoneManager.isOnPhone) Debug.Log("GameOver");
                    yield return new WaitForSeconds(Random.Range(minSpyingTime, maxSpyingTime));
                    spying.transform.DOMove(originalPos, 0.5f);
                    break;

                case "Vent":
                    spying.transform.DOShakePosition(marginTime);
                    yield return new WaitForSeconds(marginTime);
                    if (phoneManager.isOnPhone)
                        Debug.Log("GameOver");
                    yield return new WaitForSeconds(Random.Range(minSpyingTime, maxSpyingTime));
                    break;

                case "Head":
                    Transform sChild = spying.transform.GetChild(0);
                    spying.SetActive(false);
                    sChild.gameObject.SetActive(true);
                    yield return new WaitForSeconds(marginTime);
                    spying.SetActive(true);
                    if (phoneManager.isOnPhone) Debug.Log("GameOver");
                    yield return new WaitForSeconds(Random.Range(minSpyingTime, maxSpyingTime));
                    break;
            }
            StartCoroutine(WaitTillSpy());
        }
    }
}
