using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public int HealthPoint { get; private set; } = 100;
}
