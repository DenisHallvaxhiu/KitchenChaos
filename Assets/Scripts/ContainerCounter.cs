using System;
using UnityEngine;

public class ContainerCounter : BaseCounter {

    public event EventHandler OnPlayerGrabObject;

    [SerializeField] private KitchenObjectsSO kitchenObjectsSO;

    public override void Interact(Player player) {
        if(!player.HasKitchenObject()) {
            //Player not carrying anything
            Transform kitchenObjectTransform = Instantiate(kitchenObjectsSO.prefab);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);

            OnPlayerGrabObject?.Invoke(this,EventArgs.Empty);
        }
    }

}
