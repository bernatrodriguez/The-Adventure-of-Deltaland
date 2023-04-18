using System;
using UnityEngine;

public class PersonajeVida : VidaBase // Hereda de VidaBase, por lo que tiene acceso a todo
{
    public static Action EventoPersonajeDerrotado;

    public bool Derrotado { get; private set; } // Con private nos aseguramos de que solo se va a modificar dentro de esta clase
    public bool PuedeSerCurado => Salud < saludMax; // Un bool que nos indica que si la salud es menor a la m�xima (si nos falta vida), podemos ser curados

    protected override void Start()
    {
        base.Start(); // Llamamos al start de la clase base, es decir, de VidaBase
        ActualizarBarraVida(Salud, saludMax);
    }

    private BoxCollider2D _boxCollider2d;

    private void Awake()
    {
        _boxCollider2d = GetComponent<BoxCollider2D>(); // Referenciamos el collider
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) // Si apretamos la tecla T
        {
            RecibirDa�o(10); // Recibimos da�o
        }

        if (Input.GetKeyDown(KeyCode.Y)) // Si apretamos la tecla Y
        {
            RestaurarSalud(10); // Restauramos la salud
        }
    }

    public void RestaurarSalud(float cantidad)
    {
        if (Derrotado) // Si el personaje est� derrotado
        {
            return; // Paramos la ejecuci�n del c�digo
        }

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
        _boxCollider2d.enabled = false; // Desactivamos el collider
        Derrotado = true;
        EventoPersonajeDerrotado?.Invoke(); // Si el evento no es nulo, es decir, alguna clase lo est� escuchando, lo invocamos
    }

    public void RestaurarPersonaje()
    {
        _boxCollider2d.enabled = true; // Activamos el collider
        Derrotado = false;
        Salud = saludInicial; // Restrauramos la salud inicial
        ActualizarBarraVida(Salud, saludInicial);
    }

    protected override void ActualizarBarraVida(float vidaActual, float vidaMax)
    {
        UIManager.Instance.ActualizarVidaPersonaje(vidaActual, vidaMax);
    }
}
