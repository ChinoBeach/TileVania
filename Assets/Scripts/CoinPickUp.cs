using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    [SerializeField] AudioClip audioCoinPickupSFX;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(audioCoinPickupSFX, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
}
