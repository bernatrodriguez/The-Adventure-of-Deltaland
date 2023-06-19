using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ZonaConfiner : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera camara; // Cámara de Cinemachine

    private void OnTriggerEnter2D(Collider2D other) // Detectamos si un objeto entra en los confiners
    {
        if (other.CompareTag("Player")) // Si el objeto que entra tiene la tag "Player", es decir, es el jugador
        {
            camara.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) // Detectamos si un objeto sale de los confiners
    {
        if (other.CompareTag("Player")) // Si el objeto que sale tiene la tag "Player", es decir, es el jugador
        {
            camara.gameObject.SetActive(false);
        }
    }
}
