using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventarioUI : Singleton<InventarioUI>
{
    [Header("Panel Inventario Descripción")]
    [SerializeField] private GameObject panelInventarioDescripcion; // Referencia al panel de descripción del inventario
    [SerializeField] private Image itemIcono; // Referencia al icono del item en el slot seleccionado
    [SerializeField] private TextMeshProUGUI itemNombre; // Referencia al nombre del item en el slot seleccionado
    [SerializeField] private TextMeshProUGUI itemDescripcion; // Referencia a la descripción del item en el slot seleccionado

    [SerializeField] private InventarioSlot slotPrefab; // Referencia al prefab del slot
    [SerializeField] private Transform contenedor;

    public int IndexSlotInicialPorMover { get; private set; } // Propiedad que nos permite mover entre slots, set es privado porque solo lo establecemos en esta clase
    public InventarioSlot SlotSeleccionado { get; private set; } // Propiedad que contiene la información sobre el slot seleccionado, set es privado porque solo lo establecemos en esta clase
    private List<InventarioSlot> slotsDisponibles = new List<InventarioSlot>(); // Creamos e inicializamos una lista

    // Start is called before the first frame update
    private void Start()
    {
        InicializarInventario(); // Inicializamos el inventario
        IndexSlotInicialPorMover = -1; // Inicializamos el índice para mover items en el inventario, establecemos -1 porque este índice no existe en nuestro inventario, aquí que no interferirá en su valor por defecto
    }

    private void Update()
    {
        ActualizarSlotSeleccionado(); // Actualizamos el slot seleccionado
        if (Input.GetKeyDown(KeyCode.M)) // Si pulsamos el botón de mover (letra M)
        {
            if (SlotSeleccionado != null) // Si tenemos un slot seleccionado
            {
                IndexSlotInicialPorMover = SlotSeleccionado.Index; // Establecemos el índice para mover con el valor del índice del slot seleccionado
            }
        }
    }

    private void InicializarInventario()
    {
        for (int i = 0; i < Inventario.Instance.NumeroDeSlots; i++) // Dado el numero de slots
        {
            InventarioSlot nuevoSlot = Instantiate(slotPrefab, contenedor); // Instanciamos el prefab en el contenedor la cantidad de veces deseada
            nuevoSlot.Index = i; // Guardamos el índice del slot (0, 1, 2, 3, etc...) para posteriormente acceder a cada uno de ellos. Nota: no cambiar NUNCA una i por un 1, tu juego fallará y tardarás 3 días en darte cuenta
            slotsDisponibles.Add(nuevoSlot); // Guardamos el slot en la lista
        }
    }

    private void ActualizarSlotSeleccionado() // Método para detectar el slot seleccionado en el momento
    {
        GameObject goSeleccionado = EventSystem.current.currentSelectedGameObject; // Nos regresa cual es el objeto que está seleccionado en este momento
        if (goSeleccionado == null) // Si no hay un objeto seleccionado
        {
            return; // Detenemos la ejecución
        }

        // Si hay un objeto seleccionado
        InventarioSlot slot = goSeleccionado.GetComponent<InventarioSlot>(); // Verificamos que tiene la clase de InventarioSlot y guardamos la referencia en la variable
        if (slot != null) // Si sí que existe la clase de InventarioSlot en el objeto seleccionado
        {
            SlotSeleccionado = slot; // Actualizamos el slot seleccionado
        }
    }

    public void DibujarItemEnInventario(InventarioItem itemPorAñadir, int cantidad, int itemIndex)
    {
        InventarioSlot slot = slotsDisponibles[itemIndex]; // Aplicamos el mismo índice al item y al slot
        if (itemPorAñadir != null) // Si añadimos un item a un slot
        {
            slot.ActivarSlotUI(true); // Activamos la UI del slot
            slot.ActualizarSlot(itemPorAñadir, cantidad); // Reflejamos el item y la cantidad
        }
        else // Si no
        {
            slot.ActivarSlotUI(false); // Desactivamos la UI del slot
        }
    }

    private void ActualizarInventarioDescripcion(int index) // Método que actualiza la descripción del item en el inventario
    {
        if (Inventario.Instance.ItemsInventario[index] != null) // Si hay un item en el slot seleccionado
        {
            itemIcono.sprite = Inventario.Instance.ItemsInventario[index].Icono; // Actualizamos el icono
            itemNombre.text = Inventario.Instance.ItemsInventario[index].Nombre; // Actualizamos el nombre
            itemDescripcion.text = Inventario.Instance.ItemsInventario[index].Descripcion; // Actualizamos la descripción
            panelInventarioDescripcion.SetActive(true); // Activamos el panel de descripción, que por defecto está desactivado
        }
        else // Si por el contrario no hay un item en el slot seleccionado
        {
            panelInventarioDescripcion.SetActive(false); // Desactivamos el panel de descripción
        }
    }

    public void UsarItem() // Método llamado al pulsar el botón de usar en el inventario
    {
        if (SlotSeleccionado != null) // Si tenemos un slot seleccionado
        {
            SlotSeleccionado.SlotUsarItem(); // Lanzamos el evento para usar el item del slot
            SlotSeleccionado.SeleccionarSlot(); // Definimos el slot como seleccionado
        }
    }

    #region Evento
    private void SlotInteraccionRespuesta(TipoDeInteraccion tipo, int index) // Cuando hacemos click en un item, mostramos su descipcion correspondiente
    {
        if (tipo == TipoDeInteraccion.Click) // Si el tipo de interacción es click
        {
            ActualizarInventarioDescripcion(index); // Llamamos al método para actualizar el panel de descripción, con el indice del slot seleccionado
        }
    }

    private void OnEnable()
    {
        InventarioSlot.EventoSlotInteraccion += SlotInteraccionRespuesta; // Nos suscribimos al evento
    }

    private void OnDisable()
    {
        InventarioSlot.EventoSlotInteraccion -= SlotInteraccionRespuesta; // Nos desuscribimos al evento
    }
    #endregion
}
