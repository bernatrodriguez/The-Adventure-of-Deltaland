using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraccion : MonoBehaviour
{
    [SerializeField] private GameObject npcButtonInteractuar; // Bot�n que nos permite interactuar con el NPC
    [SerializeField] private NPCDialogo npcDialogo; // Referencia al NPC del ScriptableObject

    public NPCDialogo Dialogo => npcDialogo; // Propiedad que nos regresa el NPC

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Si estamos colisionando con el player
        {
            DialogoManager.Instance.NPCDisponible = this; // Definiremos este NPC como disponible para cargar su informaci�n en el panel de di�logo
            npcButtonInteractuar.SetActive(true); // Activamos el bot�n de interactuar
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Si estamos colisionando con el player
        {
            DialogoManager.Instance.NPCDisponible = null; // Definiremos que ya no hay un NPC disponible para di�logo
            npcButtonInteractuar.SetActive(false); // Activamos el bot�n de interactuar
        }
    }
}
