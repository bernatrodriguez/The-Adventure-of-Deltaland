using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum TipoDeInteraccion // Lista con los tipos de interacción
{
    Click,
    Usar,
    Equipar,
    Eliminar
}

public class InventarioSlot : MonoBehaviour
{
    public static Action<TipoDeInteraccion, int> EventoSlotInteraccion; // Definimos un evento para detectar los tipos de interacción

    // Objetos que activaremos o desactivaremos dependiendo de si tenemos un item en el slot
    [SerializeField] private Image itemIcono;
    [SerializeField] private GameObject fondoCantidad;
    [SerializeField] private TextMeshProUGUI cantidadTMP;
    
    public int Index { get; set; } // Índice del slot

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>(); // Referencia al botón
    }

    public void ActualizarSlot(InventarioItem item, int cantidad) // Pasamos un item y una cantidad concreta, para actualizar el prefab del slot
    {
        itemIcono.sprite = item.Icono;
        cantidadTMP.text = cantidad.ToString();
    }

    public void ActivarSlotUI(bool estado) // Pasamos un verdadero o un falso, dependiendo de esto activaremos o desactivaremos el icono y el fondo
    {
        itemIcono.gameObject.SetActive(estado); // Activamos/Desactivamos icono
        fondoCantidad.SetActive(estado); // Activamos/Desactivamos fondo
    }

    public void SeleccionarSlot() // Al seleccionar un slot
    {
        _button.Select(); // Definimos el slot como seleccionado
    }

    public void ClickSlot()
    {
        EventoSlotInteraccion?.Invoke(TipoDeInteraccion.Click, Index); // Si el evento no es nulo, lo invocamos con la interacción de click

        //Verificamos si podemos mover el item a otro slot
        if (InventarioUI.Instance.IndexSlotInicialPorMover != -1) // Si esta propiedad tiene un valor diferente a -1 (predeterminado), es decir, tenemos el índice de un slot seleccionado
        {
            if (InventarioUI.Instance.IndexSlotInicialPorMover != Index) // Si el índice del item seleccionado es diferente al de este slot
            {
                Inventario.Instance.MoverItem(InventarioUI.Instance.IndexSlotInicialPorMover, Index); // Movemos el item
            }
        }
    }

    public void SlotUsarItem()
    {
        if (Inventario.Instance.ItemsInventario[Index] != null) // Si existe un item en el slot con este índice
        {
            EventoSlotInteraccion?.Invoke(TipoDeInteraccion.Usar, Index); // Si el evento no es nulo, lo invocamos con la interacción de usar
        }
    }

    public void SlotEquiparItem() // Cuando seleccionemos un item en nuestro inventario y le demos a equipar, llamaremos a este método
    {
        if (Inventario.Instance.ItemsInventario[Index] != null) // Si tenemos un item en el slot
        {
            EventoSlotInteraccion?.Invoke(TipoDeInteraccion.Equipar, Index); // Si no es nulo, lo invocamos con el tipo de interacción equipar
        }
    }

    public void SlotEliminarItem() // Cuando seleccionemos un item en nuestro inventario y le demos a eliminar, llamaremos a este método
    {
        if (Inventario.Instance.ItemsInventario[Index] != null) // Si tenemos un item en el slot
        {
            EventoSlotInteraccion?.Invoke(TipoDeInteraccion.Eliminar, Index); // Si no es nulo, lo invocamos con el tipo de interacción eliminar
        }
    }
}
