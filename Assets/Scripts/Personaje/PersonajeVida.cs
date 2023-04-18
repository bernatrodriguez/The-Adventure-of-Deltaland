using System;
using UnityEngine;

public class PersonajeVida : VidaBase // Hereda de VidaBase, por lo que tiene acceso a todo
{
    public static Action EventoPersonajeDerrotado;

    public bool Derrotado { get; private set; } // Con private nos aseguramos de que solo se va a modificar dentro de esta clase
    public bool PuedeSerCurado => Salud < saludMax; // Un bool que nos indica que si la salud es menor a la máxima (si nos falta vida), podemos ser curados

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
            RecibirDaño(10); // Recibimos daño
        }

        if (Input.GetKeyDown(KeyCode.Y)) // Si apretamos la tecla Y
        {
            RestaurarSalud(10); // Restauramos la salud
        }
    }

    public void RestaurarSalud(float cantidad)
    {
        if (Derrotado) // Si el personaje está derrotado
        {
            return; // Paramos la ejecución del código
        }

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
        _boxCollider2d.enabled = false; // Desactivamos el collider
        Derrotado = true;
        EventoPersonajeDerrotado?.Invoke(); // Si el evento no es nulo, es decir, alguna clase lo está escuchando, lo invocamos
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
