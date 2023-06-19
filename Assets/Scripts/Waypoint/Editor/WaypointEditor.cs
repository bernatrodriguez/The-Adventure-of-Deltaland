using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// Con esta clase creamos un Handle (un pequeño botón) sobre cada Waypoint para poderlo mover libremente con el ratón

[CustomEditor(typeof(Waypoint))] // Tipo de editor de waypoint
public class WaypointEditor : Editor
{
    Waypoint WaypointTarget => target as Waypoint; // Definimos el objetivo del editor, accedemos a todo lo que se encuentre en la clase Waypoint

    private void OnSceneGUI()
    {
        Handles.color = Color.red; // Definimos el color
        if (WaypointTarget.Puntos == null) // Si no tenemos puntos con los que trabajar
        {
            return; // Detenemos la ejecución
        }

        // Si los tenemos
        for (int i = 0; i < WaypointTarget.Puntos.Length; i++) // Recorremos todos los puntos de la clase Waypoint
        {
            EditorGUI.BeginChangeCheck(); // Verificamos los cambios del editor para actualizar la posición de los puntos
            
            Vector3 puntoActual = WaypointTarget.PosicionActual + WaypointTarget.Puntos[i]; // Obtenemos la posición actual de cada uno de los puntos de la escena
            Vector3 nuevoPunto = Handles.FreeMoveHandle(puntoActual, Quaternion.identity, 0.7f, new Vector3(0.3f, 0.3f, 0.3f), Handles.SphereHandleCap); // Guardamos la posición de cada punto que controlemos con Handle

            // Creamos un texto debajo de cada punto
            GUIStyle texto = new GUIStyle();
            texto.fontStyle = FontStyle.Bold;
            texto.fontSize = 16;
            texto.normal.textColor = Color.black;
            Vector3 alineamiento = Vector3.down * 0.3f + Vector3.right * 0.3f; // Posicionamos el texto
            Handles.Label(WaypointTarget.PosicionActual + WaypointTarget.Puntos[i] + alineamiento, $"{i + 1}", texto); // Escribimos el texto

            if (EditorGUI.EndChangeCheck()) // Si se han hecho cambios en el editor, es decir, si se han movido puntos
            {
                Undo.RecordObject(target, "Free Move Handle"); // Los guardamos
                WaypointTarget.Puntos[i] = nuevoPunto - WaypointTarget.PosicionActual;
            }
        }
    }
}
