using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiyngZonesManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> spiyngZones;

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
        foreach (GameObject spying in spiyngZones) spying.SetActive(false);
        yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
        
        spiyngZones[Random.Range(0, spiyngZones.Count)].SetActive(true);
        StartCoroutine(Spying());
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator Spying()
    {
        yield return new WaitForSeconds(marginTime);
        
        if (phoneManager.isOnPhone) Debug.Log("GameOver");
        yield return new WaitForSeconds(Random.Range(minSpyingTime, maxSpyingTime));
        
        StartCoroutine(WaitTillSpy());
    }
}
