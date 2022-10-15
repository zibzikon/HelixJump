using System;

namespace HelixJump.Core.Interfaces
{
    public interface IHittable
    {    
        public void Hit(IHitInfo hitInfo);

    }
}