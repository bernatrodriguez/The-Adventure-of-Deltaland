using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Acciones/Atacar Personaje")] // A�adimos la acci�n al men� de Unity
public class AccionAtacarPersonaje : IAAccion
{
    public override void Ejecutar(IAController controller)
    {
        Atacar(controller);
    }

    private void Atacar(IAController controller)
    {
        if (controller.PersonajeReferencia == null) // Si no tenemos una referencia del personaje
        {
            return; // Detenemos la ejecuci�n
        }

        if (controller.EsTiempoDeAtacar() == false) // Si no es tiempo de atacar
        {
            return; // Detenemos la ejecuci�n
        }

        // Si ninguna de las anteriores condiciones se cumple
        if (controller.PersonajeEnRangoDeAtaque(controller.RangoDeAtaqueDeterminado)) // Si el personaje est� en rango de ataque
        {
            if (controller.TipoAtaque == TiposDeAtaque.Embestida) // Si el tipo de ataque es embestida
            {
                controller.AtaqueEmbestida(controller.Da�o); // Aplicamos el ataque de embestida
            }
            else // Si no
            {
                controller.AtaqueMelee(controller.Da�o); // Aplicamos el ataque melee
            }
            controller.ActualizarTiempoEntreAtaques(); // Actualizamos el tiempo entre ataques
        }
    }
}
