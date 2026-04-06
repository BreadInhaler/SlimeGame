public class Wallet{
    private int amount;
    private Player player;
    public Wallet(Player player,int amount){
        this.player=player;
        this.amount=amount;
        UpdateWalletUI();
    }
    public int AddAmount(int amount){
        this.amount+=amount;
        UpdateWalletUI();
        return this.amount;
    }
    public int RemoveAmount(int amount){
        if(HasEnough(amount)) this.amount-=amount;
        UpdateWalletUI();
        return this.amount;
    }
    public bool HasEnough(int amount){
        return this.amount <= amount;
    }
    private void UpdateWalletUI() {
        player.hudData.playerMoney=this.amount;
        player.hudHandler.UpdateUI(player.hudData);
    }
}