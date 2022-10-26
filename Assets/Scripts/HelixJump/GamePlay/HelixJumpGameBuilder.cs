using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using HelixJump.GamePlay.Interfaces;
using UnityEngine;

public class HelixJumpGameBuilder : IGameBuilder
{
    public async Task<IGame> BuildAsync()
    {
        return new HelixJumpGame();
    }

}
