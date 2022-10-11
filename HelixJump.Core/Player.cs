using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


    public interface IPlayer
    {
        
    }

    public interface ITowerLayer
    {
        public Resolution Resolution { get; }
    }

    public struct Resolution
    {
        public int Value { get; }
        
        public Resolution(int value)
        {
            Value = value;
        }
    }
    
    public interface ITowerLayerPart
    {}
    
    public class TowerLayer : ITowerLayer
    {
        public IEnumerable<ITowerLayerPart> LayerParts { get; }
        public Resolution Resolution { get; }

        public TowerLayer(Resolution resolution, IEnumerable<ITowerLayerPart> layerParts)
        {
            Resolution = resolution;
            
            var towerLayerParts = layerParts as ITowerLayerPart[] ?? layerParts.ToArray();
            
            if (towerLayerParts.Length != Resolution.Value)
                throw new IndexOutOfRangeException($"Enumerable: {layerParts.GetType()} does not match the tower resolution of tower {GetType()}");
            
            LayerParts = towerLayerParts;
        }
        
        
    }
    
    public interface ITower
    {
        
    }
    
    public interface ITowerBuilder
    {
        public Task<ITower> BuildTowerAsync();
    }
    
    public class Player
    {
        private float _xPosition;
        public float XPosition
        {
            get => _xPosition;
            private set
            {
                if (value > 1)
                    throw new IndexOutOfRangeException("Player position cannot be bigger than 1f");
                
                _xPosition = value;
            }
        }
        
        public int RowPosition { get; private set; }
    }
