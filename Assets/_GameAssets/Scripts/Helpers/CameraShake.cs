using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }

    private CinemachineBasicMultiChannelPerlin _MultiChannelPerlin;

    private float _ShakeTime;
    private float _ShakeTimeTotal;
    private float _startingIntensity;

    private void Awake()
    {
        Instance = this;
        _MultiChannelPerlin = GetComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private IEnumerator CameraShakeCoroutine(float intensity, float time, float delay)
    {
        yield return new WaitForSeconds(delay);
        _MultiChannelPerlin.AmplitudeGain = intensity;
        _ShakeTime = time;
        _ShakeTimeTotal = time;
        _startingIntensity = intensity;
    }

    public void ShakeCamera(float intensity, float time, float delay = 0f)
    {
        StartCoroutine(CameraShakeCoroutine(intensity, time, delay));
    }

    private void Update()
    {
        if (_ShakeTime > 0f)
        {
            _ShakeTime -= Time.deltaTime;

            if (_ShakeTime <= 0f)
            {
                _MultiChannelPerlin.AmplitudeGain = Mathf.Lerp(_startingIntensity, 0f, 1 - (_ShakeTime / _ShakeTimeTotal));
            }
        }
    }
}
