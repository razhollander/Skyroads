using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoreDomain.Scripts.Utils.Command;
using Cysharp.Threading.Tasks;

public class SpawnAsteroidCommand : CommandSync<SpawnAsteroidCommand>
{
    private readonly IAsteroidsModule _asteroidsModule;

    public SpawnAsteroidCommand(IAsteroidsModule asteroidsModule)
    {
        _asteroidsModule = asteroidsModule;
    }
    public override void Execute()
    {
        
    }
}
