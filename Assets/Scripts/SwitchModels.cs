using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchModels : MonoBehaviour
{
    #region Structs
    [Serializable]
    public struct JapaneseStruct
    {
        public GameObject[] models;
        public bool FourSmallWindows;
        public bool NarrowWindows;
        public bool BigWindows;
        public bool SmallWindows;
        public bool Block;

        public bool[] GetBools()
        {
            return new bool[] { FourSmallWindows, NarrowWindows, BigWindows, SmallWindows, Block };
        }

        public void UpdateBool(int index, bool value)
        {
            switch (index)
            {
                case 0:
                    FourSmallWindows = value;
                    break;
                case 1:
                    NarrowWindows = value;
                    break;
                case 2:
                    BigWindows = value;
                    break;
                case 3:
                    SmallWindows = value;
                    break;
                case 4:
                    Block = value;
                    break;
            }
        }
    }

    [Serializable]
    public struct KurokowaStruct
    {
        public GameObject[] models;
        public bool OneWindow;
        public bool TwoWindows;
        public bool FourWindows;

        public bool[] GetBools()
        {
            return new bool[] { OneWindow, TwoWindows, FourWindows };
        }

        public void UpdateBool(int index, bool value)
        {
            switch (index)
            {
                case 0:
                    OneWindow = value;
                    break;
                case 1:
                    TwoWindows = value;
                    break;
                case 2:
                    FourWindows = value;
                    break;
            }
        }
    }

    [Serializable]
    public struct BrutalistStruct
    {
        public GameObject[] models;
        public bool OneWindow;
        public bool TwoWindows;
        public bool Tunnel;
        public bool TunnelJunction;
        public bool Block;

        public bool[] GetBools()
        {
            return new bool[] { OneWindow, TwoWindows, Tunnel, TunnelJunction, Block };
        }

        public void UpdateBool(int index, bool value)
        {
            switch (index)
            {
                case 0:
                    OneWindow = value;
                    break;
                case 1:
                    TwoWindows = value;
                    break;
                case 2:
                    Tunnel = value;
                    break;
                case 3:
                    TunnelJunction = value;
                    break;
                case 4:
                    Block = value;
                    break;
            }
        }
    }
    #endregion

    public JapaneseStruct JapaneseModels;
    public KurokowaStruct KurokowaModels;
    public BrutalistStruct BrutalistModels;

    private bool[] _bools,
        _previousBools;
    private GameObject[] _models;

    // Start is called before the first frame update
    void Start()
    {
        _bools = new bool[
            JapaneseModels.GetBools().Length
                + KurokowaModels.GetBools().Length
                + BrutalistModels.GetBools().Length
        ];
        GetBools();

        _previousBools = new bool[_bools.Length];
        _bools.CopyTo(_previousBools, 0);

        _models = new GameObject[_bools.Length];
        JapaneseModels.models.CopyTo(_models, 0);
        KurokowaModels.models.CopyTo(_models, JapaneseModels.models.Length);
        BrutalistModels.models.CopyTo(
            _models,
            JapaneseModels.models.Length + KurokowaModels.models.Length
        );
    }

    void Update()
    {
        _bools.CopyTo(_previousBools, 0);
        GetBools();

        for (int i = 0; i < _bools.Length; i++)
        {
            if (_bools[i] != _previousBools[i])
            {
                _previousBools[i] = _bools[i];
                ChangeModel(i);
                UpdateBools();
                break;
            }
        }
    }

    private void GetBools()
    {
        var japBools = JapaneseModels.GetBools();
        var kuroBools = KurokowaModels.GetBools();
        var brutBools = BrutalistModels.GetBools();

        japBools.CopyTo(_bools, 0);
        kuroBools.CopyTo(_bools, japBools.Length);
        brutBools.CopyTo(_bools, japBools.Length + kuroBools.Length);
    }

    private void UpdateBools()
    {
        for (int i = 0; i < _bools.Length; i++)
        {
            if (i < JapaneseModels.GetBools().Length)
            {
                JapaneseModels.UpdateBool(i, _bools[i]);
            }
            else if (i < JapaneseModels.GetBools().Length + KurokowaModels.GetBools().Length)
            {
                KurokowaModels.UpdateBool(i - JapaneseModels.GetBools().Length, _bools[i]);
            }
            else
            {
                BrutalistModels.UpdateBool(
                    i - JapaneseModels.GetBools().Length - KurokowaModels.GetBools().Length,
                    _bools[i]
                );
            }
        }
    }

    private void ChangeModel(int index)
    {
        for (int i = 0; i < _models.Length; i++)
        {
            if (i == index)
            {
                _bools[i] = true;
                _models[i].SetActive(true);
            }
            else
            {
                _bools[i] = false;
                _models[i].SetActive(false);
            }
        }
    }
}
