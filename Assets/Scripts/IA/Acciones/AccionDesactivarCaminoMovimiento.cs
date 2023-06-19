using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Acciones/Desactivar Camino Movimiento")] // Lo guardamos en el men� de Unity
public class AccionDesactivarCaminoMovimiento : IAAccion
{
    public override void Ejecutar(IAController controller)
    {
        if (controller.EnemigoMovimiento == null) // Si no tenemos la referencia de la clase EnemigoMovimiento
        {
            return; // Terminamos la ejecuci�n
        }

        // Si la tenemos
        controller.EnemigoMovimiento.enabled = false; // Desactivamos el movimiento en el camino
    }
}
