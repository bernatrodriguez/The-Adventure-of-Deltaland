using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Pocion Vida")] // A�adimos el item al men� de Unity

public class ItemPocionVida : InventarioItem // Hereda de la clase de InventarioItem
{
    [Header("Pocion info")]
    public float HPRestauracion; // Puntos de vida que restauramos con la poci�n

    public override bool UsarItem() // Con este m�todo, utilizando override, podemos sobreescribir el m�todo UsarItem, que hemos definido como virtual
    {
        if (Inventario.Instance.Personaje.PersonajeVida.PuedeSerCurado) // Si el personaje puede ser curado
        {
            Inventario.Instance.Personaje.PersonajeVida.RestaurarSalud(HPRestauracion); // Restauramos su salud con el valor de la variable HPRestauraci�n
            return true; // Regeresamos que se ha aplicado el item
        }

        return false; // Regresamos que no se ha aplicado el item
    }

    public override string DescripcionItemCrafting()
    {
        string descripcion = $"Restaura {HPRestauracion} de Salud";
        return descripcion;
    }
}
