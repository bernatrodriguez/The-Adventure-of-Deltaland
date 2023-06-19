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

        if (itemArma.Arma.Tipo == TipoArma.Magia)
        {
            armaSkillIcono.sprite = itemArma.Arma.IconoSkill; // Para poder actualizar la habilidad del arma del panel
            armaSkillIcono.gameObject.SetActive(true); // Activamos la habilidad
        }

    }

    public void EliminarArma() // Método para eliminar el arma
    {
        armaIcono.gameObject.SetActive(false); // Desactivamos el arma
        armaSkillIcono.gameObject.SetActive(false); // Desactivamos la habilidad
        ArmaEquipada = null; // Hacemos el arma nula

    }
}
