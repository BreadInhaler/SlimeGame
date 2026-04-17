[System.Serializable]
public class SPHandler{
    public float amount;
    public float maxAmount;
    private Player player;
    public SPHandler(Player player,int amount,int maxAmount){
        this.player=player;
        this.amount=amount;
        this.maxAmount=maxAmount;
        UpdateSPUI();
    }
    public float AddAmount(int amount){
        if(this.amount==maxAmount) return this.amount;
        if(this.amount+amount>=maxAmount) return this.amount;
        this.amount+=amount;
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
    private void UpdateSPUI() {
        player.hudData.playerSP=this.amount/this.maxAmount;
        player.hudHandler.UpdateUI(player.hudData);
    }
}