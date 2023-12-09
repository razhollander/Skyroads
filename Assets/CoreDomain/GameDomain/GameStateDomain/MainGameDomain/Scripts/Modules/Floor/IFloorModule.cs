using UnityEngine;

public interface IFloorModule
{
    public Transform FloorStartPoint { get; }
    void CreateFloor();
    public void StartMovement();
    public void StopMovement();
}