using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Pocion Mana")] // A�adimos el item al men� de Unity

public class ItemPocionMana : InventarioItem // Hereda de la clase de InventarioItem
{
    [Header("Pocion info")]
    public float MPRestauracion; // Puntos de man� que restauramos con la poci�n

    public override bool UsarItem() // Con este m�todo, utilizando override, podemos sobreescribir el m�todo UsarItem, que hemos definido como virtual
    {
        if (Inventario.Instance.Personaje.PersonajeMana.SePuedeRestaurar) // Si el man� puede ser restaurado
        {
            Inventario.Instance.Personaje.PersonajeMana.RestaurarMana(MPRestauracion); // Restauramos el man� con el valor de la variable MPRestauraci�n
            return true; // Regeresamos que se ha aplicado el item
        }

        return false; // Regresamos que no se ha aplicado el item
    }
}
