using DG.Tweening;
using TMPro;
using UnityEngine;

public class TımerUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform _timerRotatiableTransform;
    [SerializeField] private TMP_Text _timerText;

    [Header("Settings")]
    [SerializeField] private float _rotationDuration;
    [SerializeField] private Ease _rotationEase;

    private float _elapsedTime;

    private void Start()
    {
        PlayRotationAnimation();
        StartTimer();
    }

    private void PlayRotationAnimation()
    {
        _timerRotatiableTransform.DORotate(new Vector3(0f, 0f, -360f), _rotationDuration, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(_rotationEase  );
    }

    private void StartTimer()
    {
        _elapsedTime = 0f;
        InvokeRepeating(nameof(UptadeTimerUI), 0f, 1f);
    }

    private void UptadeTimerUI()
    {
        _elapsedTime += 1f;

        int minutes = Mathf.FloorToInt (_elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(_elapsedTime % 60f);

        _timerText.text = string.Format("{0:00}:{1:00}",minutes,seconds);
    }
}
