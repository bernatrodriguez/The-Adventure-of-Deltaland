using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TipoDeteccion // Tipo de detección del enemigo
{
    Rango,
    Melee
}

public class EnemigoInteraccion : MonoBehaviour
{
    [SerializeField] private GameObject seleccionRangoFX;
    [SerializeField] private GameObject seleccionMeleeFX;

    public void MostrarEnemigoSeleccionado(bool estado, TipoDeteccion tipo)
    {
        if (tipo == TipoDeteccion.Rango) // Si el tipo de detección es de tipo rango
        {
            seleccionRangoFX.SetActive(estado); // Mostramos la selección de rango
        }
        else // En caso contratrio
        {
            seleccionMeleeFX.SetActive(estado); // Mostramos la selección de melee
        }
    }

    public void DesactivarSpritesSeleccion() // Método para desactivar los arcos de selección de forma forzosa
    {
        seleccionMeleeFX.SetActive(false);
        seleccionRangoFX.SetActive(false);
    }
}
