using System;
using UnityEngine;
using UnityEngine.UI;

public class UserInput : MonoBehaviour
{
    [SerializeField] private Button _createRandomMap;
    [SerializeField] private Button _createLinksMap;
    [SerializeField] private Button _createWaveCollapseMap;
    [SerializeField] private Button _saveCoefficients;

    [SerializeField] private Slider _height;
    [SerializeField] private Slider _low;
    [SerializeField] private Slider _bridge;
    [SerializeField] private Slider _clear;
    [SerializeField] private Slider _road;

    public event Action<int> RequestedNewMap;
    public event Action<float, float, float, float, float> ChangedCoefficient;

    private void OnEnable()
    {
        UpdateListening(true);
    }

    private void OnDisable()
    {
        UpdateListening(false);
    }

    private void OnLinksRequest()
    {
        RequestedNewMap?.Invoke(1);
    }

    private void OnRandomRequest()
    {
        RequestedNewMap?.Invoke(2);
    }

    private void OnWaveCollapseRequest()
    {
        RequestedNewMap?.Invoke(3);
    }

    private void OnCoefficientChanged()
    {
        ChangedCoefficient?.Invoke(_height.value, _low.value, _bridge.value, _clear.value, _road.value);
    }

    private void UpdateListening(bool isActive)
    {
        if(isActive)
        {
            if(_createLinksMap != null)
                _createLinksMap.onClick.AddListener(OnLinksRequest);

            if (_createLinksMap != null)
                _createRandomMap.onClick.AddListener(OnRandomRequest);

            if (_createLinksMap != null)
                _createWaveCollapseMap.onClick.AddListener(OnWaveCollapseRequest);

            if (_createLinksMap != null)
                _saveCoefficients.onClick.AddListener(OnCoefficientChanged);
        }
        else
        {
            if (_createLinksMap != null)
                _createLinksMap.onClick.RemoveListener(OnLinksRequest);
            if (_createLinksMap != null)
                _createRandomMap.onClick.RemoveListener(OnRandomRequest);
            if (_createLinksMap != null)
                _createWaveCollapseMap.onClick.RemoveListener(OnWaveCollapseRequest);
            if (_createLinksMap != null)
                _saveCoefficients.onClick.RemoveListener(OnCoefficientChanged);
        }
    }
}
