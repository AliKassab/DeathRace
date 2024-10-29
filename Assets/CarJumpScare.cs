using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CarJumpScare : MonoBehaviour
{
    [SerializeField] List<GameObject> lights;

    private void OnEnable()
    {
        foreach (GameObject go in lights)
        {
            go.SetActive(true);
        }
    }
}
