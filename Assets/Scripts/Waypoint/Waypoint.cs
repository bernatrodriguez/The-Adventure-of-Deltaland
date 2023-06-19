using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private Vector3[] puntos; // Array de puntos por los que se moverá el NPC
    public Vector3[] Puntos => puntos; // Propiedad que nos regresa los puntos

    public Vector3 PosicionActual { get; set; } // Propiedad para guardar la posición del personaje y definir los puntos a partir de esta. En caso de no hacerlo, los puntos parten de la posición 0,0 de la escena
    private bool juegoIniciado;

    private void Start()
    {
        juegoIniciado = true;
        PosicionActual = transform.position; // Definimos la posición actual como la posición del personaje
    }

    public Vector3 ObtenerPosicionMovimiento(int index) // Le pasamos el valor del punto al que nos queremos mover
    {
        return PosicionActual + puntos[index]; // Regresamos la posición del punto al que nos queremos mover
    }

    private void OnDrawGizmos()
    {
        if (juegoIniciado == false && transform.hasChanged) // Si el juego no ha iniciado y la posición del personaje ha cambiado
        {
            PosicionActual = transform.position; // Definimos la posición actual como la posición del personaje. Esto nos actualiza la posición de los puntos si movemos el personaje en la escena
        }

        if (puntos == null || puntos.Length <= 0) // Si no existen puntos o la longitud de estos es 0
        {
            return; // Detenemos la ejecución
        }

        // Si no es así, recorremos todos los puntos
        for (int i = 0; i < puntos.Length; i++)
        {
            Gizmos.color = Color.blue; // Definimos el color de los puntos
            Gizmos.DrawWireSphere(puntos[i] + PosicionActual, 0.5f); // Dibujamos una esfera en la posición de cada punto y con un radio de 0.5
            if (i < puntos.Length -1) // Si no superamos los puntos del array
            {
                Gizmos.color = Color.gray; // Definimos el color de las líneas
                Gizmos.DrawLine(puntos[i] + PosicionActual, puntos[i + 1] + PosicionActual); // Dibujamos las líneas entre los puntos
            }
        }
    }
}
