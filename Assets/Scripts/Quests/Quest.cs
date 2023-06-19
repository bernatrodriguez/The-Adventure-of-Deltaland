using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu] // Añadimos el ScriptableObject al menú de Unity para poder crear nuestras quests
public class Quest : ScriptableObject
{
    public static Action<Quest> EventoQuestCompletado; // Evento que indica que el quest ha sido completado

    [Header("Info")]
    public string Nombre;
    public string ID;
    public int CantidadObjetivo;

    [Header("Descripción")]
    [TextArea] public string Descripcion;

    [Header("Recompensas")]
    public int RecompensaOro; // Oro
    public int RecompensaExp; // Experiencia
    public QuestRecompensaItem RecompensaItem; // Item recompensado

    [HideInInspector] public int CantidadActual; // Variable de cantidad actual, va cambiando y por ello la ocultamos del inspector
    [HideInInspector] public bool QuestCompletadoCheck; // Variable que verifica el quest completado

    public void AñadirProgreso(int cantidad) // Progreso de las quest
    {
        CantidadActual += cantidad; // Cada vez que llamamos al método, aumentamos la cantidad de progreso de la quest
        VerificarQuestCompletado(); // Verificamos si el quest se ha completado
    }

    private void VerificarQuestCompletado()
    {
        if (CantidadActual >= CantidadObjetivo) // Si la cantidad actual es mayor o igual a la cantidad objetivo
        {
            CantidadActual = CantidadObjetivo; // Establecemos la cantidad máxima a la cantidad actual
            QuestCompletado();
        }
    }
    private void QuestCompletado()
    {
        if (QuestCompletadoCheck) // Si se ha completado el quest
        {
            return; // Terminamos la ejecución
        }

        // Pero si no ha sido completado
        QuestCompletadoCheck = true; // Marcamos el quest como completado
        EventoQuestCompletado?.Invoke(this); // Si no es nulo, lanzamos el evento con la referencia de este quest
    }

    private void OnEnable()
    {
        QuestCompletadoCheck = false;
        CantidadActual = 0;
    }
}

[Serializable] // Mostramos en el inspector
public class QuestRecompensaItem
{
    public InventarioItem Item; // Referencia al item
    public int Cantidad; // Cantidad del item
}