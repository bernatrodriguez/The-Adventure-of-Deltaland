using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Decisiones/Detectar Personaje")] // Lo añadimos al menú de Unity
public class DecisionDetectarPersonaje : IADecision
{
    public override bool Decidir(IAController controller)
    {
        return DetectarPersonaje(controller); // Regresamos el valor de detectar personaje
    }

    private bool DetectarPersonaje(IAController controller) // Método para detectar al personaje
    {
        Collider2D personajeDetectado = Physics2D.OverlapCircle(controller.transform.position, controller.RangoDeteccion, controller.PersonajeLayerMask); // Variable que guarda si hemos colisionado con el personaje
        if (personajeDetectado != null) // Si hemos detectado al personaje
        {
            controller.PersonajeReferencia = personajeDetectado.transform; // Definimos la referencia
            return true; // Devolvemos verdadero
        }

        // Si no lo hemos detectado
        controller.PersonajeReferencia = null; // Eliminamos la referencia
        return false; // Devolvemos falso
    }
}
