using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MiniJeux.Call911
{
    public class PhoneManager : MonoBehaviour
    {
        private float timer;
        public bool isOnPhone = true;
    
        [SerializeField] private float offPosY;
        [SerializeField] private float onPosY;
        [SerializeField] private float moveDuration;

        [SerializeField] private float battery = 100f;
        [SerializeField] private float batteryCost;
        [SerializeField] private GameObject batteryDisplay;
        
        [SerializeField] private List<int> sequence;
        [SerializeField] private List<GameObject> sequenceCase;
        [SerializeField] private int currentSequenceDone;
        
        void Update()
        {
            if (isOnPhone) timer += Time.deltaTime;

            if (timer >= 1f)
            {
                timer -= 1f;
                UseBattery();
            }
        }

        #region On/Off system
        public void PressOnOffButton()
        {
            if (isOnPhone) TurnOff();
            else TurnOn();
        }
    
        void TurnOff()
        {
            isOnPhone = false;
            GetComponent<SpriteRenderer>().color = Color.black;
            transform.DOMove(new Vector3(transform.position.x, offPosY, 0), moveDuration).SetEase(Ease.OutQuint);
        }
    
        void TurnOn()
        {
            isOnPhone = true;
            GetComponent<SpriteRenderer>().color = Color.blue;
            transform.DOMove(new Vector3(transform.position.x, onPosY, 0), moveDuration).SetEase(Ease.OutQuint);
        }
        #endregion
        
        #region Battery system
        // ReSharper disable Unity.PerformanceAnalysis
        private void UseBattery()
        {
            if (battery <= 0f) Debug.Log("Game Over");
            battery -= batteryCost;
            batteryDisplay.GetComponent<TextMeshProUGUI>().text = battery+"%";
        }
        #endregion
        
        #region Call system

        public void CheckAwnser()
        {
            //NotImplementedYet
        }
        #endregion
    }
}
