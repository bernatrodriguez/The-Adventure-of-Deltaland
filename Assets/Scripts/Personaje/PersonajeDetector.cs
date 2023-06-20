using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PersonajeDetector : MonoBehaviour
{
    public static Action<EnemigoInteraccion> EventoEnemigoDetectado; // Evento al detectar al enemigo
    public static Action EventoEnemigoPerdido; // Evento al no detectar el enemigo

    public EnemigoInteraccion EnemigoDetectado { get; private set; } // Propiedad para guardar la referencia del enemigo

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemigo")) // Si el objeto que entra tiene la etiqueta Enemigo
        {
            EnemigoDetectado = other.GetComponent<EnemigoInteraccion>(); // Creamos al referencia

            if (EnemigoDetectado.GetComponent<EnemigoVida>().Salud > 0) // Si el enemigo está con vida
            {
                EventoEnemigoDetectado?.Invoke(EnemigoDetectado); // Si el evento de enemigo detectado no es nulo, lo invocamos
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemigo")) // Si el objeto que sale tiene la etiqueta Enemigo
        {
            EventoEnemigoPerdido?.Invoke(); // Si el evento no es nulo, lo invocamos
        }
    }
}
