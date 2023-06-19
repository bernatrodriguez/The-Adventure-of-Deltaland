using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestManager : Singleton<QuestManager>
{
    [Header("Personaje")]
    [SerializeField] private Personaje personaje; // Referencia de nuestro personaje
    
    [Header("Quests")]
    [SerializeField] private Quest[] questDisponibles; // Array de quests disponibles

    [Header("Inspector Quests")]
    [SerializeField] private InspectorQuestDescripcion inspectorQuestPrefab; // Referencia del prefab de inspectorQuest
    [SerializeField] private Transform inspectorQuestContenedor; // Contenedor de quest

    [Header("Personaje Quests")]
    [SerializeField] private PersonajeQuestDescripcion personajeQuestPrefab; // Referencia del prefab de personajeQuest
    [SerializeField] private Transform personajeQuestContenedor; // Contenedor de quest

    [Header("Panel Quest Completada")]
    [SerializeField] private GameObject panelQuestCompletado; // Referencia al panel de quest completada
    [SerializeField] private TextMeshProUGUI questNombre; // Referencia al nombre de la quest completada
    [SerializeField] private TextMeshProUGUI questRecompensaOro; // Referencia a la recompensa de oro
    [SerializeField] private TextMeshProUGUI questRecompensaExp; // Referencia a la recompensa de experiencia
    [SerializeField] private TextMeshProUGUI questRecompensaItemCantidad; // Referencia a la cantidad del item de recompensa
    [SerializeField] private Image questRecompensaItemIcono; // Referencia a la imagen del item

    public Quest QuestPorReclamar { get; private set; } // Propiedad con acceso privado, solo lo establecemos dentro de esta clase

    private void Start()
    {
        CargarQuestEnInspector();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            AñadirProgreso("Mata10", 1);
            AñadirProgreso("Mata25", 1);
            AñadirProgreso("Mata50", 1);
        }
    }

    private void CargarQuestEnInspector()
    {
        for (int i = 0; i < questDisponibles.Length; i++) // Recorremos todos los quest disponibles
        {
            InspectorQuestDescripcion nuevoQuest = Instantiate(inspectorQuestPrefab, inspectorQuestContenedor); // Instanciamos el prefab
            nuevoQuest.ConfigurarQuestUI(questDisponibles[i]); // Configuramos todas las tarjetas de quests
        }
    }

    private void AñadirQuestPorCompletar(Quest questPorCompletar)
    {
        PersonajeQuestDescripcion nuevoQuest = Instantiate(personajeQuestPrefab, personajeQuestContenedor); // Instanciamos el prefab
        nuevoQuest.ConfigurarQuestUI(questPorCompletar); // Configuramos las tarjetas
    }

    public void AñadirQuest(Quest questPorCompletar)
    {
        AñadirQuestPorCompletar(questPorCompletar);
    }

    public void ReclamarRecompensa() // Método al pulsar el botón de reclamar recompensa
    {
        if (QuestPorReclamar == null) // Si no hay ninguna quest por reclamar
        {
            return; // Detenemos al ejecución
        }

        // Si la hay, pasamos los valores a sus respectivas clases
        MonedasManager.Instance.AñadirMonedas(QuestPorReclamar.RecompensaOro);
        personaje.PersonajeExperiencia.AñadirExperiencia(QuestPorReclamar.RecompensaExp);
        Inventario.Instance.AñadirItem(QuestPorReclamar.RecompensaItem.Item, QuestPorReclamar.RecompensaItem.Cantidad);

        panelQuestCompletado.SetActive(false); // Desactivamos el panel de quest completado
        QuestPorReclamar = null; // Ya no hay quest por reclamar
    }

    public void AñadirProgreso(string questID, int cantidad)
    {
        Quest questPorActualizar = QuestExiste(questID); // Si la referencia existe, la guardamos en la variable
        questPorActualizar.AñadirProgreso(cantidad); // Aplicamos la cantidad de progreso
    }

    private Quest QuestExiste(string questID)
    {
        for (int i = 0; i < questDisponibles.Length; i++) // Recorremos el array de quests
        {
            if (questDisponibles[i].ID == questID) // Si encontramos una quest con el mismo identificador que buscamos
            {
                return questDisponibles[i]; // Regresamos la referencia del quest
            }
        }

        // Si no encontramos una quest con el mismo identificador
        return null; // No regresamos ningun valor
    }

    private void MostrarQuestCompletado(Quest questCompletado)
    {
        panelQuestCompletado.SetActive(true); // Activamos el panel de quest completada
        
        // Actualizamos los elementos del panel
        questNombre.text = questCompletado.Nombre;
        questRecompensaOro.text = questCompletado.RecompensaOro.ToString();
        questRecompensaExp.text = questCompletado.RecompensaExp.ToString();
        questRecompensaItemCantidad.text = questCompletado.RecompensaItem.Cantidad.ToString();
        questRecompensaItemIcono.sprite = questCompletado.RecompensaItem.Item.Icono;
    }

    private void QuestCompletadoRespuesta(Quest questCompletado)
    {
        QuestPorReclamar = QuestExiste(questCompletado.ID); // Guardamos el ID del quest completado en una variable
        if (QuestPorReclamar != null) // Si existe una quest por reclamar
        {
            MostrarQuestCompletado(QuestPorReclamar); // Mostramos el panel de quest completado con la quest por reclamar
        }
    }
    
    private void OnEnable()
    {
        Quest.EventoQuestCompletado += QuestCompletadoRespuesta; // Nos suscribimos al evento
    }

    private void OnDisable()
    {
        Quest.EventoQuestCompletado -= QuestCompletadoRespuesta; // Nos desuscribimos al evento
    }
}
