using UnityEngine;
using UnityEngine.UI;
public class PlateIconSingleUI : MonoBehaviour {
    [SerializeField] private Image image;
    public void SetKitchenObjectSO(KitchenObjectsSO kitchenObjectsSO) {
        image.sprite = kitchenObjectsSO.sprite;
    }
}
