using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swap : PowerUp
{
    public void Awake()
    {
        type = PowerUpTypes.SWAP;
    }

    protected override PowerUp GetPowerUp()
    {
        return this;
    }

    protected override PowerUpTypes GetType()
    {
        return type;
    }

    public override void UsePowerUp()
    {
        //    case 2: //Swap Players
        Debug.Log("Swap used");
        int random = Random.Range(0, 3);
        player.GetComponent<PlayerBody>().ChangePosition(random);
    }
}
