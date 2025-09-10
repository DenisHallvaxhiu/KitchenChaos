using UnityEngine;

public class CuttingCounter : BaseCounter {

    [SerializeField] private KitchenObjectsSO cutKitchenObjectSO;

    public override void Interact(Player player) {
        if(!HasKitchenObject()) {
            //There is no kitchen object
            if(player.HasKitchenObject()) {
                //Player is carrying smth
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else {
                //Player not carrying anything
            }
        }
        else {
            if(player.HasKitchenObject()) {
                //Player is carrying smth
            }
            else {
                //Player not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
    public override void InteractAlternate(Player player) {
        if(HasKitchenObject()) {
            //There is a object
            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(cutKitchenObjectSO,this);
        }
    }

}
