using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DoorScript
{
	[RequireComponent(typeof(AudioSource))]


public class Door : MonoBehaviour {
	public bool open;
	[SerializeField] private bool isLocked;
	[SerializeField] private LayerMask level1Mask;
	[SerializeField] private LayerMask level2Mask;
	[SerializeField] private int puntosReq;

	public float smooth = 1.0f;
	float DoorOpenAngle = -90.0f;
    float DoorCloseAngle = 0.0f;
	public AudioSource asource;
	public AudioClip openDoor,closeDoor, tryOpenDoor;
	// Use this for initialization
	void Start () {
		asource = GetComponent<AudioSource> ();

		GameObject parent = transform.parent.gameObject;

		if((level1Mask.value & (1 << parent.layer)) != 0)
		{
			isLocked = true;
			puntosReq = 27; // Puntos para desbloquear la puerta del primer nivel
		}
		else if((level2Mask.value & (1 << parent.layer)) != 0)
		{
			isLocked = true;
			puntosReq = 61; // Puntos para desbloquear la puerta del segundo nivel
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (open)
		{
            var target = Quaternion.Euler (0, DoorOpenAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * 5 * smooth);
	
		}
		else
		{
            var target1= Quaternion.Euler (0, DoorCloseAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, target1, Time.deltaTime * 5 * smooth);
	
		} 

		if (isLocked)
		{
			if (UIManager.Instance.ObtenerPuntos() >= puntosReq)
			{
				isLocked = false;
			}
		}

	}

	public void OpenDoor(){
		if(!isLocked)
		{
			open =!open;
			asource.clip = open?openDoor:closeDoor;
			asource.Play ();
		}
		else
		{
			asource.clip = tryOpenDoor;
			asource.Play();
			UIManager.Instance.MostrarMensaje("Necesitas al menos: " + puntosReq + " puntos para abrir esta puerta.", 2f);
		}
	}
}
}