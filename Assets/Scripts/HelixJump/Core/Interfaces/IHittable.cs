using System;

namespace HelixJump.Core.Interfaces
{
    public interface IHittable
    {    
        void Hit(IHitInfo hitInfo);

    }
}