using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogoManager : Singleton<DialogoManager> // Singleton para poder llamar esta clase en otras
{
    // Referencias al panel del di�logo
    [SerializeField] private GameObject panelDialogo;
    [SerializeField] private Image npcIcono;
    [SerializeField] private TextMeshProUGUI npcNombreTMP;
    [SerializeField] private TextMeshProUGUI npcConversacionTMP;

    public NPCInteraccion NPCDisponible { get; set; } // Propiedad que indica si el NPC est� disponible para cargar su informaci�n en el panel de di�logo

    private Queue<string> dialogosSecuencia; // Variable que almacena la secuencia de di�logo
    private bool dialogoAnimado; // Variable para animar el texto del di�logo
    private bool despedidaMostrada; // Variable que indica si se ha mostrado la despedida

    private void Start()
    {
        dialogosSecuencia = new Queue<string>(); // Inicializamos la secuencia de di�logo
    }

    private void Update()
    {
        if (NPCDisponible == null) // Si no hay un NPC para cargar
        {
            return; // Paramos la ejecuci�n
        }

        // Si lo hay
        if (Input.GetKeyDown(KeyCode.E)) // Si pulsamos la tecla E
        {
            ConfigurarPanel(NPCDisponible.Dialogo); // Llamamos al di�logo
        }

        if (Input.GetKeyDown(KeyCode.Space)) // Si pulsamos la tecla espacio
        {
            if (despedidaMostrada) // Si ya hemos mostrado la despedida
            {
                AbrirCerrarPanelDialogo(false); // Cerramos el panel de di�logo
                despedidaMostrada = false; // Restauramos la variable de despedidaMostrada
                return; // Terminamos la ejecuci�n del m�todo
            }

            if (NPCDisponible.Dialogo.ContieneInteraccionExtra) // Si el NPC contiene alguna interacci�n extra
            {
                UIManager.Instance.AbrirPanelInteraccion(NPCDisponible.Dialogo.InteraccionExtra); // Llamamos al UI Manager para mostrarnos el panel correspondiente a su interacci�n
                AbrirCerrarPanelDialogo(false); // Cerramos el panel de di�logo
                return; // Terminamos la ejecuci�n para no continuar con el di�logo
            }

            if (dialogoAnimado) // Si ya hemos animado el dialogo
            {
                ContinuarDialogo(); // Lo continuamos
            }
        }
    }

    public void AbrirCerrarPanelDialogo(bool estado) // M�todo para abrir y cerrar el panel de di�logo seg�n el par�metro que pasemos al m�todo
    {
        panelDialogo.SetActive(estado); // Lo activamos o desactivamos en funci�n del estado del m�todo
    }

    private void ConfigurarPanel(NPCDialogo npcDialogo) // M�todo para modificar el panel de di�logo en funci�n del NPC con el que hablamos
    {
        AbrirCerrarPanelDialogo(true); // Abrimos el panel de di�logo
        CargarDialogosSecuencia(npcDialogo); // Cargamos la secuencia de di�logo del NPC en cuesti�n
        
        npcIcono.sprite = npcDialogo.Icono; // Actualizamos el icono
        npcNombreTMP.text = $"{npcDialogo.Nombre}:"; // Actualizamos el nombre

        MostrarTextoConAnimacion(npcDialogo.Saludo); // Animamos el saludo
    }

    private void CargarDialogosSecuencia(NPCDialogo npcDialogo)
    {
        if (npcDialogo.Conversacion == null || npcDialogo.Conversacion.Length <= 0) // Si no hay una conversaci�n que cargar
        {
            return; // Detenemos la ejecuci�n;
        }

        // Si tenemos conversaci�n que cargar
        for (int i = 0; i < npcDialogo.Conversacion.Length; i++) // Recorremos el di�logo
        {
            dialogosSecuencia.Enqueue(npcDialogo.Conversacion[i].Oracion); // Mostramos una oraci�n tras otra
        }
    }

    private void ContinuarDialogo() // M�todo para continuar el di�logo una vez estamos dentro
    {
        if (NPCDisponible == null) // Si no hay un NPC disponible
        {
            return; // Terminamos la ejecuci�n
        }

        if (despedidaMostrada) // Si la despedida se ha mostrado
        {
            return; // Terminamos la ejecuci�n
        }

        if (dialogosSecuencia.Count == 0) // Si no hay m�s oraciones que mostrar
        {
            string despedida = NPCDisponible.Dialogo.Despedida; // Definimos la despedida del NPC en una variable
            MostrarTextoConAnimacion(despedida); // Mostramos la despedida animada
            despedidaMostrada = true; // Ya se ha mostrado la despedida
            return; // Terminamos la ejecuci�n
        }

        string siguienteDialogo = dialogosSecuencia.Dequeue(); // Guardamos en una variable la siguiente oraci�n de la secuencia de di�logo
        MostrarTextoConAnimacion(siguienteDialogo); // Lo mostramos animado
    }

    private IEnumerator AnimarTexto(string oracion) // M�todo para animar el texto de di�logo
    {
        dialogoAnimado = false;
        npcConversacionTMP.text = ""; // Comenzamos vaciando el texto
        char[] letras = oracion.ToCharArray(); // Ponemos todos los caracteres de la oraci�n en un array
        
        for (int i = 0; i < letras.Length; i++) // Recorremos letra por letra
        {
            npcConversacionTMP.text += letras[i]; // Y vamos sum�ndolas a la conversaci�n
            yield return new WaitForSeconds(0.05f); // Esperando un peque�o tiempo entre ellas
        }

        dialogoAnimado = true;
    }

    private void MostrarTextoConAnimacion(string oracion) // Mostramos texto animado de la oraci�n que le pasemos
    {
        StartCoroutine(AnimarTexto(oracion));
    }
}
