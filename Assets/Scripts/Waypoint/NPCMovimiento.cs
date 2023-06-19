using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovimiento : WaypointMovimiento // Hereda de la clase WaypointMovimiento
{
    [SerializeField] private DireccionMovimiento direccion; // Direcci�n del personaje

    private readonly int caminarAbajo = Animator.StringToHash("CaminarAbajo"); // Creamos una variable con el par�metro del animator

    protected override void RotarPersonaje()
    {
        if (direccion != DireccionMovimiento.Horizontal) // Si la direcci�n no es horizontal
        {
            return; // Detenemos la ejecuci�n
        }

        // Si es horizontal
        if (PuntoPorMoverse.x > ultimaPosicion.x) // Si vamos hacia la derecha
        {
            transform.localScale = new Vector3(1, 1, 1); // Aplicamos la escala en X positiva
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1); // Aplicamos la escala en X negativa
        }
    }

    protected override void RotarVertical()
    {
        if (direccion != DireccionMovimiento.Vertical) // Si la direcci�n no es vertical
        {
            return; // Detenemos la ejecuci�n
        }

        // Si es vertical
        if (PuntoPorMoverse.y > ultimaPosicion.y) // Si el personaje se mueve hacia arriba
        {
            _animator.SetBool(caminarAbajo, false); // Establecemos el par�metro caminarAbajo del Animator en false (activamos la animaci�n de caminar hacia arriba)
        }
        else // Si se mueve hacia abajo
        {
            _animator.SetBool(caminarAbajo, true); // Establecemos el par�metro caminarAbajo del Animator en true (activamos la animaci�n de caminar hacia abajo)
        }
    }
}
