using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private Vector3[] puntos; // Array de puntos por los que se mover� el NPC
    public Vector3[] Puntos => puntos; // Propiedad que nos regresa los puntos

    public Vector3 PosicionActual { get; set; } // Propiedad para guardar la posici�n del personaje y definir los puntos a partir de esta. En caso de no hacerlo, los puntos parten de la posici�n 0,0 de la escena
    private bool juegoIniciado;

    private void Start()
    {
        juegoIniciado = true;
        PosicionActual = transform.position; // Definimos la posici�n actual como la posici�n del personaje
    }

    public Vector3 ObtenerPosicionMovimiento(int index) // Le pasamos el valor del punto al que nos queremos mover
    {
        return PosicionActual + puntos[index]; // Regresamos la posici�n del punto al que nos queremos mover
    }

    private void OnDrawGizmos()
    {
        if (juegoIniciado == false && transform.hasChanged) // Si el juego no ha iniciado y la posici�n del personaje ha cambiado
        {
            PosicionActual = transform.position; // Definimos la posici�n actual como la posici�n del personaje. Esto nos actualiza la posici�n de los puntos si movemos el personaje en la escena
        }

        if (puntos == null || puntos.Length <= 0) // Si no existen puntos o la longitud de estos es 0
        {
            return; // Detenemos la ejecuci�n
        }

        // Si no es as�, recorremos todos los puntos
        for (int i = 0; i < puntos.Length; i++)
        {
            Gizmos.color = Color.blue; // Definimos el color de los puntos
            Gizmos.DrawWireSphere(puntos[i] + PosicionActual, 0.5f); // Dibujamos una esfera en la posici�n de cada punto y con un radio de 0.5
            if (i < puntos.Length -1) // Si no superamos los puntos del array
            {
                Gizmos.color = Color.gray; // Definimos el color de las l�neas
                Gizmos.DrawLine(puntos[i] + PosicionActual, puntos[i + 1] + PosicionActual); // Dibujamos las l�neas entre los puntos
            }
        }
    }
}
