using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Proyectil : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float velocidad; // Velocidad del proyectil

    public PersonajeAtaque PersonajeAtaque { get; private set; }

    private Rigidbody2D _rigidbody2D;
    private Vector2 direccion; // Direccion del proyectil
    private EnemigoInteraccion enemigoObjetivo; // Referencia del enemigo

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>(); // Referencia del rigidbody
    }

    private void FixedUpdate()
    {
        if (enemigoObjetivo == null) // Si el enemigo objetivo es nulo
        {
            return; // Detenemos la ejecuci�n
        }

        // Si no lo es
        MoverProyectil(); // Movemos el proyectil
    }

    private void MoverProyectil() // M�todo para mover el proyectil
    {
        direccion = enemigoObjetivo.transform.position - transform.position; // direccion del proyectil
        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg; // �ngulo para poder rotar el proyectil

        transform.rotation = Quaternion.AngleAxis(angulo, Vector3.forward); // Aplicamos la rotaci�n
        _rigidbody2D.MovePosition(_rigidbody2D.position + direccion.normalized * velocidad * Time.fixedDeltaTime); // Movemos la posici�n del proyectil
    }

    public void InicializarProyectil(PersonajeAtaque ataque)
    {
        PersonajeAtaque = ataque; // Definimos el ataque
        enemigoObjetivo = ataque.EnemigoObjetivo; // Definimos el objetivo
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemigo")) // Si colisiona con tag de enemigo
        {
            float da�o = PersonajeAtaque.ObtenerDa�o(); // Obtenemos la variable de da�o
            EnemigoVida enemigoVida = enemigoObjetivo.GetComponent<EnemigoVida>(); // Llamamos al m�todo de recibir da�o
            enemigoVida.RecibirDa�o(da�o);
            PersonajeAtaque.EventoEnemigoDa�ado?.Invoke(da�o, enemigoVida); // Lanzamos el evento
            gameObject.SetActive(false); // Desactivamos el objeto
        }
    }
}