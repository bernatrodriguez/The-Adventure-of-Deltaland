using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TiposDeItem
{
    Armas,
    Pociones,
    Pergaminos,
    Ingredientes,
    Tesoros
}

public class InventarioItem : ScriptableObject
{
    [Header("Parametros")]
    public string ID; // Identificador del item
    public string Nombre; // Nombre del item
    public Sprite Icono; // Icono del item
    [TextArea] public string Descripcion; // Descripción del item. TextArea nos da un mayor espacio para escribir texto

    [Header("Informacion")]
    public TiposDeItem Tipo; // Tipo de item
    public bool EsConsumible; // Item consumible, es decir, lo gastamos al usarlo
    public bool EsAcumulable; // Item acumulable, es decir, varios se acumulan en un solo slot del inventario
    public int AcumulacionMax; // Cuantas unidades de items acumulables podemos poner por slot

    [HideInInspector] public int Cantidad; // Controla la cantidad que queda, la ocultamos del inspector ya que solo la usamos como contador

    public InventarioItem CopiarItem() // Para evitar posibles errores, vamos a usar diferentes ScriptableObjects en cada slot
    {
        InventarioItem nuevaInstancia = Instantiate(this);
        return nuevaInstancia;
    }

    public virtual bool UsarItem() // Método para usar el item, con acceso virtual para poder sobreescribirlo
    {
        return true; // Por defecto nos regresa el valor de verdadero
    }

    public virtual bool EquiparItem() // Método para equipar el item, con acceso virtual para poder sobreescribirlo
    {
        return true; // Por defecto nos regresa el valor de verdadero
    }

    public virtual bool EliminarItem() // Método para eliminar el item, con acceso virtual para poder sobreescribirlo
    {
        return true; // Por defecto nos regresa el valor de verdadero
    }

    public virtual string DescripcionItemCrafting()
    {
        return "";
    }
}