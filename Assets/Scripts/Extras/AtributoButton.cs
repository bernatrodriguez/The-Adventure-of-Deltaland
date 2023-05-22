using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum TipoAtributo
{
    Fuerza,
    Inteligencia,
    Destreza
}

public class AtributoButton : MonoBehaviour
{
    public static Action<TipoAtributo> EventoAgregarAtributo; // Cuando pulsamos el botón de añadir atributo lanzamos este evento
    [SerializeField] private TipoAtributo tipo;

    public void AgregarAtributo()
    {
        EventoAgregarAtributo?.Invoke(tipo); // Si el evento no es nulo, lo invocamos
    }
}
