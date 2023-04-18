using System;
using UnityEngine;

public class PersonajeVida : VidaBase // Hereda de VidaBase, por lo que tiene acceso a todo
{
    public static Action EventoPersonajeDerrotado;
    public bool PuedeSerCurado => Salud < saludMax; // Un bool que nos indica que si la salud es menor a la m�xima (si nos falta vida), podemos ser curados

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) // Si apretamos la tecla T
        {
            RecibirDa�o(10); // Recibimos da�o
        }

        if (Input.GetKeyDown(KeyCode.R)) // Si apretamos la tecla T
        {
            RestaurarSalud(10); // Restauramos la salud
        }
    }

    public void RestaurarSalud(float cantidad)
    {
        if (PuedeSerCurado) // Si podemos ser curados
        {
            Salud += cantidad; // (Salud = Salud + cantidad) Restauramos la salud
            if (Salud > saludMax)
            {
                Salud = saludMax; // Si la salud restaurada es mayor a la m�xima, definimos la salud como la m�xima (as� nunca sobrepasaremos la m�xima)
            }

            ActualizarBarraVida(Salud, saludMax); // Actualizamos la barra de vida con la informaci�n de este c�digoS
        }
    }

    protected override void PersonajeDerrotado()
    {
        EventoPersonajeDerrotado?.Invoke(); // Si el evento no es nulo, es decir, alguna clase lo est� escuchando, lo invocamos
    }

    protected override void ActualizarBarraVida(float vidaActual, float vidaMax)
    {
        
    }
}
