using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InspectorQuestDescripcion : QuestDescripcion // Hereda de la clase base QuestDescripcion
{
    [SerializeField] private TextMeshProUGUI questRecompensa; // Referenci al texto de la recompensa

    public override void ConfigurarQuestUI(Quest quest)
    {
        base.ConfigurarQuestUI(quest);
        questRecompensa.text = $"-{quest.RecompensaOro} oro" + // Escribimos la recompensa de oro
            $"\n-{quest.RecompensaExp} exp" + // Escribimos la recompensa de experiencia
            $"\n-{quest.RecompensaItem.Item.Nombre} x{quest.RecompensaItem.Cantidad}"; // Escribimos la recompensa de item poniendo el nombre del item y su cantidad
    }

    public void AceptarQuest() // M�todo para aceptar las quest
    {
        if (QuestPorCompletar == null) // Si el quest no est� cargado
        {
            return; // Terminamos la ejecuci�n
        }

        //Si se ha cargado
        QuestManager.Instance.A�adirQuest(QuestPorCompletar); // A�adimos el quest
        gameObject.SetActive(false); // Lo eliminamos del inspector
    }
}
