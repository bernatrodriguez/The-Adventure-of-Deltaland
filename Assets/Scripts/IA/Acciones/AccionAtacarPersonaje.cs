using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Acciones/Atacar Personaje")] // Añadimos la acción al menú de Unity
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
            return; // Detenemos la ejecución
        }

        if (controller.EsTiempoDeAtacar() == false) // Si no es tiempo de atacar
        {
            return; // Detenemos la ejecución
        }

        // Si ninguna de las anteriores condiciones se cumple
        if (controller.PersonajeEnRangoDeAtaque(controller.RangoDeAtaqueDeterminado)) // Si el personaje está en rango de ataque
        {
            if (controller.TipoAtaque == TiposDeAtaque.Embestida) // Si el tipo de ataque es embestida
            {
                controller.AtaqueEmbestida(controller.Daño); // Aplicamos el ataque de embestida
            }
            else // Si no
            {
                controller.AtaqueMelee(controller.Daño); // Aplicamos el ataque melee
            }
            controller.ActualizarTiempoEntreAtaques(); // Actualizamos el tiempo entre ataques
        }
    }
}
