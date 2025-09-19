using System;
using System.Collections.Generic;
using UnityEngine;
public class PlateKitchenObject : KitchenObject {

    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs {
        public KitchenObjectsSO kitchenObjectSO;
    }

    [SerializeField] private List<KitchenObjectsSO> validKitchenObjectSOList;

    private List<KitchenObjectsSO> kitchenObjectSOList;

    private void Awake() {
        kitchenObjectSOList = new List<KitchenObjectsSO>();
    }

    public bool TryAddIngridient(KitchenObjectsSO kitchenObjectsSO) {
        if(!validKitchenObjectSOList.Contains(kitchenObjectsSO)) {
            //Not valid ingredient
            return false;
        }
        if(kitchenObjectSOList.Contains(kitchenObjectsSO)) {
            return false;
        }
        else {
            kitchenObjectSOList.Add(kitchenObjectsSO);

            OnIngredientAdded?.Invoke(this,new OnIngredientAddedEventArgs
            {
                kitchenObjectSO = kitchenObjectsSO
            });
            return true;
        }

    }
}
