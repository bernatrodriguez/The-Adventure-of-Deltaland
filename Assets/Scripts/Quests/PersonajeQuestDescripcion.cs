using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PersonajeQuestDescripcion : QuestDescripcion // Hereda de la clase base QuestDescripcion
{
    [SerializeField] private TextMeshProUGUI tareaObjetivo;
    [SerializeField] private TextMeshProUGUI recompensaOro;
    [SerializeField] private TextMeshProUGUI recompensaExp;

    [Header("Item")]
    [SerializeField] private Image recompensaItemIcono;
    [SerializeField] private TextMeshProUGUI recompensaItemcantidad;

    private void Update()
    {
        if (QuestPorCompletar.QuestCompletadoCheck) // Si el quest cargado está completado
        {
            return; // Regresamos
        }

        // Si no está completado
        tareaObjetivo.text = $"{QuestPorCompletar.CantidadActual}/{QuestPorCompletar.CantidadObjetivo}"; // Definimos la cantidad actual y la cantidad objetivo de la tarea
    }

    public override void ConfigurarQuestUI(Quest quest)
    {
        base.ConfigurarQuestUI(quest);
        recompensaOro.text = quest.RecompensaOro.ToString(); // Definimos la recompensa de oro
        recompensaExp.text = quest.RecompensaExp.ToString(); // Definimos la recompensa de experiencia
        tareaObjetivo.text = $"{quest.CantidadActual}/{quest.CantidadObjetivo}"; // Definimos la cantidad actual y la cantidad objetivo de la tarea

        recompensaItemIcono.sprite = quest.RecompensaItem.Item.Icono; // Definimos el icono del item
        recompensaItemcantidad.text = quest.RecompensaItem.Cantidad.ToString(); // Definimos la cantidad del item
    }

    private void QuestCompletadoRespuesta(Quest questCompletado)
    {
        if (questCompletado.ID == QuestPorCompletar.ID) // Si el ID del quest completado es igual al ID del quest por completar
        {
            tareaObjetivo.text = $"{QuestPorCompletar.CantidadActual}/{QuestPorCompletar.CantidadObjetivo}"; // Actualizamos la tarea objetivo
            gameObject.SetActive(false); // Quitamos la tarjeta, la misión está completada
        }
    }

    private void OnEnable()
    {
        // Para evitar errores al completar las quest mientras el panel de quests está desactivado, hacemos lo siguiente:
        if (QuestPorCompletar.QuestCompletadoCheck) // Si el quest ha sido completado
        {
            gameObject.SetActive(false); // Desactivamos la tarjeta de la quest, es decir, la borramos de la lista de quests
        }

        Quest.EventoQuestCompletado += QuestCompletadoRespuesta; // Nos suscribimos al evento
    }

    private void OnDisable()
    {
        Quest.EventoQuestCompletado -= QuestCompletadoRespuesta; // Nos desuscribimos al evento
    }
}
