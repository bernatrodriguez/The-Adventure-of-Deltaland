using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : Singleton<Inventario>
{
    [Header("Items")] // Título
    [SerializeField] private InventarioItem[] itemsInventario; // Array que contendrá los items del inventario
    [SerializeField] private int numeroDeSlots; // Variable que contiene el número de slots
    [SerializeField] private Personaje personaje; // Referencia al personaje

    public InventarioItem[] ItemsInventario => itemsInventario; // Propiedad que nos regresa el array del inventario
    public int NumeroDeSlots => numeroDeSlots; // Propiedad que nos regresa el numero de slots
    public Personaje Personaje => personaje; // Propiedad que nos regresa la variable de personaje

    private void Start()
    {
        itemsInventario = new InventarioItem[numeroDeSlots]; // Creamos el array con la longitud del numero de items que tenemos
    }

    public void AñadirItem(InventarioItem itemPorAñadir, int cantidad) // Como parámetros, le pasamos la referencia del item y su cantidad
    {
        // En caso de tener un item nulo
        if (itemPorAñadir == null) // Si el item es nulo
        {
            return; // Terminamos la ejecución
        }

        // En caso de tener un item similar en el inventario
        List<int> indexes = VerificarExistencias(itemPorAñadir.ID); // Con esta lista guardamos los indices de los items que tratamos de añadir al inventario
        if (itemPorAñadir.EsAcumulable) // Si el item que tratamos de añadir es acumulable, continuamos
        {
            if (indexes.Count > 0) // Si los indices son mayores a 0, continuamos
            {
                for (int i = 0; i < indexes.Count; i++)
                {
                    if (itemsInventario[indexes[i]].Cantidad < itemPorAñadir.AcumulacionMax) // Si la cantidad del item en el inventario no supera su acumulación máxima
                    {
                        itemsInventario[indexes[i]].Cantidad += cantidad; // Sumamos la cantidad que hemos recogido
                        if (itemsInventario[indexes[i]].Cantidad > itemPorAñadir.AcumulacionMax) // Si al sumar hemos superado la acumulación máxima
                        {
                            int diferencia = itemsInventario[indexes[i]].Cantidad - itemPorAñadir.AcumulacionMax; // Obtenemos la diferencia (cantidad - máximo)
                            itemsInventario[indexes[i]].Cantidad = itemPorAñadir.AcumulacionMax; // Añadimos al inventario la cantidad máxima en un slot
                            AñadirItem(itemPorAñadir, diferencia); // Volvemos a llamar a este mismo método pero con la diferencia
                        }

                        InventarioUI.Instance.DibujarItemEnInventario(itemPorAñadir, itemsInventario[indexes[i]].Cantidad, indexes[i]); // Actualizamos la UI del inventario
                        return; // Detenemos la ejecución
                    }
                }
            }
        }

        // En caso de NO tener un item similar en el inventario
        if (cantidad <= 0) // Si la cantidad que tratamos de añadir es 0
        {
            return; // Terminamos la ejecución
        }

        if (cantidad > itemPorAñadir.AcumulacionMax) // Si la cantidad que tratamos de añadir supera la acumulación máxima del item
        {
            AñadirItemEnSlotDisponible(itemPorAñadir, itemPorAñadir.AcumulacionMax); // Añadimos la cantidad máxima al slot vacío
            cantidad -= itemPorAñadir.AcumulacionMax; // Obtenemos la diferencia
            AñadirItem(itemPorAñadir, cantidad); // Volvemos a llamar a este mismo método pero con la diferencia
        }
        else // Si no supera la acumulación máxima
        {
            AñadirItemEnSlotDisponible(itemPorAñadir, cantidad); // Añadimos en el slot vacío la cantidad deseada
        }
    }

    private List<int> VerificarExistencias(string itemID) // Verificamos si el item ya existe en el inventario, para de este modo acumularlos
    {
        List<int> indexesDelItem = new List<int>();
        for (int i = 0; i < itemsInventario.Length; i++) // Recorremos todo el inventario
        {
            if (itemsInventario[i] != null) // Si encontramos un slot que no está vacío
            {
                if (itemsInventario[i].ID == itemID) // Si encontramos un item con el mismo ID
                {
                    indexesDelItem.Add(i); // Añadimos el indice a la lista
                }
            }
        }

        return indexesDelItem; // Regresamos la lista de índices
    }

    private void AñadirItemEnSlotDisponible(InventarioItem item, int cantidad)
    {
        for (int i = 0; i < itemsInventario.Length; i++) // Recorremos todo el inventario
        {
            if (itemsInventario[i] == null) // Si encontramos un slot nulo, es decir, vacío
            {
                itemsInventario[i] = item.CopiarItem(); // Añadimos el item al inventario creando una nueva instancia del ScriptableObject
                itemsInventario[i].Cantidad = cantidad; // Añadimos la cantidad deseada
                InventarioUI.Instance.DibujarItemEnInventario(item, cantidad, i); // Actualizamos la UI del inventario
                return; // Terminamos la ejecución
            }
        }
    }

    private void EliminarItem(int index) // Método para eliminar items, por ejemplo, al usar una poción de vida
    {
        itemsInventario[index].Cantidad--; // Reducimos la cantidad del item en 1
        if (itemsInventario[index].Cantidad <= 0) // Si la cantidad resultante del item es igual o menor que 0
        {
            itemsInventario[index].Cantidad = 0; // Establecemos la cantidad en 0
            itemsInventario[index] = null; // Hacemos el item nulo, es decir, lo eliminamos
            InventarioUI.Instance.DibujarItemEnInventario(null, 0, index); // Actualizamos la interfaz del inventario vaciando el slot
        }
        else // Si la cantidad es superior a 0
        {
            InventarioUI.Instance.DibujarItemEnInventario(itemsInventario[index], itemsInventario[index].Cantidad, index); // Actualizamos la interfaz del inventario con la nueva cantidad
        }
    }
    
    public void MoverItem(int indexInicial, int indexFinal) // Como parámetros, le pasamos el índice del slot desde el cual se moverá el item, y el índice del slot al que se moverá el mismo
    {
        if (itemsInventario[indexInicial] == null || itemsInventario[indexFinal] != null) // Si no hay un item en el slot inicial o hay un item en el slot final (slot ocupado)
        {
            return; // Paramos la ejecución, no hacemos nada
        }

        InventarioItem itemPorMover = itemsInventario[indexInicial].CopiarItem(); // Copiamos el item del slot inicial
        itemsInventario[indexFinal] = itemPorMover; // Establecemos el slot final con el valor del item copiado
        InventarioUI.Instance.DibujarItemEnInventario(itemPorMover, itemPorMover.Cantidad, indexFinal); // Dibujamos el item en el slot final

        itemsInventario[indexInicial] = null; // Eliminamos el slot inicial
        InventarioUI.Instance.DibujarItemEnInventario(null, 0, indexInicial); // Lo mostramos como vacío
    }

    private void UsarItem(int index) // Método para usar items
    {
        if (itemsInventario[index] == null) // Si este item no existe en el slot
        {
            return; // Detenemos la ejecución
        }

        if (itemsInventario[index].UsarItem()) // Si el item en el slot se ha podido usar, es decir, si el método UsarItem nos ha regresado true
        {
            EliminarItem(index); // Eliminamos una unidad del item
        }
    }
    
    #region Eventos

    private void SlotInteraccionRespuesta(TipoDeInteraccion tipo, int index)
    {
        switch (tipo) // En caso de que el tipo de interacción sea:
        {
            case TipoDeInteraccion.Usar: // Usar
                UsarItem(index);
                break;
            case TipoDeInteraccion.Equipar: // Equipar
                break;
            case TipoDeInteraccion.Eliminar: // Eliminar
                break;
        }
    }

    private void OnEnable()
    {
        InventarioSlot.EventoSlotInteraccion += SlotInteraccionRespuesta; // Nos suscribimos al evento
    }

    private void OnDisable()
    {
        InventarioSlot.EventoSlotInteraccion -= SlotInteraccionRespuesta; // Nos desuscribimos del evento
    }

    #endregion
}
