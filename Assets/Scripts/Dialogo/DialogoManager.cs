using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogoManager : Singleton<DialogoManager> // Singleton para poder llamar esta clase en otras
{
    // Referencias al panel del diálogo
    [SerializeField] private GameObject panelDialogo;
    [SerializeField] private Image npcIcono;
    [SerializeField] private TextMeshProUGUI npcNombreTMP;
    [SerializeField] private TextMeshProUGUI npcConversacionTMP;

    public NPCInteraccion NPCDisponible { get; set; } // Propiedad que indica si el NPC está disponible para cargar su información en el panel de diálogo

    private Queue<string> dialogosSecuencia; // Variable que almacena la secuencia de diálogo
    private bool dialogoAnimado; // Variable para animar el texto del diálogo
    private bool despedidaMostrada; // Variable que indica si se ha mostrado la despedida

    private void Start()
    {
        dialogosSecuencia = new Queue<string>(); // Inicializamos la secuencia de diálogo
    }

    private void Update()
    {
        if (NPCDisponible == null) // Si no hay un NPC para cargar
        {
            return; // Paramos la ejecución
        }

        // Si lo hay
        if (Input.GetKeyDown(KeyCode.E)) // Si pulsamos la tecla E
        {
            ConfigurarPanel(NPCDisponible.Dialogo); // Llamamos al diálogo
        }

        if (Input.GetKeyDown(KeyCode.Space)) // Si pulsamos la tecla espacio
        {
            if (despedidaMostrada) // Si ya hemos mostrado la despedida
            {
                AbrirCerrarPanelDialogo(false); // Cerramos el panel de diálogo
                despedidaMostrada = false; // Restauramos la variable de despedidaMostrada
                return; // Terminamos la ejecución del método
            }

            if (NPCDisponible.Dialogo.ContieneInteraccionExtra) // Si el NPC contiene alguna interacción extra
            {
                UIManager.Instance.AbrirPanelInteraccion(NPCDisponible.Dialogo.InteraccionExtra); // Llamamos al UI Manager para mostrarnos el panel correspondiente a su interacción
                AbrirCerrarPanelDialogo(false); // Cerramos el panel de diálogo
                return; // Terminamos la ejecución para no continuar con el diálogo
            }

            if (dialogoAnimado) // Si ya hemos animado el dialogo
            {
                ContinuarDialogo(); // Lo continuamos
            }
        }
    }

    public void AbrirCerrarPanelDialogo(bool estado) // Método para abrir y cerrar el panel de diálogo según el parámetro que pasemos al método
    {
        panelDialogo.SetActive(estado); // Lo activamos o desactivamos en función del estado del método
    }

    private void ConfigurarPanel(NPCDialogo npcDialogo) // Método para modificar el panel de diálogo en función del NPC con el que hablamos
    {
        AbrirCerrarPanelDialogo(true); // Abrimos el panel de diálogo
        CargarDialogosSecuencia(npcDialogo); // Cargamos la secuencia de diálogo del NPC en cuestión
        
        npcIcono.sprite = npcDialogo.Icono; // Actualizamos el icono
        npcNombreTMP.text = $"{npcDialogo.Nombre}:"; // Actualizamos el nombre

        MostrarTextoConAnimacion(npcDialogo.Saludo); // Animamos el saludo
    }

    private void CargarDialogosSecuencia(NPCDialogo npcDialogo)
    {
        if (npcDialogo.Conversacion == null || npcDialogo.Conversacion.Length <= 0) // Si no hay una conversación que cargar
        {
            return; // Detenemos la ejecución;
        }

        // Si tenemos conversación que cargar
        for (int i = 0; i < npcDialogo.Conversacion.Length; i++) // Recorremos el diálogo
        {
            dialogosSecuencia.Enqueue(npcDialogo.Conversacion[i].Oracion); // Mostramos una oración tras otra
        }
    }

    private void ContinuarDialogo() // Método para continuar el diálogo una vez estamos dentro
    {
        if (NPCDisponible == null) // Si no hay un NPC disponible
        {
            return; // Terminamos la ejecución
        }

        if (despedidaMostrada) // Si la despedida se ha mostrado
        {
            return; // Terminamos la ejecución
        }

        if (dialogosSecuencia.Count == 0) // Si no hay más oraciones que mostrar
        {
            string despedida = NPCDisponible.Dialogo.Despedida; // Definimos la despedida del NPC en una variable
            MostrarTextoConAnimacion(despedida); // Mostramos la despedida animada
            despedidaMostrada = true; // Ya se ha mostrado la despedida
            return; // Terminamos la ejecución
        }

        string siguienteDialogo = dialogosSecuencia.Dequeue(); // Guardamos en una variable la siguiente oración de la secuencia de diálogo
        MostrarTextoConAnimacion(siguienteDialogo); // Lo mostramos animado
    }

    private IEnumerator AnimarTexto(string oracion) // Método para animar el texto de diálogo
    {
        dialogoAnimado = false;
        npcConversacionTMP.text = ""; // Comenzamos vaciando el texto
        char[] letras = oracion.ToCharArray(); // Ponemos todos los caracteres de la oración en un array
        
        for (int i = 0; i < letras.Length; i++) // Recorremos letra por letra
        {
            npcConversacionTMP.text += letras[i]; // Y vamos sumándolas a la conversación
            yield return new WaitForSeconds(0.05f); // Esperando un pequeño tiempo entre ellas
        }

        dialogoAnimado = true;
    }

    private void MostrarTextoConAnimacion(string oracion) // Mostramos texto animado de la oración que le pasemos
    {
        StartCoroutine(AnimarTexto(oracion));
    }
}
