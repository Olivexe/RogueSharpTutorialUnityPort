using System;
using System.Collections.Generic;
using RogueSharp.Random;

namespace RogueSharpTutorial.Utilities
{
    public class GamePool<T>
    {
        private readonly List<PoolItem<T>>  poolItems;
        private static readonly IRandom     random          = new DotNetRandom();
        private int                         totalWeight;

        public GamePool()
        {
            poolItems = new List<PoolItem<T>>();
        }

        public T Get()
        {
            int runningWeight   = 0;
            int roll            = random.Next(1, totalWeight);

            foreach (var poolItem in poolItems)
            {
                runningWeight += poolItem.Weight;
                if (roll <= runningWeight)
                {
                    Remove(poolItem);
                    return poolItem.Item;
                }
            }

            throw new InvalidOperationException("Could not get an item from the pool");
        }

        public void Add(T item, int weight)
        {
            poolItems.Add(new PoolItem<T> { Item = item, Weight = weight });
            totalWeight += weight;
        }

        public void Remove(PoolItem<T> poolItem)
        {
            poolItems.Remove(poolItem);
            totalWeight -= poolItem.Weight;
        }
    }

    public class PoolItem<T>
    {
        public int  Weight  { get; set; }
        public T    Item    { get; set; }
    }
}
