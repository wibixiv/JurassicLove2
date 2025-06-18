using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            StartCoroutine(Spying(spyingZones[Random.Range(0, spyingZones.Count)]));
        }
    
        // ReSharper disable Unity.PerformanceAnalysis
        IEnumerator Spying(GameObject spying)
        {
            switch (spying.name)
            {
                case "Mirror":
                    yield return HandleMirrorSpying(spying);
                    break;

                case "Vent":
                    yield return HandleVentSpying(spying);
                    break;

                case "Feet":
                    yield return HandleFeetSpying(spying);
                    break;
            }
            StartCoroutine(WaitTillSpy());
        }

        IEnumerator HandleMirrorSpying(GameObject spying)
        {
            Vector3 originalPos = spying.transform.position;

            spying.transform.DOMove(new Vector3(4.08f, -3.1f, 0), marginTime);
            yield return new WaitForSeconds(marginTime);
            yield return StartCoroutine(WaitAndCheckPhone(Random.Range(minSpyingTime, maxSpyingTime)));
            spying.transform.DOMove(originalPos, 0.5f);
        }

        IEnumerator HandleVentSpying(GameObject spying)
        {
            var spriteRenderer = spying.GetComponent<SpriteRenderer>();

            spying.transform.DOShakePosition(duration: marginTime, strength: 0.2f);
            yield return new WaitForSeconds(marginTime);

            spriteRenderer.color = Color.black;
            yield return StartCoroutine(WaitAndCheckPhone(Random.Range(minSpyingTime, maxSpyingTime)));
            spriteRenderer.color = Color.red;
        }

        IEnumerator HandleFeetSpying(GameObject spying)
        {
            Transform sChild = spying.transform.GetChild(0);
            Vector3 originalFeetPos = spying.transform.position;
            Vector3 originalHeadPos = sChild.position;
            sChild.SetParent(null);
            
            spying.transform.DOMove(new Vector3(originalFeetPos.x, -2.3f, 0), marginTime);
            yield return new WaitForSeconds(marginTime);
            
            sChild.DOMove(new Vector3(originalHeadPos.x, 2.5f, 0), marginTime / 2);
            yield return StartCoroutine(WaitAndCheckPhone(Random.Range(minSpyingTime, maxSpyingTime)));
            
            sChild.DOMove(originalHeadPos, 0.2f);
            spying.transform.DOMove(originalFeetPos, 1f);
            yield return new WaitForSeconds(1f);
            sChild.SetParent(spying.transform);
        }

        IEnumerator WaitAndCheckPhone(float waitTime)
        {
            float timer = 0f;

            while (timer < waitTime)
            {
                if (phoneManager.isOnPhone)
                {
                    Debug.Log("GameOver");
                    yield break;
                }
                timer += Time.deltaTime;
                yield return null;
            }
        }
    }
}
