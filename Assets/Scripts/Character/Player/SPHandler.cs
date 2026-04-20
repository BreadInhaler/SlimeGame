[System.Serializable]
public class SPHandler{
    // ----------------------------------- quantity --------------------------------------
    [UnityEngine.SerializeField] private float amount;
    [UnityEngine.SerializeField]private float maxAmount;
    // ----------------------------------- regen --------------------------------------
    [UnityEngine.SerializeField]private float regenAmount=1f; //amount of sp regenerated in tick
    [UnityEngine.SerializeField]private float regenTick=0.1f; // number in seconds it takes to tick regen of sp
    private float regenTimer=0; //timer to countDown regen
    // ----------------------------------- regen delay --------------------------------------
    [UnityEngine.SerializeField]private float regenDelay=3f; //time delay in seconds to start regening sp after an attack
    private float spDelay; //timer of the sp regen delay after an attack
    // ----------------------------------- other stuff --------------------------------------
    private Player player;
    public SPHandler(Player player,float amount,float maxAmount){
        this.player=player;
        this.amount=amount;
        this.maxAmount=maxAmount;
        UpdateSPUI();
    }
    public float AddAmount(float amount){
        this.amount+=amount;
        if(this.amount>maxAmount)amount=maxAmount; 
        UpdateSPUI();
        return this.amount;
    }
    public float AddAmount(float amount, bool mult){
        UnityEngine.Debug.Log("current amount of sp to add "+amount+" and is % "+mult);
        if(mult == false) return AddAmount(amount);
        float finalAmount = amount * maxAmount;
        UnityEngine.Debug.Log("final amount to add "+finalAmount);
        if(this.amount + finalAmount > maxAmount) this.amount = maxAmount;
        else this.amount += finalAmount;
        UpdateSPUI();
        return this.amount;
    }
    public float RemoveAmount(int amount){
        if(HasEnough(amount)) this.amount-=amount;
        UpdateSPUI();
        return this.amount;
    }
    public bool HasEnough(int amount){
        return this.amount >= amount;
    }
    public void UpdateSPAmout(float deltaTime){
        spDelay+=deltaTime;
        if(spDelay<regenDelay) return;
        if(regenTimer>=regenTick){
            regenTimer=0;
            AddAmount(regenAmount);
        }else regenTimer+=deltaTime;
    }
    public float GetAmount(){
        return amount;
    }
    public float GetMaxAmount(){
        return maxAmount;
    }
    private void UpdateSPUI() {
        player.hudData.playerSP=this.amount/this.maxAmount;
        player.hudHandler.UpdateUI(player.hudData);
    }
    public void ResetDelayTimer(){
        this.spDelay=0;
    }
}