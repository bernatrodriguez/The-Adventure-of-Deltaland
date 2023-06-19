using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPorAgregar : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private InventarioItem InventarioItemReferencia;
    [SerializeField] private int cantidadPorAgregar;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Si estamos colisionando con el player
        {
            Inventario.Instance.AñadirItem(InventarioItemReferencia, cantidadPorAgregar); // Añadimos el item al inventario
            Destroy(gameObject); // Destruimos el objeto del juego, es decir, lo hacemos desaparecer del mapa
        }
    }
}
