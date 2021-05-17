using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Planet : MonoBehaviour
{
    public float SizeKM;
    public string Name;

    private TextMeshPro _label;

    private void Start()
    {
        _label = GetComponentInChildren<TextMeshPro>();
        if (_label != null)
        {
            _label.text = string.Format("{0}\n{1} KM", Name, SizeKM);
        }
    }
}
