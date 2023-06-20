using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecetaTarjeta : MonoBehaviour
{
    [SerializeField] private Image recetaIcono;
    [SerializeField] private TextMeshProUGUI recetaNombre;

    public Receta RecetaCargada { get; private set; }

    public void ConfigurarRecetaTarjeta(Receta receta)
    {
        RecetaCargada = receta;
        recetaIcono.sprite = receta.ItemResultado.Icono;
        recetaNombre.text = receta.ItemResultado.Nombre;
    }

    public void SeleccionarReceta()
    {
        UIManager.Instance.AbrirCerrarPanelCraftingInformacion(true);
        CraftingManager.Instance.MostrarReceta(RecetaCargada);
    }
}
