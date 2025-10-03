using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour {

    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Button soundEffectButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;

    //KeyBindings
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAlternateText;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private TextMeshProUGUI gamepadInteractText;
    [SerializeField] private TextMeshProUGUI GamepadInteractAlternateText;
    [SerializeField] private TextMeshProUGUI gamepadPauseText;
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAlternateButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button gamepadInteractButton;
    [SerializeField] private Button gamepadInteractAlternateButton;
    [SerializeField] private Button gamepadPauseButton;
    [SerializeField] Transform pressToRebindKeyTransform;


    private Action onCloseButtonAction;


    private void Awake() {
        Instance = this;
        soundEffectButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        closeButton.onClick.AddListener(() =>
        {
            onCloseButtonAction();
            Hide();
        });

        moveUpButton.onClick.AddListener(() => { RebindBinding(GameScript.Binding.Move_Up); });
        moveDownButton.onClick.AddListener(() => { RebindBinding(GameScript.Binding.Move_Down); });
        moveLeftButton.onClick.AddListener(() => { RebindBinding(GameScript.Binding.Move_Left); });
        moveRightButton.onClick.AddListener(() => { RebindBinding(GameScript.Binding.Move_Right); });
        interactButton.onClick.AddListener(() => { RebindBinding(GameScript.Binding.Interact); });
        interactAlternateButton.onClick.AddListener(() => { RebindBinding(GameScript.Binding.InteractAlternate); });
        pauseButton.onClick.AddListener(() => { RebindBinding(GameScript.Binding.Pause); });
        gamepadInteractButton.onClick.AddListener(() => { RebindBinding(GameScript.Binding.Gamepad_Interact); });
        gamepadInteractAlternateButton.onClick.AddListener(() => { RebindBinding(GameScript.Binding.Gamepad_InteractAlternate); });
        gamepadPauseButton.onClick.AddListener(() => { RebindBinding(GameScript.Binding.Gamepad_Pause); });

    }

    private void Start() {

        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;

        UpdateVisual();
        Hide();
        HideShowPressToRebindKey();
    }

    private void GameManager_OnGameUnpaused(object sender,System.EventArgs e) {
        Hide();
    }

    private void UpdateVisual() {
        soundEffectsText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);


        moveUpText.text = GameScript.Instance.GetBindingText(GameScript.Binding.Move_Up);
        moveDownText.text = GameScript.Instance.GetBindingText(GameScript.Binding.Move_Down);
        moveLeftText.text = GameScript.Instance.GetBindingText(GameScript.Binding.Move_Left);
        moveRightText.text = GameScript.Instance.GetBindingText(GameScript.Binding.Move_Right);
        interactText.text = GameScript.Instance.GetBindingText(GameScript.Binding.Interact);
        interactAlternateText.text = GameScript.Instance.GetBindingText(GameScript.Binding.InteractAlternate);
        pauseText.text = GameScript.Instance.GetBindingText(GameScript.Binding.Pause);
        gamepadInteractText.text = GameScript.Instance.GetBindingText(GameScript.Binding.Gamepad_Interact);
        GamepadInteractAlternateText.text = GameScript.Instance.GetBindingText(GameScript.Binding.Gamepad_InteractAlternate);
        gamepadPauseText.text = GameScript.Instance.GetBindingText(GameScript.Binding.Gamepad_Pause);
    }

    public void Show(Action onCloseButtonAction) {
        this.onCloseButtonAction = onCloseButtonAction;
        gameObject.SetActive(true);

        soundEffectButton.Select();
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    private void ShowPressToRebindKey() {
        pressToRebindKeyTransform.gameObject.SetActive(true);
    }
    private void HideShowPressToRebindKey() {
        pressToRebindKeyTransform.gameObject.SetActive(false);
    }

    private void RebindBinding(GameScript.Binding binding) {
        ShowPressToRebindKey();
        GameScript.Instance.RebindBinding(binding,() =>
        {
            HideShowPressToRebindKey();
            UpdateVisual();
        });
    }
}
