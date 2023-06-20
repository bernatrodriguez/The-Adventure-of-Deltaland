using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Arma")] // Permitimos poder crear armas desde el menú de Unity
public class ItemArma : InventarioItem
{
    [Header("Arma")]
    public Arma Arma; // Variable del tipo arma

    public override bool EquiparItem()
    {
        if (ContenedorArma.Instance.ArmaEquipada != null) // Si el contenedor tiene un arma equipada
        {
            return false; // Regresamos falso, no hemos podido equipar un arma porque ya hay una
        }

        // Si no tiene un arma equipada
        ContenedorArma.Instance.EquiparArma(this); // Equipamos el arma
        return true; // Regresamos verdadero, hemos podido equiparla
    }

    public override bool EliminarItem()
    {
        if (ContenedorArma.Instance.ArmaEquipada == null) // Si no hay ningun arma equipada
        {
            return false; // Devolvemos falso, no hemos podido eliminarla
        }

        // Si la hay
        ContenedorArma.Instance.EliminarArma(); // Llamamos al método de eliminar arma
        return true; // Devolvemos verdadero, se ha podido eliminar
    }

    public override string DescripcionItemCrafting()
    {
        string descripcion = $"- Probabilidad de Critico: {Arma.ChanceCritico}%\n" +
                             $"- Probabilidad de Bloqueo: {Arma.ChanceBloqueo}%";
        return descripcion;
    }
}
