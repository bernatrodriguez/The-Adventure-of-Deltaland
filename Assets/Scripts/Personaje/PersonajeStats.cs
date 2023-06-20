using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats")] // A�adimos el ScriptableObject al men� de Unity con el nombre Stats
public class PersonajeStats : ScriptableObject // Este tipo de clase nos permite almacenar datos y guardarlos aunque dejemos de jugar
{
    [Header("Stats")]
    public float Da�o = 5f;
    public float Defensa = 2f;
    public float Velocidad = 5f;
    public float Nivel;
    public float ExpActual;
    public float ExpRequeridaSiguienteNivel;
    [Range(0f, 100f)] public float PorcentajeCritico; // Se mueve en el rango de 0 a 100, es un porcentaje
    [Range(0f, 100f)] public float PorcentajeBloqueo; // Se mueve en el rango de 0 a 100, es un porcentaje

    [Header("Atributos")]
    public int Fuerza;
    public int Inteligencia;
    public int Destreza;

    [HideInInspector] public int PuntosDisponibles; // Oculto en el inspector

    public void A�adirBonusPorAtributoFuerza() // Valores que aumentan cuando sumamos un punto del atributo Fuerza
    {
        Da�o += 2f;
        Defensa += 1f;
        PorcentajeBloqueo += 0.03f;
    }

    public void A�adirBonusPorAtributoInteligencia() // Valores que aumentan cuando sumamos un punto del atributo Inteligencia
    {
        Da�o += 3f;
        PorcentajeCritico += 0.2f;
    }

    public void A�adirBonusPorAtributoDestreza() // Valores que aumentan cuando sumamos un punto del atributo Destreza
    {
        Velocidad += 0.1f;
        PorcentajeBloqueo += 0.05f;
    }

    public void A�adirBonusPorArma(Arma arma) // Le pasamos el par�metro del arma
    {
        Da�o += arma.Da�o; // Sumamos el da�o
        PorcentajeCritico += arma.ChanceCritico; // Sumamos la probabilidad de cr�tico
        PorcentajeBloqueo += arma.ChanceBloqueo; // Sumamos la probabilidad de bloqueo
    }

    public void EliminarBonusPorArma(Arma arma) // Le pasamos el par�metro del arma
    {
        Da�o -= arma.Da�o; // Eliminamos el da�o
        PorcentajeCritico -= arma.ChanceCritico; // Restamos la probabilidad de cr�tico
        PorcentajeBloqueo -= arma.ChanceBloqueo; // Restamos la probabilidad de bloqueo
    }

    public void ResetearValores()
    {
        Da�o = 5f;
        Defensa = 2f;
        Velocidad = 5f;
        Nivel = 1;
        ExpActual = 0f;
        ExpRequeridaSiguienteNivel = 0f;
        PorcentajeBloqueo = 0f;
        PorcentajeCritico = 0f;

        Fuerza = 0;
        Inteligencia = 0;
        Destreza = 0;

        PuntosDisponibles = 0;
    }
}
