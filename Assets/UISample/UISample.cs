using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UISample : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _event;

    public void MovePlus()
    {
        transform.DOMove(new Vector3(2, 0, 0), 1.0f)
        .SetLink(gameObject);
    }
    public void MoveMinus()
    {
        transform.DOMove(new Vector3(2, 0, 0), 1.0f)
        .SetLink(gameObject);
    }

    public void PlayEvent() 
    {
        _event?.Invoke();
    }
    public void StopAndPlay(float scale)
    {
        Time.timeScale = scale;
    }
    
}
