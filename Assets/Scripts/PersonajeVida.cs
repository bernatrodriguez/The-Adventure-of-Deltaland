using System;
using UnityEngine;

public class PersonajeVida : VidaBase // Hereda de VidaBase, por lo que tiene acceso a todo
{
    public static Action EventoPersonajeDerrotado;
    public bool PuedeSerCurado => Salud < saludMax; // Un bool que nos indica que si la salud es menor a la máxima (si nos falta vida), podemos ser curados

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) // Si apretamos la tecla T
        {
            RecibirDaño(10); // Recibimos daño
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
                Salud = saludMax; // Si la salud restaurada es mayor a la máxima, definimos la salud como la máxima (así nunca sobrepasaremos la máxima)
            }

            ActualizarBarraVida(Salud, saludMax); // Actualizamos la barra de vida con la información de este códigoS
        }
    }

    protected override void PersonajeDerrotado()
    {
        EventoPersonajeDerrotado?.Invoke(); // Si el evento no es nulo, es decir, alguna clase lo está escuchando, lo invocamos
    }

    protected override void ActualizarBarraVida(float vidaActual, float vidaMax)
    {
        
    }
}
