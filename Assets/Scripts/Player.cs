﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 200;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = .2f;

    [Header("Sounds")]
    [SerializeField] AudioClip laserSFX;
    [SerializeField] [Range(0, 1)] float laserSFXVolume = 0.7f;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0, 1)] float deathSFXVolume = 0.7f;

    Coroutine fireCoroutine;

    float xMin, xMax, yMin, yMax;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    public int GetHealth()
    {
        return health;
    }

    private void OnTriggerEnter2D(Collider2D laser)
    {
        DamageDealer damageDealer = laser.gameObject.GetComponent<DamageDealer>();
        
        if (damageDealer != null)
        {
            ProccessHit(damageDealer);
        }
    }

    private void ProccessHit(DamageDealer damageDealer)
    {
        damageDealer.Hit();
        
        health -= damageDealer.Damage;
        
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<Level>().LoadGameOver();
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
        Destroy(gameObject);
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            fireCoroutine = StartCoroutine(FireContinuously());
        }
        
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fireCoroutine);
        }
    }

    private IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(laserSFX, Camera.main.transform.position, laserSFXVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);

        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }
}
