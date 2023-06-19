using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraccion : MonoBehaviour
{
    [SerializeField] private GameObject npcButtonInteractuar; // Botón que nos permite interactuar con el NPC
    [SerializeField] private NPCDialogo npcDialogo; // Referencia al NPC del ScriptableObject

    public NPCDialogo Dialogo => npcDialogo; // Propiedad que nos regresa el NPC

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Si estamos colisionando con el player
        {
            DialogoManager.Instance.NPCDisponible = this; // Definiremos este NPC como disponible para cargar su información en el panel de diálogo
            npcButtonInteractuar.SetActive(true); // Activamos el botón de interactuar
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Si estamos colisionando con el player
        {
            DialogoManager.Instance.NPCDisponible = null; // Definiremos que ya no hay un NPC disponible para diálogo
            npcButtonInteractuar.SetActive(false); // Activamos el botón de interactuar
        }
    }
}
