using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum MeshType
{
    Sphere,
    Plane,
    Box,
};

[System.Serializable]
public class PlanetInfo
{
    public GameObject Prefab;
    public string Name;
    public float SizeKM;
}

public class World : MonoBehaviour
{
    private Planet[] _planets;

    [SerializeField] private GameObject _prefabSphere;
    [SerializeField] private GameObject _prefabBox;
    [SerializeField] private GameObject _prefabPlane;
    [SerializeField] private GameObject _prefabLabel;

    [SerializeField] private Vector3 _direction;
    [SerializeField] private float _speed = 1.0f;
    [SerializeField] private float _speedMult = 1.0001f;
    [SerializeField] private float _scaleCurrent = 1.0f;

    [SerializeField] private PlanetInfo[] _infoList;

//    private void Start()
//    {
//        _planets = GetComponentsInChildren<Planet>();
//    }

    private void Update()
    {
        transform.position = transform.position + (_direction * _speed * Time.deltaTime);
        _speed *= _speedMult;
    }

    [ContextMenu("Generate")]
    public void Generate()
    {
        foreach (Transform t in transform)
            DestroyImmediate(t.gameObject);

        System.Array.Sort(_infoList, (a,b) => 
            {
                if (a.SizeKM < b.SizeKM) return -1;
                else if (a.SizeKM > b.SizeKM) return 1;
                
                return 0;
            });

        float xNext = 0.0f;

        foreach (PlanetInfo info in _infoList)
        {
            GameObject goParent = new GameObject(info.Name);
            GameObject goMesh = Instantiate(info.Prefab != null ? info.Prefab : _prefabSphere);
            GameObject goLabel = Instantiate(_prefabLabel);

            goParent.transform.SetParent(transform);
            goMesh.transform.SetParent(goParent.transform);
            goLabel.transform.SetParent(goParent.transform);

            float scale = info.SizeKM * 0.0001f;
            xNext += scale * 3.0f;

            goParent.transform.localScale = new Vector3(scale, scale, scale);
            goParent.transform.localPosition = new Vector3(xNext, 0.0f, 0.0f);


            goMesh.transform.localPosition = new Vector3(0.0f, 0.5f, 0.0f);

            TextMeshPro label = goLabel.GetComponent<TextMeshPro>();
            label.text = string.Format("{0}\n{1} KM", info.Name, info.SizeKM);
            goLabel.transform.localPosition = new Vector3(0.0f, 1.2f, 0.0f);

            goMesh.SetActive(true);
            goLabel.SetActive(true);
        }
    }

    static public void SetParentAndResetTransform(Transform parent, Transform child)
    {
        child.SetParent(parent);
        child.localPosition = Vector3.zero;
        child.localScale = Vector3.one;
        child.localRotation = Quaternion.identity;
    }
}
