public static class Wallet{
    private static int amount;
    public static int AddAmount(int amount){
        Wallet.amount+=amount;
        return amount;
    }
    public static int RemoveAmount(int amount){
        if(HasEnough(amount)) Wallet.amount-=amount;
        return amount;
    }
    public static bool HasEnough(int amount){
        return amount <= Wallet.amount;
    }
}