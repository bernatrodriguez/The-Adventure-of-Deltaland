using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemigoVida : VidaBase
{
    public static Action<float> EventoEnemigoDerrotado;

    [Header("Vida")]
    [SerializeField] private EnemigoBarraVida barraVidaPrefab; // Referenciamos la barra de vida
    [SerializeField] private Transform barraVidaPosicion; // Referenciamos la posición

    [Header("Rastros")]
    [SerializeField] private GameObject rastros;

    private EnemigoBarraVida _enemigoBarraVidaCreada;
    private EnemigoInteraccion _enemigoInteraccion;
    private EnemigoMovimiento _enemigoMovimiento;
    private EnemigoLoot _enemigoLoot;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;
    private IAController _controller;

    private void Awake()
    {
        // Obtenemos los componentes necesarios
        _enemigoInteraccion = GetComponent<EnemigoInteraccion>();
        _enemigoMovimiento = GetComponent<EnemigoMovimiento>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _enemigoLoot = GetComponent<EnemigoLoot>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _controller = GetComponent<IAController>();
    }

    protected override void Start()
    {
        base.Start();
        CrearBarraVida(); // Creamos la barra de vida del enemigo
    }

    private void CrearBarraVida()
    {
        _enemigoBarraVidaCreada = Instantiate(barraVidaPrefab, barraVidaPosicion); // Instanciamos el prefab de la barra de vida en la posición definida
        ActualizarBarraVida(Salud, saludMax); // Actualizamos la barra
    }

    protected override void ActualizarBarraVida(float vidaActual, float vidaMax)
    {
        _enemigoBarraVidaCreada.ModificarSalud(vidaActual, vidaMax); // Modificamos los valores de la barra de vida
    }

    protected override void PersonajeDerrotado()
    {
        DesactivarEnemigo(); // Eliminamos al enemigo
        EventoEnemigoDerrotado?.Invoke(_enemigoLoot.ExpGanada);
        QuestManager.Instance.AñadirProgreso("Mata10", 1);
        QuestManager.Instance.AñadirProgreso("Mata25", 1);
        QuestManager.Instance.AñadirProgreso("Mata50", 1);
    }

    private void DesactivarEnemigo() // Método para eliminar al enemigo cuando es derrotado
    {
        rastros.SetActive(true); // Llamamos a rastros (loot drop)
        _enemigoBarraVidaCreada.gameObject.SetActive(false); // Desactivamos la barra de vida
        _spriteRenderer.enabled = false; // Dejamos de renderizarlo
        _enemigoMovimiento.enabled = false; // Detenemos el movimiento
        _controller.enabled = false; // Desactivamos el controller para que deje de seguir la ruta
        _boxCollider2D.isTrigger = true; // Lo ponemos como trigger
        _enemigoInteraccion.DesactivarSpritesSeleccion(); // Desactivamos los arcos de selección
    }
}