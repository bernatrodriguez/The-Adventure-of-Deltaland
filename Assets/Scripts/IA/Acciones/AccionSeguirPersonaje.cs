using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Acciones/Seguir Personaje")] // Lo guardamos en el menú de Unity
public class AccionSeguirPersonaje : IAAccion
{
    public override void Ejecutar(IAController controller)
    {
        SeguirPersonaje(controller);
    }

    private void SeguirPersonaje(IAController controller)
    {
        if (controller.PersonajeReferencia == null) // Si la referencia es nula
        {
            return; // Terminamos al ejecución
        }

        // Si no es nula
        Vector3 dirHaciaPersonaje = controller.PersonajeReferencia.position - controller.transform.position; // Creamos un vector de dirección hacia el personaje
        Vector3 direccion = dirHaciaPersonaje.normalized; // Creamos un vector de dirección hacia el personaje normalizado (valor máximo 1)
        float distancia = dirHaciaPersonaje.magnitude; // Distancia del vector, es decir, hasta el personaje

        if (distancia >= 1.3f) // Si la distancia del enemigo al personaje es mayor o igual a 1.5
        {
            controller.transform.Translate(direccion * controller.VelocidadMovimiento * Time.deltaTime); // Lo sigue
        }
        // Si esta distancia es menor, deja de ejecutarse el código y el enemigo se detiene para no ponerse encima nuestro
    }
}
