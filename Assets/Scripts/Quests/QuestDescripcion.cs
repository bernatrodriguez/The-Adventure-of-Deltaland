using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestDescripcion : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questNombre; // Nombre de la quest
    [SerializeField] private TextMeshProUGUI questDescripcion; // Descripción de la quest

    public Quest QuestPorCompletar { get; set; }

    // Ya que tenemos dos paneles de quest en el juego (uno en el NPC y otro en el player) debemos sincronizar estos paneles
    public virtual void ConfigurarQuestUI(Quest quest)
    {
        QuestPorCompletar = quest;
        questNombre.text = quest.Nombre;
        questDescripcion.text = quest.Descripcion;
    }
}
