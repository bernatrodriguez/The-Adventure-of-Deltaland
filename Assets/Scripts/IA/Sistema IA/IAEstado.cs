using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="IA/Estado")] // Añadimos el ScriptableObject al menú de Unity para crear nuevos estados
public class IAEstado : ScriptableObject
{
    public IAAccion[] Acciones; // Array de acciones
    public IATransicion[] Transiciones; // Array de decisiones

    public void EjecutarEstado(IAController controller) // Método principal que ejecuta acciones y realiza sus transiciones
    {
        EjecutarAcciones(controller);
        RealizarTransiciones(controller);
    }

    private void EjecutarAcciones(IAController controller)
    {
        if (Acciones == null || Acciones.Length <= 0) // Si no hay acciones
        {
            return; // Terminamos la ejecución
        }
        
        // Si las hay
        for (int i = 0; i < Acciones.Length; i++) // Recorremos todas las acciones del estado
        {
            Acciones[i].Ejecutar(controller); // Y las ejecutamos
        }
    }

    private void RealizarTransiciones(IAController controller)
    {
        if (Transiciones == null || Transiciones.Length <= 0) // Si no hay transiciones
        {
            return; // Terminamos la ejecución
        }

        // Si las hay
        for (int i = 0; i < Transiciones.Length; i++) // Recorremos todas las transiciones del estado
        {
            bool decisionValor = Transiciones[i].Decision.Decidir(controller); // Obtenemos el valor de decisión de cada una de las transiciones
            if (decisionValor) // Si el valor de decisión es verdadero
            {
                controller.CambiarEstado(Transiciones[i].EstadoVerdadero); // Hacemos una transición al estado verdadero
            }
            else
            {
                controller.CambiarEstado(Transiciones[i].EstadoFalso); // Hacemos una transición al estado falso
            }
        }
    }
}
