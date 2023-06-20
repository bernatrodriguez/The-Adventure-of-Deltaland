using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SeleccionManager : MonoBehaviour
{
    public static Action<EnemigoInteraccion> EventoEnemigoSeleccionado; // Evento de enemigo seleccionado
    public static Action EventoObjetoNoSeleccionado; // Evento de no seleccionado

    public EnemigoInteraccion EnemigoSeleccionado { get; set; } // Propiedad para guardar al enemigo seleccionado

    private Camera camara; // Cámara

    private void Start()
    {
        camara = Camera.main; // Referenciamos la cámara principal del juego
    }

    private void Update()
    {
        SeleccionarEnemigo(); // Seleccionamos al enemigo
    }

    private void SeleccionarEnemigo()
    {
        if (Input.GetMouseButtonDown(0)) // Si hacemos click izquierdo
        {
            RaycastHit2D hit = Physics2D.Raycast(camara.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Enemigo")); // Creamos un raycast en la posición del ratón hacia el enemigo
            if (hit.collider != null) // Si hemos colisionado con el enemigo haciendo click
            {
                EnemigoSeleccionado = hit.collider.GetComponent<EnemigoInteraccion>(); // Seleccionamos el enemigo
                EnemigoVida enemigoVida = EnemigoSeleccionado.GetComponent<EnemigoVida>(); // Referencia a la vida del enemigo

                if (enemigoVida.Salud > 0f) // Si el enemigo tiene vida
                {
                    EventoEnemigoSeleccionado?.Invoke(EnemigoSeleccionado);
                }
                else // Si no
                {
                    EnemigoLoot loot = EnemigoSeleccionado.GetComponent<EnemigoLoot>();
                    LootManager.Instance.MostrarLoot(loot); // Mostramos el loot
                }
            }
            else // Si no se ha colisionado
            {
                EventoObjetoNoSeleccionado?.Invoke(); // Si el evento de objeto no seleccionado no es nulo, lo invocamos
            }
        }
    }
}
