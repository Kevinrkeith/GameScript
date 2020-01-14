using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.PowerupScripts
{
    public class Ghost : PowerUp
    {
        public void Awake()
        {
            type = PowerUpTypes.GHOST;
            timer = 5;

        }
        float timer;
        //public override void UsePowerUp()
        //{
        //    player.GetComponent<PlayerBody>().setMesh(true);
        //    for (float time = 0f; time<timer;time+=Time.deltaTime)
        //    {
        //        player.GetComponent <PlayerBody>().setMesh(false);
        //    }
        //}
        protected override PowerUp GetPowerUp()
        {
            return this;
        }

        protected override PowerUpTypes GetType()
        {
            Debug.Log("Goes through here");
            return type;
        }

        public override void UsePowerUp()
        {
            Debug.Log("Swap used");
            player.GetComponent<PlayerBody>().SetMaterial(true);
            string originalTag = player.tag;
            player.tag = "Ghost";
            
        }
        //Used an abstract class to better manage everything and so that we can make it easier to randomize certain elements
    }
}