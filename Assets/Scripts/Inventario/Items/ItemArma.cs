using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Arma")] // Permitimos poder crear armas desde el menú de Unity
public class ItemArma : InventarioItem
{
    [Header("Arma")]
    public Arma Arma; // Variable del tipo arma
}
