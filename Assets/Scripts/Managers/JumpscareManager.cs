using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JumpscareManager : MonoBehaviour
{
    [SerializeField] List<AudioSource> audioSources = new List<AudioSource>();
    [SerializeField] List<TextMeshProUGUI> subtitles = new List<TextMeshProUGUI>();
    [SerializeField] List<GameObject> elements = new List<GameObject>();

    void OnEnable() 
    {
        if (audioSources.Count > 0)
            StartCoroutine(StartAudio());
        if (subtitles.Count > 0)
            StartCoroutine(StartText());
        if (elements.Count > 0)
            StartCoroutine(StartElements());
    }

    IEnumerator StartAudio()
    {
        foreach (var audioSource in audioSources)
            audioSource.Play();
        StopCoroutine(StartAudio());
        yield return null;
    }
    IEnumerator StartText()
    {
        foreach (var subtitles in subtitles)
            subtitles.gameObject.SetActive(true);
        StopCoroutine(StartText());
        yield return null;
    }
    IEnumerator StartElements()
    {
        foreach (var element in elements)
            element.gameObject.SetActive(true);
        StopCoroutine(StartElements());
        yield return null;
    }

}
