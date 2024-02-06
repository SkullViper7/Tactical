using System;
using Unity.VisualScripting;
using UnityEngine;

public class TurnGameSystemTransmitter : MonoBehaviour
{
    public bool GameLaunched;
    public bool PlayerCanPlay;
    public bool PlayerHasPlayedOrNot;
    public bool PlayerStillAlive;

    public event Action<bool> HasPlayerHavePlayed;

    public void PlayerHasPlayed(bool playerHasPlayed)
    {
        PlayerHasPlayedOrNot = playerHasPlayed;
        HasPlayerHavePlayed?.Invoke(PlayerHasPlayedOrNot);
    }

    public bool MonstersCanPlay;
    public bool MonstersHasPlayedOrNot;
    public bool AreMonstersStillAlives;

    public event Action<bool> HaveMonstersPlayed;

    public void MonstersHavePlayed(bool monstersHavePlayed)
    {
        MonstersHasPlayedOrNot = monstersHavePlayed;
        HaveMonstersPlayed?.Invoke(MonstersHasPlayedOrNot);
    }


}
