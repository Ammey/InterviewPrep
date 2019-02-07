
using System;
using System.Collections.Generic;
using System.Linq;

public class RandomizedCollection
{
    readonly Dictionary<int, SortedSet<int>> map;
    int[] numArr;
    int size;
    Random rand;

    /** Initialize your data structure here. */
    public RandomizedCollection()
    {
        map = new Dictionary<int, SortedSet<int>>();
        numArr = new int[2];
        size = 0;
        rand = new Random();
    }

    /** Inserts a value to the collection. Returns true if the collection did not already contain the specified element. */
    public bool Insert(int val)
    {
        if (numArr.Length == size)
        {
            ResizeSet(size * 2);
        }

        numArr[size] = val;
        size++;

        if (map.ContainsKey(val))
        {
            var list = map[val];
            list.Add(size - 1);
            map[val] = list;
            return false;
        }
        else
        {
            map.Add(val, new SortedSet<int> { size - 1 });
            return true;
        }
    }

    /** Removes a value from the collection. Returns true if the collection contained the specified element. */
    public bool Remove(int val)
    {
        if(map.ContainsKey(val))
        {
            var set = map[val];
            var location = set.Last();
            set.Remove(location);

            if (set.Count == 0)
            {
                map.Remove(val);
            }

            if(location != size - 1)
            {
                var lastVal = numArr[size - 1];
                var lastSet = map[lastVal];
                lastSet.Remove(lastSet.Last());
                lastSet.Add(location);
                map[lastVal] = lastSet;

                numArr[location] = lastVal;
            }
            numArr[size - 1] = 0;
            size--;
            return true;
        }

        return false;
    }

    /** Get a random element from the collection. */
    public int GetRandom()
    {
        var num = rand.Next(size);
        return numArr[num];
    }

    public void ResizeSet(int size)
    {
        Array.Resize(ref this.numArr, size);
    }

    public static void Main()
    {
        RandomizedCollection obj = new RandomizedCollection();
        bool param_1 = obj.Insert(10);
        param_1 = obj.Insert(10);
        param_1 = obj.Insert(20);
        param_1 = obj.Insert(20);
        param_1 = obj.Insert(30);
        param_1 = obj.Insert(30);
        param_1 = obj.Remove(10);
        param_1 = obj.Remove(10);
        param_1 = obj.Remove(30);
        param_1 = obj.Remove(30);
        Console.WriteLine(obj.GetRandom());
        Console.WriteLine(obj.GetRandom());
        Console.WriteLine(obj.GetRandom());
        Console.WriteLine(obj.GetRandom());
    }
}

/**
 * Your RandomizedCollection object will be instantiated and called as such:
 * RandomizedCollection obj = new RandomizedCollection();
 * bool param_1 = obj.Insert(val);
 * bool param_2 = obj.Remove(val);
 * int param_3 = obj.GetRandom();
 */
