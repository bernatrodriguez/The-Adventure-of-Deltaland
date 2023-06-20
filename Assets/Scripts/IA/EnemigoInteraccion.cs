using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TipoDeteccion // Tipo de detecci�n del enemigo
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
        if (tipo == TipoDeteccion.Rango) // Si el tipo de detecci�n es de tipo rango
        {
            seleccionRangoFX.SetActive(estado); // Mostramos la selecci�n de rango
        }
        else // En caso contratrio
        {
            seleccionMeleeFX.SetActive(estado); // Mostramos la selecci�n de melee
        }
    }

    public void DesactivarSpritesSeleccion() // M�todo para desactivar los arcos de selecci�n de forma forzosa
    {
        seleccionMeleeFX.SetActive(false);
        seleccionRangoFX.SetActive(false);
    }
}
