﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 100;
    [SerializeField] bool isPlayerShot = true;

    public int Damage { get => damage; }

    public void Hit()
    {
        Destroy(gameObject);
    }
}