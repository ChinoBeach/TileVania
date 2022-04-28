using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    [SerializeField] AudioClip audioCoinPickupSFX;
    [SerializeField] int intPointsForCoinPickup = 100;

    bool bolWasCollected = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !bolWasCollected)
        {
            bolWasCollected = true;

            FindObjectOfType<GameSession>().AddToScore(intPointsForCoinPickup);
            AudioSource.PlayClipAtPoint(audioCoinPickupSFX, Camera.main.transform.position);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
