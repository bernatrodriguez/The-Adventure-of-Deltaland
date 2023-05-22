using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : MonoBehaviour
{
    [SerializeField] private PersonajeStats stats; // Referenciamos los stats

    public PersonajeVida PersonajeVida { get; private set; }
    public PersonajeAnimaciones PersonajeAnimaciones { get; private set; }
    public PersonajeMana PersonajeMana { get; private set; }

    private void Awake()
    {
        PersonajeVida = GetComponent<PersonajeVida>(); // Referenciamos la clase PersonajeVida
        PersonajeAnimaciones = GetComponent<PersonajeAnimaciones>(); // Referenciamos la clase PersonajeAnimaciones
        PersonajeMana = GetComponent<PersonajeMana>(); // Referenciamos la clase PersonajeMana
    }

    public void RestaurarPersonaje()
    {
        PersonajeVida.RestaurarPersonaje(); // Restauramos la vida
        PersonajeAnimaciones.RevivirPersonaje(); // Restauramos las animaciones
        PersonajeMana.RestablecerMana(); // Restablecemos el man�
    }

    private void AtributoRespuesta(TipoAtributo tipo)
    {
        if (stats.PuntosDisponibles <= 0) // Si no tenemos puntos
        {
            return; // Terminamos la ejecuci�n, no llamamos al c�digo de abajo
        }

        switch (tipo) // Escuchamos qu� tipo de atributo se trata de aumentar
        {
            case TipoAtributo.Fuerza: // Si estamos tratando de aumentar el atributo de Fuerza
                stats.Fuerza++; // Sumamos 1 al atributo (para poderlo mostrar en el panel de stats)
                stats.A�adirBonusPorAtributoFuerza(); // Llamamos al m�todo encargado de sumarnos las estad�sticas correspondientes
                break;
            case TipoAtributo.Inteligencia: // Si estamos tratando de aumentar el atributo de Inteligencia
                stats.Inteligencia++; // Sumamos 1 al atributo (para poderlo mostrar en el panel de stats)
                stats.A�adirBonusPorAtributoInteligencia(); // Llamamos al m�todo encargado de sumarnos las estad�sticas correspondientes
                break;
            case TipoAtributo.Destreza: // Si estamos tratando de aumentar el atributo de Destreza
                stats.Destreza++; // Sumamos 1 al atributo (para poderlo mostrar en el panel de stats)
                stats.A�adirBonusPorAtributoDestreza(); // Llamamos al m�todo encargado de sumarnos las estad�sticas correspondientes
                break;
        }

        stats.PuntosDisponibles -= 1;
    }
    
    private void OnEnable() // Escuchamos el evento de agregar atributo para llamar al m�todo
    {
        AtributoButton.EventoAgregarAtributo += AtributoRespuesta;
    }

    private void OnDisable()
    {
        AtributoButton.EventoAgregarAtributo -= AtributoRespuesta;
    }
}
