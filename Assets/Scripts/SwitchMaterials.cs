using System.Collections.Generic;
using UnityEngine;

public class SwitchMaterials : MonoBehaviour
{
    public enum MaterialType
    {
        Default,
        Red,
        Blue,
        Green,
        Yellow
    };

    public MaterialType Material;

    [SerializeField]
    private SwitchModels _switchModels;

    private List<Material> _defaultMaterials;

    [SerializeField]
    private Material[] _otherMaterials;

    private MaterialType _lastMaterial;

    void Awake()
    {
        _defaultMaterials = new List<Material>();

        // get default materials
        for (int i = 0; i < _switchModels._models.Length; i++)
        {
            Renderer[] rndrs = _switchModels._models[i].GetComponentsInChildren<Renderer>();

            foreach (var rndr in rndrs)
            {
                foreach (var mat in rndr.materials)
                {
                    _defaultMaterials.Add(mat);
                }
            }
        }

        _lastMaterial = Material;
    }

    // Update is called once per frame
    void Update()
    {
        if (_lastMaterial != Material)
        {
            UpdateMaterials();
            _lastMaterial = Material;
        }
    }

    public void UpdateMaterials()
    {
        int counter = 0;

        for (int i = 0; i < _switchModels._models.Length; i++)
        {
            Renderer[] rndrs = _switchModels._models[i].GetComponentsInChildren<Renderer>(true);

            foreach (var rndr in rndrs)
            {
                List<Material> mats = new List<Material>();

                for (int j = 0; j < rndr.materials.Length; j++)
                {
                    switch (Material)
                    {
                        case MaterialType.Default:
                            mats.Add(_defaultMaterials[counter]);
                            break;
                        case MaterialType.Red:
                            mats.Add(_otherMaterials[0]);
                            break;
                        case MaterialType.Blue:
                            mats.Add(_otherMaterials[1]);
                            break;
                        case MaterialType.Green:
                            mats.Add(_otherMaterials[2]);
                            break;
                        case MaterialType.Yellow:
                            mats.Add(_otherMaterials[3]);
                            break;
                        default:
                            mats.Add(_otherMaterials[0]);
                            break;
                    }

                    counter++;
                }

                rndr.SetMaterials(mats);
            }
        }
    }
}
