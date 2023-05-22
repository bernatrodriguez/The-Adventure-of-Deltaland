using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeExperiencia : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private PersonajeStats stats; // Referenciamos las stats de nuestro ScriptableObject

    [Header("Config")]
    [SerializeField] private int nivelMax; // Nivel de experiencia m�ximo al que puede llegar nuestro personaje
    [SerializeField] private int expBase; // Cu�nta experiencia necesitamos para llegar a un nuevo nivel
    // Por cada nivel que subamos el personaje deberemos incrementar la experiencia base para que subir de nivel sea cada vez m�s costoso
    [SerializeField] private int valorIncremental; // Valor de incremento de nivel

    // Variables para controlar la experiencia
    private float expActual;
    private float expActualTemp;
    private float expRequeridaSiguienteNivel;

    // Valores de inicio
    void Start()
    {
        stats.Nivel = 1;
        expRequeridaSiguienteNivel = expBase;
        stats.ExpRequeridaSiguienteNivel = expRequeridaSiguienteNivel; // Actualizamos el valor del ScriptableObject
        ActualizarBarraExp(); // Actualizamos la barra de experiencia
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            A�adirExperiencia(2f);
        }
    }

    public void A�adirExperiencia(float expObtenida)
    {
        if (expObtenida > 0f) // Si la experiencia obtenida es mayor a 0
        {
            float expRestanteNuevoNivel = expRequeridaSiguienteNivel - expActualTemp; // Valor que indica cu�nto nos falta para llegar al siguiente nivel
            if (expObtenida >= expRestanteNuevoNivel) // Si la experiencia obtenida es mayor o igual a lo que nos falta por llegar al siguiente nivel
            {
                expObtenida -= expRestanteNuevoNivel;
                expActual += expObtenida; // Acumulamos la experiencia obtenida en cada iteraci�n
                ActualizarNivel(); // Actualizamos el nivel
                A�adirExperiencia(expObtenida); // Volvemos a llamar al m�todo (recursi�n) para sumar experiencia restante tras haber subido de nivel
                // Esto es un elemento imprescindible ya que de no ser as�, subir�amos de nivel y perder�amos el resto de experiencia
                // Ejemplo: si un enemigo nos da 10 XP y el nivel lo subimos con 2 XP, lo subimos y nos quedan 8 XP para el sigueinte nivel, no lo perdemos gracias a la recursi�n
            }
            else // Si no
            {
                expActual += expObtenida; // Acumulamos la experiencia obtenida en cada iteraci�n
                expActualTemp += expObtenida;
                if (expActualTemp == expRequeridaSiguienteNivel)
                {
                    ActualizarNivel(); // Actualizamos el nivel
                }
            }
        }

        stats.ExpActual = expActual; // Actualizamos el stat del ScriptableObject
        ActualizarBarraExp(); // Actualizamos la barra de experiencia
    }

    private void ActualizarNivel()
    {
        if (stats.Nivel < nivelMax) // Si no estamos en nivel m�ximo
        {
            stats.Nivel++; // Subimos de nivel
            expActualTemp = 0f;
            expRequeridaSiguienteNivel *= valorIncremental; // Incrementamos la experiencia requerida para el siguiente nivel
            stats.ExpRequeridaSiguienteNivel = expRequeridaSiguienteNivel; // Actualizamos el stat del ScriptableObject
            stats.PuntosDisponibles += 3; // Sumamos 3 puntos de atributo
        }
    }

    private void ActualizarBarraExp() // Enviamos los valores de experiencia al UIManager para mostrarlos en la barra
    {
        UIManager.Instance.ActualizarExpPersonaje(expActualTemp, expRequeridaSiguienteNivel);
    }
}
