using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Pocion Mana")] // Añadimos el item al menú de Unity

public class ItemPocionMana : InventarioItem // Hereda de la clase de InventarioItem
{
    [Header("Pocion info")]
    public float MPRestauracion; // Puntos de maná que restauramos con la poción

    public override bool UsarItem() // Con este método, utilizando override, podemos sobreescribir el método UsarItem, que hemos definido como virtual
    {
        if (Inventario.Instance.Personaje.PersonajeMana.SePuedeRestaurar) // Si el maná puede ser restaurado
        {
            Inventario.Instance.Personaje.PersonajeMana.RestaurarMana(MPRestauracion); // Restauramos el maná con el valor de la variable MPRestauración
            return true; // Regeresamos que se ha aplicado el item
        }

        return false; // Regresamos que no se ha aplicado el item
    }
}
