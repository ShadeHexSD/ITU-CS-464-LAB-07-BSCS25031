using System;

// Holds the player's money. Static = one shared copy for the whole game.
public static class Wallet
{
    public static int Coins = 1240;
    public static int Gems = 85;
    public static event Action Changed;   // screens listen to this to refresh their numbers

    public static void AddCoins(int amount) { Coins += amount; Changed?.Invoke(); }
    public static void AddGems(int amount) { Gems += amount; Changed?.Invoke(); }

    // Returns false (and changes nothing) if you cannot afford it.
    public static bool TrySpendGems(int amount)
    {
        if (Gems < amount) return false;
        Gems -= amount;
        Changed?.Invoke();
        return true;
    }
}