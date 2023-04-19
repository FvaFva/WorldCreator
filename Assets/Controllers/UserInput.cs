using System;
using UnityEngine;
using UnityEngine.UI;

public class UserInput : MonoBehaviour
{
    [SerializeField] private Button _createRandomMap;
    [SerializeField] private Button _createLinksMap;
    [SerializeField] private Button _createWafeColapsMap;

    public event Action<int> RequestedNewMap;

    private void OnEnable()
    {
        UpdateLisening(true);
    }

    private void OnDisable()
    {
        UpdateLisening(false);
    }

    private void OnLinksRequest()
    {
        RequestedNewMap?.Invoke(1);
    }
    private void OnRandomRequest()
    {
        RequestedNewMap?.Invoke(2);
    }
    private void OnWaveColapsRequest()
    {
        RequestedNewMap?.Invoke(3);
    }

    private void UpdateLisening(bool isActive)
    {
        if(isActive)
        {
            if(_createLinksMap != null)
                _createLinksMap.onClick.AddListener(OnLinksRequest);
            if (_createLinksMap != null)
                _createRandomMap.onClick.AddListener(OnRandomRequest);
            if (_createLinksMap != null)
                _createWafeColapsMap.onClick.AddListener(OnWaveColapsRequest);
        }
        else
        {
            if (_createLinksMap != null)
                _createLinksMap.onClick.RemoveListener(OnLinksRequest);
            if (_createLinksMap != null)
                _createRandomMap.onClick.RemoveListener(OnRandomRequest);
            if (_createLinksMap != null)
                _createWafeColapsMap.onClick.RemoveListener(OnWaveColapsRequest);
        }
    }
}
