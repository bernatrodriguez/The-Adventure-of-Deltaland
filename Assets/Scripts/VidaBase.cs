using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaBase : MonoBehaviour
{
    [SerializeField] protected float saludInicial;
    [SerializeField] protected float saludMax;

    public float Salud { get; protected set; } // Propiedad de tipo float que puede ser tanto regresada (get) como modificada (set protected -> solo en la clase vidabase), almacena los datos de la salud

    // Start is called before the first frame update
    void Start()
    {
        Salud = saludInicial;
    }

    public void RecibirDaño(float cantidad) // Solo recibiremos el parámetro cantidad
    {
        if (cantidad <= 0) // Si la cantidad es menor o igual que 0
        {
            return; // No continuamos con la ejecución del código
        }

        if (Salud > 0f) // Si la cantidad es mayor a 0
        {
            Salud -= cantidad; // (Salud = Salud - cantidad) Restamos la cantidad a nuestra salud
            ActualizarBarraVida(Salud, saludMax); // Llamamos a la función ActualizarBarraVida con la información de la salud actual obtenida en este código y la salud máxima

            if (Salud <= 0f) // Si la salud es 0, es decir, si es personaje ha sido derrotado
            {
                ActualizarBarraVida(Salud, saludMax); // Actualizamos de nuevo la barra
                PersonajeDerrotado(); // Llamamos a la función de personaje derrotado
            }
        }
    }

    protected virtual void ActualizarBarraVida(float vidaActual, float vidaMax) // Actualizar barra de vida
    {

    }

    protected virtual void PersonajeDerrotado()
    {

    }
}
