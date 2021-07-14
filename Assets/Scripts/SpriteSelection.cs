using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSelection : MonoBehaviour
{
   GameManager gameManager;
   private void Awake()
   {
      // initialize variables
      
      gameManager = FindObjectOfType<GameManager>();
      

   }
   public void SelectAndGate()
   {
  
      if (gameManager.GetPlayerPoints()>=gameManager.AndGatePoints)
      {
         gameManager.SpawnGate(GateType.And);
         gameManager.DebitPlayerPoints(gameManager.AndGatePoints);
      }
   }


   public void SelectOrGate()
   {

      if (gameManager.GetPlayerPoints()>=gameManager.OrGatePoints)
      {
         gameManager.SpawnGate(GateType.Or);
         gameManager.DebitPlayerPoints(gameManager.OrGatePoints);
      }
   }
   public void SelectNorGate()
   {

      if (gameManager.GetPlayerPoints()>=gameManager.NorGatePoints)
      {
         gameManager.SpawnGate(GateType.Nor);
         gameManager.DebitPlayerPoints(gameManager.NorGatePoints);
      }
   }
   public void SelectXorGate()
   {
      if (gameManager.GetPlayerPoints()>=gameManager.XorGatePoints)
      {
         gameManager.SpawnGate(GateType.Xor);
         gameManager.DebitPlayerPoints(gameManager.XorGatePoints);
      }
   }
   public void SelectXNorGate()
   {

      if (gameManager.GetPlayerPoints()>=gameManager.XNorGatePoints)
      {
         gameManager.SpawnGate(GateType.XNor);
         gameManager.DebitPlayerPoints(gameManager.XNorGatePoints);
      }
   }
   public void SelectNandGate()
   {

      if (gameManager.GetPlayerPoints()>=gameManager.NAndGatePoints)
      {
         gameManager.SpawnGate(GateType.Nand);
         gameManager.DebitPlayerPoints(gameManager.NAndGatePoints);
      }
   }
   public void SelectNotGate()
   {
      if (gameManager.GetPlayerPoints()>=gameManager.NotGatePoints)
      {
         gameManager.SpawnGate(GateType.Not);
         gameManager.DebitPlayerPoints(gameManager.NotGatePoints);
      }
   }
   
   
  
}
