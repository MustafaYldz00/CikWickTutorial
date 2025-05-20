using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinPopup : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TýmerUI _týmerUI; 
    [SerializeField] private Button _oneMoreButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private TMP_Text _timerText;


    private void OnEnable()
    {
        _timerText.text = _týmerUI.GetFinalTime();

        _oneMoreButton.onClick.AddListener(OnOneMoreButtonClicked);
    }

    private void OnOneMoreButtonClicked()
    {
        SceneManager.LoadScene(Consts.SceneNames.GAME_SCENE);
    }
}
