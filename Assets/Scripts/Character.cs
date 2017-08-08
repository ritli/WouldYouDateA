using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Character : MonoBehaviour, IPointerDownHandler {

    [SerializeField]
    Vector2 offset = Vector2.zero;
    public CharacterData characterData;

	// Use this for initialization
	void Start () 
    {
        //characterData = new CharacterData();
	}

    /// <summary>
    /// Data is passed by the instantiating function.
    /// </summary>
    /// <param name="data">Data.</param>
    void LoadCharData(ref CharacterData data)
    {
        characterData = data;
    }

    public Vector2 GetOffset()
    {
        return offset;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Manager.GameState == GameState.explore)
        {
            Manager.StartDialogue(characterData);
        }
    }
}
