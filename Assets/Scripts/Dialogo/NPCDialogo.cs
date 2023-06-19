using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteraccionExtraNPC // Interacciones extra de los NPC
{
    Quests,
    Tienda,
    Crafting
}

[CreateAssetMenu] // A�adimos el ScriptableObject al men� de Unity
public class NPCDialogo : ScriptableObject
{
    [Header("Info")]
    public string Nombre;
    public Sprite Icono;
    public bool ContieneInteraccionExtra;
    public InteraccionExtraNPC InteraccionExtra;

    [Header("Saludo")]
    [TextArea] public string Saludo; // Saludo del NPC, tiene un area de texto m�s grande

    [Header("Di�logo")]
    public DialogoTexto[] Conversacion; // Dialogo del NPC, formada por un array de oraciones

    [Header("Despedida")]
    [TextArea] public string Despedida; // Despedida del NPC, tiene un area de texto m�s grande
}

[Serializable] // Podemos verlo en el inspector
public class DialogoTexto
{
    [TextArea] public string Oracion; // Oraci�n del dialogo principal del NPC, tiene un area de texto m�s grande
}
