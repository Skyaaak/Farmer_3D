using System;
using System.Collections;
using System.Collections.Generic;
using InventoryManager;
using Player;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;
    
    private PlayerMovement playerMovement;
    private PlayerGenetic playerGenetic;
    [SerializeField] private Inventory playerInventory;
    private CharacterController controller;
    [SerializeField] private Item itemInHand;

    void Start()
    {
        this.controller = GetComponent<CharacterController>();
        this.playerGenetic = new PlayerGenetic();
        this.playerMovement = new PlayerMovement(this.transform, this.controller, this.gameInput);
        this.playerInventory = new Inventory();
        this.itemInHand = null;
    }

    // Update is called once per frame
    
    void Update()
    {
        this.playerMovement.Update();
        this.playerGenetic.Update();
        this.playerInventory.Update(playerGenetic.Strengh);
    }

    public Item GetItemInHand() => this.itemInHand;
    public void EquipItem(Item item) => this.itemInHand = item;
    public void UnequipItem() => this.itemInHand = null;
}
