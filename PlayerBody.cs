using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBody : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float turnSpeed;
    [SerializeField]
    private float collisionExplosionStrength;
    [SerializeField]
    private float collisionExplosionLift;
    [SerializeField]
    private float collisionExplosionRadius;
    [SerializeField]
    private float collisionLossMultiplyer;
    [SerializeField]
    private float collisionWinMultiplyer;

    [SerializeField]
    private float powerupGhostDuration;

    [SerializeField]
    private bool[] powerups;

    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private GameObject[] otherPlayers;

    [SerializeField]
    private Material ghostMaterial;
    [SerializeField]
    private Material normalMaterial;

    internal void setMesh(bool v)
    {
        Debug.Log("Swapped");
    }

    [SerializeField]
    private MeshRenderer meshRenderer;

    [SerializeField]
    private Text powerupText;
    [SerializeField]
    private Text hitByText;
    [SerializeField]
    private Text killsText;
    [SerializeField]
    private Text deathsText;
    [SerializeField]
    private Transform cameraTransform;

    private PowerUp powerUp;

    private Vector3 direction;
    private Vector3 lastPosition;

    public bool isFiring = false;

    private float ghostCount = -1;

    public string hitLastBy = "";
    public int kills = 0;
    public int deaths = 0;
    public int playerNumber;
    //private void Start()
    //{
    //    powerupText = GetComponent<Text>();
    //}
    private void Awake()
    {
        powerupText.text = "PowerUp: None";
    }
    //Update is called once per frame
    private void Update()
    {
        isFiring = false;
        if (Input.GetAxis("Fire" + playerNumber) > 0) isFiring = true;
        if (isFiring)
        {
            if (powerUp != null)
            {
                powerUp.UsePowerUp();
                powerUp = null;
                powerupText.text = "Power Up: ";
            }
        }
        
        if (ghostCount >= powerupGhostDuration)
        {
            
            meshRenderer.material = normalMaterial;
            ghostCount = -1;
            Debug.Log(gameObject.tag);
            gameObject.tag = "Player" + playerNumber;
            Debug.Log(gameObject.tag);
            SetMaterial(false);
        }
        if (gameObject.tag == "Ghost")
        {
            if (ghostCount >= 0) ghostCount += Time.deltaTime;
        }
        //if (powerups[0])
        //{
        //    if (powerups[1]) powerupText.text = "Powerup: Ghost";
        //    else if (powerups[2]) powerupText.text = "Powerup: Swap";
        //}        
        //else powerupText.text = "Powerup: None";
        killsText.text = "Kills: " + kills;
        deathsText.text = "Deaths: " + deaths;
        hitByText.text = "Last Hit: " + hitLastBy;
    }
    public void ChangePosition(int random)
    {
        Vector3 otherPosition = otherPlayers[random].transform.position;
        otherPlayers[random].transform.position = transform.position;
        transform.position = otherPosition;
    }
    void FixedUpdate()
    {
        direction = new Vector3(Input.GetAxis("Horizontal" + playerNumber), 0, Input.GetAxis("Vertical" + playerNumber));

        rb.AddForce(direction.x * Time.fixedDeltaTime * speed * cameraTransform.right, ForceMode.Acceleration);
        rb.AddForce(direction.z * Time.fixedDeltaTime * turnSpeed * cameraTransform.forward, ForceMode.Acceleration);

        lastPosition = transform.position; //for camera

    }
    public GameObject[] GetOthers()
    {
        return otherPlayers;
    }
    //controller changes this
    public void SetMovement(Vector3 value)
    {
        direction = value;
    }

    public void SetFire(bool value)
    {
        isFiring = value;
    }

    //for camera
    public float GetLookDirection()
    {
        if (Mathf.Atan2(transform.position.x - lastPosition.x, transform.position.z - lastPosition.z) * (180 / Mathf.PI) != 0) return Mathf.Atan2(transform.position.x - lastPosition.x, transform.position.z - lastPosition.z) * (180 / Mathf.PI);
        else return transform.rotation.eulerAngles.y;
    }

    //used to determine win on collision and the effects of collisions
    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;

        if (tag == "Ghost")
        {
            Vector3 collisionLocation = collision.GetContact(0).point;
            Rigidbody otherRB = collision.gameObject.GetComponent<Rigidbody>();
            transform.position = (transform.position + ((collisionLocation - transform.position) * 5f));
        }
        else if (tag == "Player1" || tag == "Player2" || tag == "Player2" || tag == "Player4")
        {
            hitLastBy = tag;
            Vector3 collisionLocation = collision.GetContact(0).point;
            Rigidbody otherRB = collision.gameObject.GetComponent<Rigidbody>();

            if (otherRB.velocity.magnitude >= rb.velocity.magnitude) rb.AddExplosionForce(collisionExplosionStrength * collisionWinMultiplyer, collisionLocation, collisionExplosionRadius, collisionExplosionLift * collisionWinMultiplyer);
            else
            {
                if (otherRB.velocity.magnitude < rb.velocity.magnitude) rb.AddExplosionForce(collisionExplosionStrength * collisionLossMultiplyer, collisionLocation, collisionExplosionRadius, collisionExplosionLift * collisionLossMultiplyer);
            }
        }
    }

    public void GeneratePowerup(PowerUp powerUp)
    {
        this.powerUp = powerUp;
        powerupText.text = "PowerUp: " + powerUp.GetType();
    }
    public void SetMaterial(Boolean isActive)
    {
        if (isActive)
        {
            meshRenderer.material = ghostMaterial;
            tag = "Ghost";
            ghostCount = 0;
        }
        else
        {
            meshRenderer.material = normalMaterial;
            tag = "Player" + playerNumber;
        }
    }
    private void PowerupActivate(int value)
    {
        //switch (value)
        //{
        //    case 1: //Ghost
        //        meshRenderer.material = ghostMaterial;
        //        tag = "Ghost";
        //        ghostCount = 0;
        //        powerups[0] = false;
        //        powerups[1] = false;
        //        break;
        //    case 2: //Swap Players
        //        powerups[0] = false;
        //        powerups[2] = false;
        //        int random = Random.Range(0, 3);
        //        Vector3 otherPosition = otherPlayers[random].transform.position;
        //        otherPlayers[random].GetComponent<PlayerBody>().hitLastBy = "Player" + playerNumber;
        //        otherPlayers[random].transform.position = transform.position;
        //        transform.position = otherPosition;
        //        break;
        //}
    }
}
