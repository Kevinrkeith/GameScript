using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PowerupScript : MonoBehaviour
{
    [SerializeField]
    private Material orange;
    [SerializeField]
    private Material pink;
    [SerializeField]
    private Material  inactiveMaterial;

    [SerializeField]
    private MeshRenderer meshRenderer;

    [SerializeField]
    private float timeUntilChange;
    [SerializeField]
    private float timeUntilRespawn;

    private float count = 0;
    [SerializeField]
    private PowerUp[] powerUps;

    private bool isPink = false;
    private bool isHidden = false;

    // Update is called once per frame
    void Update()
    {
        count += Time.deltaTime;
        if(count >= timeUntilChange && !isHidden)
        {
            count = 0;
            if (isPink) meshRenderer.material = orange;
            else meshRenderer.material = pink;
            isPink = !isPink;
        }
        else if (isHidden && count >= timeUntilRespawn)
        {
            count = 0;
            isHidden = false;
            if (isPink) meshRenderer.material = pink;
            else meshRenderer.material = orange;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if ((other.tag == "Player1" || other.tag == "Player2" || other.tag == "Player3" || other.tag == "Player4") && !isHidden)
        {
            int powerUpIndex = Random.Range(0,powerUps.Length);
            powerUps[powerUpIndex].SetPlayerObject(other.gameObject);
            other.GetComponent<PlayerBody>().GeneratePowerup(powerUps[powerUpIndex]);
            meshRenderer.material = inactiveMaterial;
            count = 0;
            isHidden = true;
        }
    }
}
