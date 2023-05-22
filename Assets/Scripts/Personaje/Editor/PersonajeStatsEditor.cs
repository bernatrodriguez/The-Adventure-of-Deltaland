using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PersonajeStats))] // Esta clase va a editar el tipo PersonajeStats
public class PersonajeStatsEditor : Editor
{
    public PersonajeStats StatsTarget => target as PersonajeStats; // Definimos el objetivo del editor

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Restaurar valores")) // Creamos un botón para restaurar valores
        {
            StatsTarget.ResetearValores();
        }
    }
}
