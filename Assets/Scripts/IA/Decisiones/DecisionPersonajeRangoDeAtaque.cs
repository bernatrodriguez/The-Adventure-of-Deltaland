using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Decisiones/Personaje En Rango De Ataque")] // Añadimos la decisión al menú de Unity
public class DecisionPersonajeRangoDeAtaque : IADecision
{
    public override bool Decidir(IAController controller)
    {
        return EnRangoDeAtaque(controller);
    }

    private bool EnRangoDeAtaque(IAController controller)
    {
        if (controller.PersonajeReferencia == null) // Si no tenemos una referencia del personaje
        {
            return false; // Regresamos falso
        }

        float distancia = (controller.PersonajeReferencia.position - controller.transform.position).sqrMagnitude; // Creamos una variable con la distancia hasta el personaje restando los dos vectores. Utilizamos sqrMagnitude para obtener la magnitud al cuadrado y poder compararla de forma más efectiva
        if (distancia < Mathf.Pow(controller.RangoDeAtaqueDeterminado, 2)) // Si la distancia hasta el personaje es menor al rango de ataque al cuadrado, es decir, si el personaje está en rango de ataque
        {
            return true; // Regresamos verdadero
        }

        // Si no está en rango de ataque
        return false; // Regresamos falso
    }
}
