using UnityEngine;

public class ClearCounter : BaseCounter {

    [SerializeField] private KitchenObjectsSO kitchenObjectsSO;

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
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    //Player holding a plate
                    if(plateKitchenObject.TryAddIngridient(GetKitchenObject().GetKitchenObjectsSO())) {
                        GetKitchenObject().DestroySelf();
                    }
                }
                else {
                    //Player not carrying plate but smth else
                    if(GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
                        //Counter Holding a plate
                        if(plateKitchenObject.TryAddIngridient(player.GetKitchenObject().GetKitchenObjectsSO())) {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else {
                //Player not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

}
