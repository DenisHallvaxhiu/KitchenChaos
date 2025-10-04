using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI keyMoveUpText;
    [SerializeField] private TextMeshProUGUI keyMoveDownText;
    [SerializeField] private TextMeshProUGUI keyMoveLeftText;
    [SerializeField] private TextMeshProUGUI keyMoveRightText;
    [SerializeField] private TextMeshProUGUI keyMoveInteractText;
    [SerializeField] private TextMeshProUGUI keyMoveInteractAlternateText;
    [SerializeField] private TextMeshProUGUI keyMovePauseText;
    [SerializeField] private TextMeshProUGUI gamepadInteractText;
    [SerializeField] private TextMeshProUGUI gamepadInteractAlternateText;
    [SerializeField] private TextMeshProUGUI gamepadPauseText;



    private void Start() {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        GameScript.Instance.OnBindingRebind += GameScript_OnBindingRebind;
        UpdateVisual();
        Show();
    }

    private void GameManager_OnStateChanged(object sender,System.EventArgs e) {
        if(GameManager.Instance.IsCountdownToStartActive()) {
            Hide();
        }
    }

    private void GameScript_OnBindingRebind(object sender,System.EventArgs e) {
        UpdateVisual();
    }

    private void UpdateVisual() {
        keyMoveUpText.text = GameScript.Instance.GetBindingText(GameScript.Binding.Move_Up);
        keyMoveDownText.text = GameScript.Instance.GetBindingText(GameScript.Binding.Move_Down);
        keyMoveLeftText.text = GameScript.Instance.GetBindingText(GameScript.Binding.Move_Left);
        keyMoveRightText.text = GameScript.Instance.GetBindingText(GameScript.Binding.Move_Right);
        keyMoveInteractText.text = GameScript.Instance.GetBindingText(GameScript.Binding.Interact);
        keyMoveInteractAlternateText.text = GameScript.Instance.GetBindingText(GameScript.Binding.InteractAlternate);
        keyMovePauseText.text = GameScript.Instance.GetBindingText(GameScript.Binding.Pause);
        gamepadInteractText.text = GameScript.Instance.GetBindingText(GameScript.Binding.Gamepad_Interact);
        gamepadInteractAlternateText.text = GameScript.Instance.GetBindingText(GameScript.Binding.Gamepad_InteractAlternate);
        gamepadPauseText.text = GameScript.Instance.GetBindingText(GameScript.Binding.Gamepad_Pause);
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
