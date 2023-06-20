using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class PersonajeAtaque : MonoBehaviour
{
    public static Action<float, EnemigoVida> EventoEnemigoDa�ado;

    [Header("Stats")]
    [SerializeField] private PersonajeStats stats;

    [Header("Pooler")]
    [SerializeField] private ObjectPooler pooler;

    [Header("Ataque")]
    [SerializeField] private float tiempoEntreAtaques;
    [SerializeField] private Transform[] posicionesDisparo;

    public Arma ArmaEquipada { get; private set; }
    public EnemigoInteraccion EnemigoObjetivo { get; private set; }
    public bool Atacando { get; set; }

    private PersonajeMana _personajeMana; // Referencia al man�
    private int indexDireccionDisparo; // Seg�n hacia donde se mueva el personaje, le daremos un �ndice que llamar� a su animaci�n correspondiente en un array
    private float tiempoParaSiguienteAtaque; // Tiempo para el siguiente ataque

    private void Awake()
    {
        _personajeMana = GetComponent<PersonajeMana>(); // Obtenemos el componente del man�
    }

    private void Update()
    {
        ObtenerDireccionDisparo(); // Obtenemos la direcci�n de disparo

        if (Time.time > tiempoParaSiguienteAtaque) // Si el tiempo actual del juego es mayor que el tiempo para el siguiente ataque
        {
            if (Input.GetKeyDown(KeyCode.Space)) // Si pulsamos la tecla espacio
            {
                if (ArmaEquipada == null || EnemigoObjetivo == null) // Si no hay un arma equipada o no hay un enemigo objetivo
                {
                    return; // Detenemos la ejecuci�n
                }

                UsarArma(); // Usamos el arma
                tiempoParaSiguienteAtaque = Time.time + tiempoEntreAtaques; // Actualizamos el tiempo para el siguiente ataque
                StartCoroutine(IEEstablecerCondicionAtaque());
            }
        }
    }

    private void UsarArma()
    {
        if (ArmaEquipada.Tipo == TipoArma.Magia) // Si el arma es de tipo magia
        {
            if (_personajeMana.ManaActual < ArmaEquipada.ManaRequerida) // Si no tenemos man� suficiente
            {
                return; // Terminamos la ejecuci�n
            }

            // Si tenemos man� suficiente

            GameObject nuevoProyectil = pooler.ObtenerInstancia(); // Instanciamos un proyectil en el pooler
            nuevoProyectil.transform.localPosition = posicionesDisparo[indexDireccionDisparo].position; // Definimos el punto de salida del proyectil

            Proyectil proyectil = nuevoProyectil.GetComponent<Proyectil>(); // Obtenemos la referencia de la clase proyectil
            proyectil.InicializarProyectil(this); // Inicializamos el proyectil

            nuevoProyectil.SetActive(true); // Activamos el proyectil
            _personajeMana.UsarMana(ArmaEquipada.ManaRequerida); // Descontamos el man� requerido por el arma
        }
        else
        {
            float da�o = ObtenerDa�o(); // Obtenemos el da�o
            EnemigoVida enemigoVida = EnemigoObjetivo.GetComponent<EnemigoVida>(); // referenciamos la clase
            enemigoVida.RecibirDa�o(da�o);
            EventoEnemigoDa�ado?.Invoke(da�o, enemigoVida); // Lanzamos el evento
        }
    }

    public float ObtenerDa�o()
    {
        float cantidad = stats.Da�o; // Accedemos a la stat del da�o
        if (Random.value < stats.PorcentajeCritico / 100) // Calculamos el cr�tico
        {
            cantidad *= 2;
        }

        return cantidad; // Regresamos la cantidad de da�o
    }

    private IEnumerator IEEstablecerCondicionAtaque() // Mientras estemos atacando, mostraremos la animaci�n de ataque
    {
        Atacando = true; // Atacando es verdadero
        yield return new WaitForSeconds(0.3f); // Esperamos 0.3 segundos
        Atacando = false; // Atacando es falso
    }

    public void EquiparArma(ItemArma armaPorEquipar) // Metodo para equipar un arma
    {
        ArmaEquipada = armaPorEquipar.Arma; // Definimos una variable con el arma por equipar
        if (ArmaEquipada.Tipo == TipoArma.Magia) // Si el arma es del tipo magia
        {
            pooler.CrearPooler(ArmaEquipada.ProyectilPrefab.gameObject); // Creamos el pooler de proyectiles
        }

        stats.A�adirBonusPorArma(ArmaEquipada); // A�adimos el bonus
    }

    public void EliminarArma()
    {
        if (ArmaEquipada == null) // Si no tenemos un arma equipada
        {
            return; // Detenemos la ejecuci�
        }

        if (ArmaEquipada.Tipo == TipoArma.Magia) // Si el arma es del tipo magia
        {
            pooler.DestruirPooler(); // Destruimos el pooler
        }

        stats.EliminarBonusPorArma(ArmaEquipada); // Eliminamos el bonus
        ArmaEquipada = null; // Desactivamos el arma
    }

    private void ObtenerDireccionDisparo()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); // Valor de hacia donde nos movemos
        
        if (input.x > 0.1f) // Si nos movemos hacia la derecha
        {
            indexDireccionDisparo = 1; // Indice 1 del array (animaci�n derecha)
        }
        else if (input.x < 0f) // Si nos movemos hacia la izquierda
        {
            indexDireccionDisparo = 3; // Indice 3 del array (animaci�n izquierda)
        }
        else if (input.y > 0.1f) // Si nos movemos hacia arriba
        {
            indexDireccionDisparo = 0; // Indice 0 del array (animaci�n arriba)
        }
        else if (input.y < 0f) // Si nos movemos hacia abajo
        {
            indexDireccionDisparo = 2; // Indice 2 del array (animaci�n abajo)
        }
    }

    private void EnemigoRangoSeleccionado(EnemigoInteraccion enemigoSeleccionado)
    {
        if (ArmaEquipada == null) // Si el arma equipada es nula
        {
            return; // Detenemos la ejecucion
        }

        if (ArmaEquipada.Tipo != TipoArma.Magia) // Si no es del tipo magia
        {
            return; // Detenemos la ejecucion
        }

        if (EnemigoObjetivo == enemigoSeleccionado) // Si el enemigo objetivo es el mismo que el seleccionado
        {
            return; // Detenemos la ejecucion (para no volver a seleccionar al mismo enemigo)
        }

        // Si no es el caso
        EnemigoObjetivo = enemigoSeleccionado; // Seleccionamos el enemigo
        EnemigoObjetivo.MostrarEnemigoSeleccionado(true, TipoDeteccion.Rango); // Mostramos el enemigo selccionado
    }

    private void EnemigoNoSeleccionado()
    {
        if (EnemigoObjetivo == null) // Si el enemigo seleccionado es nulo
        {
            return; // Detenemos la ejecuci�n
        }

        // Si lo hay
        EnemigoObjetivo.MostrarEnemigoSeleccionado(false, TipoDeteccion.Rango); // Ocultamos la selecci�n
        EnemigoObjetivo = null; // Perdemos la referencia
    }

    private void EnemigoMeleeDetectado(EnemigoInteraccion enemigoDetectado)
    {
        if (ArmaEquipada == null) // Si el arma equipada es nula
        {
            return; // Detenemos la ejecucion
        }

        if (ArmaEquipada.Tipo != TipoArma.Melee) // Si no es del tipo melee
        {
            return; // Detenemos la ejecucion
        }

        EnemigoObjetivo = enemigoDetectado; // Seleccionamos al enemigo
        EnemigoObjetivo.MostrarEnemigoSeleccionado(true, TipoDeteccion.Melee); // Mostramos el enemigo seleccionado
    }

    private void EnemigoMeleePerdido()
    {
        if (ArmaEquipada == null) // Si el arma equipada es nula
        {
            return; // Detenemos la ejecucion
        }

        if (EnemigoObjetivo == null) // Si el enemigo seleccionado es nulo
        {
            return; // Detenemos la ejecuci�n
        }

        if (ArmaEquipada.Tipo != TipoArma.Melee) // Si no es del tipo melee
        {
            return; // Detenemos la ejecucion
        }

        EnemigoObjetivo.MostrarEnemigoSeleccionado(false, TipoDeteccion.Melee); // Dejamos de mostrar el arco de seleccion
        EnemigoObjetivo = null; // Eliminamos la referencia
    }

    private void OnEnable() // Nos suscribimos a los eventos
    {
        SeleccionManager.EventoEnemigoSeleccionado += EnemigoRangoSeleccionado;
        SeleccionManager.EventoObjetoNoSeleccionado += EnemigoNoSeleccionado;
        PersonajeDetector.EventoEnemigoDetectado += EnemigoMeleeDetectado;
        PersonajeDetector.EventoEnemigoPerdido += EnemigoMeleePerdido;
    }

    private void OnDisable() // Nos desuscribimos de los eventos
    {
        SeleccionManager.EventoEnemigoSeleccionado -= EnemigoRangoSeleccionado;
        SeleccionManager.EventoObjetoNoSeleccionado -= EnemigoNoSeleccionado;
        PersonajeDetector.EventoEnemigoDetectado -= EnemigoMeleeDetectado;
        PersonajeDetector.EventoEnemigoPerdido -= EnemigoMeleePerdido;
    }
}