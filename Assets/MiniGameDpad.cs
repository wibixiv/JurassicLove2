using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MiniGameDpad : MonoBehaviour
{
    private enum Direction
    {
        up,
        down,
        left,
        right,
        none
    }

    [SerializeField] private Image image;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private UnityEvent upEvent;
    [SerializeField] private UnityEvent downEvent;
    [SerializeField] private UnityEvent leftEvent;
    [SerializeField] private UnityEvent rightEvent;

    private UnityEvent[] directionToEvent;

    private void Awake()
    {
        directionToEvent = new[]
        {
            upEvent,
            downEvent,
            leftEvent,
            rightEvent
        };
    }

    public void SwitchToDirection(int direction)
    {
        if (direction != (int)Direction.none)
        {
            directionToEvent[direction].Invoke();
        }
        
        image.sprite = sprites[direction];
    }
}