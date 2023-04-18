using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Personaje personaje;
    [SerializeField] private Transform puntoReaparicion;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) // Si pulsamos la letra R
        {
            if (personaje.PersonajeVida.Derrotado) // Si el personaje está derrotado
            {
                personaje.transform.localPosition = puntoReaparicion.position; // Llevamos al personaje al punto de reaparición
                personaje.RestaurarPersonaje(); // Restauramos el personaje
            }
        }
    }
}
