public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // Step 1: Create an array to hold the results with the specified length
        double[] results = new double[length];
    
        // Step 2: Loop through each position in the array
        for (int i = 0; i < length; i++)
        {
            // Step 3: Calculate the multiple: (i+1) * number
            // We use i+1 because we want multiples: 1st multiple, 2nd multiple, etc.
            results[i] = number * (i + 1);
        }
    
        // Step 4: Return the array containing all the multiples
        return results;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // Step 1: If amount equals list count or is 0, no rotation needed
        if (amount == 0 || amount == data.Count)
            return;
    
        // Step 2: Handle cases where amount might be larger than list count
        // Using modulo ensures amount is within valid range
        amount = amount % data.Count;
    
        // Step 3: If amount is 0 after modulo (when amount is multiple of data.Count), return
        if (amount == 0)
            return;
    
        // Step 4: Calculate split point - where the list will be divided
        int splitIndex = data.Count - amount;
    
        // Step 5: Get the portion that needs to move to the front (last 'amount' elements)
        List<int> endPart = data.GetRange(splitIndex, amount);
    
        // Step 6: Get the portion that needs to move to the back (first 'splitIndex' elements)
        List<int> startPart = data.GetRange(0, splitIndex);
    
        // Step 7: Clear the original list
        data.Clear();
    
        // Step 8: Add the rotated elements in correct order
        // First add the elements that were at the end (now moved to front)
        data.AddRange(endPart);
    
        // Step 9: Add the elements that were at the beginning (now moved to back)
        data.AddRange(startPart);

    }
}
