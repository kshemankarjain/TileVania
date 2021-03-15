using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip CoinPickUpSFX;
    [SerializeField] int PointsForCoinPickup = 100;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<GameSession>().AddToScore(PointsForCoinPickup);

        AudioSource.PlayClipAtPoint(CoinPickUpSFX, Camera.main.transform.position); 
        Destroy(gameObject); 

    }
}
