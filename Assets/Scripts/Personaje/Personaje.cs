using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : MonoBehaviour
{
    public PersonajeVida PersonajeVida { get; private set; }
    public PersonajeAnimaciones PersonajeAnimaciones { get; private set; }

    private void Awake()
    {
        PersonajeVida = GetComponent<PersonajeVida>(); // Referenciamos la clase PersonajeVida
        PersonajeAnimaciones = GetComponent<PersonajeAnimaciones>(); // Referenciamos la clase PersonajeAnimaciones
    }

    public void RestaurarPersonaje()
    {
        PersonajeVida.RestaurarPersonaje(); // Restauramos la vida
        PersonajeAnimaciones.RevivirPersonaje(); // Restauramos las animaciones
    }
}
