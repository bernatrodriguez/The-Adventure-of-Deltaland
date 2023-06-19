using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private int cantidadPorCrear; // Cantidad de instancias

    private List<GameObject> lista; // Lista de instancias
    public GameObject ListaContenedor { get; private set; } // Contenedor

    public void CrearPooler(GameObject objetoPorCrear) // Creamos un pooler que contendr� los objetos
    {
        lista = new List<GameObject>(); // Creamos una nueva lista
        ListaContenedor = new GameObject($"Pool - {objetoPorCrear.name}"); // Creamos el objeto en la jerarquia

        for (int i = 0; i < cantidadPorCrear; i++) // Creamos una nueva instancia del objeto las veces que definamos en cantidadPorCrear
        {
            lista.Add(A�adirInstancia(objetoPorCrear));
        }
    }

    private GameObject A�adirInstancia(GameObject objetoPorCrear) // M�todo para a�adir una instancia
    {
        GameObject nuevoObjeto = Instantiate(objetoPorCrear, ListaContenedor.transform); // Creamos un nuevo objeto
        nuevoObjeto.SetActive(false); // Lo desactivamos por defecto
        return nuevoObjeto; // Regresamos la instancia
    }

    public GameObject ObtenerInstancia() // M�todo para buscar objetos no utilizados y eliminarlos
    {
        for (int i = 0; i < lista.Count; i++) // Recorremos la lista
        {
            if (lista[i].activeSelf == false) // Buscamos una instancia no activa
            {
                return lista[i]; // Regresamos la posici�n de la instancia
            }
        }

        return null;
    }
}
