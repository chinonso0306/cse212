public static void RotateListRight(List<int> data, int amount)
{
    // PLAN:
    // 1. If amount equals list size, list stays the same
    // 2. Split list into two parts:
    //      - last 'amount' elements
    //      - first part before them
    // 3. Clear original list
    // 4. Rebuild list in rotated order

    int n = data.Count;

    // Edge case: no rotation needed
    if (amount == 0 || amount == n)
    {
        return;
    }

    // Step 1: get last part
    List<int> endPart = data.GetRange(n - amount, amount);

    // Step 2: get beginning part
    List<int> beginningPart = data.GetRange(0, n - amount);

    // Step 3: reset list
    data.Clear();

    // Step 4: rebuild in rotated order
    data.AddRange(endPart);
    data.AddRange(beginningPart);
}