﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 100;
    
    [Header("Shooting")]
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 1f;
    
    [Header("Explosion")]
    [SerializeField] GameObject explosionVFX;
    [SerializeField] float explosionDuration = 2f;
    

    // Start is called before the first frame update
    void Start()
    {
        UpdateShotCounter();
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            UpdateShotCounter();
        }
    }

    private void UpdateShotCounter()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    private void Fire()
    {
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(1, -projectileSpeed);
    }

    private void OnTriggerEnter2D(Collider2D laser)
    {
        DamageDealer damageDealer = laser.gameObject.GetComponent<DamageDealer>();

        if (damageDealer != null)
        {
            ProcessHit(damageDealer);
        }    
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        damageDealer.Hit();

        health -= damageDealer.Damage;

        if (health <= 0)
        {
            Destroy(gameObject);
            GameObject explosion = Instantiate(explosionVFX, transform.position, transform.rotation);
            Destroy(explosion, explosionDuration);
        }
    }
}