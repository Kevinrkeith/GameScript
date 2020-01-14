using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    [SerializeField]
    protected PowerUpTypes type;
    [SerializeField]
    protected GameObject player;
    public void SetPlayerObject(GameObject player)
    {
        
        this.player = player;
    }
    protected abstract PowerUp GetPowerUp();
    protected abstract PowerUpTypes GetType();
    public abstract void UsePowerUp();
}
public enum PowerUpTypes
{
    SWAP,
    GHOST
}
