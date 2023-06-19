using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Acciones/Activar Camino Movimiento")] // Lo guardamos en el men� de Unity
public class AccionActivarCaminoMovimiento : IAAccion
{
    public override void Ejecutar(IAController controller)
    {
        if (controller.EnemigoMovimiento == null) // Si no tenemos la referencia de la clase EnemigoMovimiento
        {
            return; // Terminamos la ejecuci�n
        }

        // Si la tenemos
        controller.EnemigoMovimiento.enabled = true; // Activamos el movimiento en el camino
    }
}
