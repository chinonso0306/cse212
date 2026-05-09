public static void RotateListRight(List<int> data, int amount)
{
    // Step 1: Get the items from the end of the list
    List<int> endPart = data.GetRange(data.Count - amount, amount);

    // Step 2: Get the items from the beginning of the list
    List<int> beginningPart = data.GetRange(0, data.Count - amount);

    // Step 3: Clear the original list
    data.Clear();

    // Step 4: Add the rotated sections back in order
    data.AddRange(endPart);
    data.AddRange(beginningPart);
}