using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContenedorArma : Singleton<ContenedorArma>
{
    [SerializeField] private Image armaIcono;
    [SerializeField] private Image armaSkillIcono;

    public ItemArma ArmaEquipada { get; set; } // Propiedad que guarda la referencia del arma equipada

    public void EquiparArma(ItemArma itemArma) // Método para equipar un arma
    {
        ArmaEquipada = itemArma; // Definimos la referencia
        armaIcono.sprite = itemArma.Arma.ArmaIcono; // Para poder actualizar el arma del panel
        armaIcono.gameObject.SetActive(true); // Activamos el arma

        if (itemArma.Arma.Tipo == TipoArma.Magia) // Si el tipo de arma es de magia
        {
            armaSkillIcono.sprite = itemArma.Arma.IconoSkill; // Mostramos la habilidad en el panel
            armaSkillIcono.gameObject.SetActive(true); // Lo activamos
            // Con este if nos evitamos activar el sprite en las armas de melee, que no llevan habilidad
        }

        Inventario.Instance.Personaje.PersonajeAtaque.EquiparArma(itemArma); // Equipamos el arma
    }

    public void EliminarArma() // Método para eliminar el arma
    {
        armaIcono.gameObject.SetActive(false); // Desactivamos el arma
        armaSkillIcono.gameObject.SetActive(false); // Desactivamos la habilidad
        ArmaEquipada = null; // Hacemos el arma nula
        Inventario.Instance.Personaje.PersonajeAtaque.EliminarArma(); // Eliminamos el arma
    }
}
