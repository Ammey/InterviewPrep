
public class RandomizedCollection
{
    Dictionary<int, List<int>> map;
    int[] set;
    int size;

    /** Initialize your data structure here. */
    public RandomizedCollection()
    {
        map = new Dictionary<int, List<int>>();
        set = new int[2];
        size = 0;
    }

    /** Inserts a value to the collection. Returns true if the collection did not already contain the specified element. */
    public bool Insert(int val)
    {
        size++;
        return false;
    }

    /** Removes a value from the collection. Returns true if the collection contained the specified element. */
    public bool Remove(int val)
    {
        return false;
    }

    /** Get a random element from the collection. */
    public int GetRandom()
    {
        return 0;
    }

    public static void Main()
    {
        RandomizedCollection obj = new RandomizedCollection();
        bool param_1 = obj.Insert(val);
        bool param_2 = obj.Remove(val);
        int param_3 = obj.GetRandom();
    }
}

/**
 * Your RandomizedCollection object will be instantiated and called as such:
 * RandomizedCollection obj = new RandomizedCollection();
 * bool param_1 = obj.Insert(val);
 * bool param_2 = obj.Remove(val);
 * int param_3 = obj.GetRandom();
 */
