public static double[] MultiplesOf(double startingNumber, int numberOfMultiples)
{
    // Step 1: Create an array with the required size
    double[] multiples = new double[numberOfMultiples];

    // Step 2: Loop through the array positions
    for (int i = 0; i < numberOfMultiples; i++)
    {
        // Step 3: Calculate the multiple
        // i + 1 is used because multiples start at 1, not 0
        multiples[i] = startingNumber * (i + 1);
    }

    // Step 4: Return the completed array
    return multiples;
}