using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour
{
    [SerializeField] private GameObject[] protions;
    [SerializeField] private int index = 0;
    public bool IsFinished => index == protions.Length;
    
    [SerializeField] private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        SetVisuals();
    }

    private void OnValidate()
    {
        SetVisuals();
    }

    [ContextMenu("Consume")]
    
    public void Consume()
    {
        if (!IsFinished)
        {
            index++;
            SetVisuals();
            _audioSource.Play();
        }
    }
    
    void SetVisuals()
    {
        for (int i = 0; i < protions.Length; i++)
        {
            protions[i].SetActive(i== index);
        }
    }
}
