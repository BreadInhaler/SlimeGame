using UnityEngine;
public class GlobalLoader : MonoBehaviour{
    public Sprite moneyIcon;
    private void Awake() {
        LookUpResources.Init();
        Globals.moneyIcon=this.moneyIcon;
    }
}