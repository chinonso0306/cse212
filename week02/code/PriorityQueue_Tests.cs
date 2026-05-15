using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue several items with different priorities and ensure the highest priority is returned.
    // Expected Result: [Item with Priority 10], [Item with Priority 5], [Item with Priority 2]
    // Defect(s) Found: If the code is not fixed, the Dequeue function might not actually remove the 
    // item from the list, causing it to return the same high-priority item every time Dequeue is called.
    public void TestPriorityQueue_HighPriority()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Low", 2);
        priorityQueue.Enqueue("High", 10);
        priorityQueue.Enqueue("Medium", 5);

        Assert.AreEqual("High", priorityQueue.Dequeue());
        Assert.AreEqual("Medium", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue multiple items with the same highest priority.
    // Expected Result: The item added FIRST (FIFO) should be returned first.
    // Defect(s) Found: The original code often uses a loop with a '>=' comparison. This causes 
    // the queue to return the LAST item added with that priority instead of the FIRST.
    public void TestPriorityQueue_FIFO_Tie()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("First-High", 10);
        priorityQueue.Enqueue("Second-High", 10);
        priorityQueue.Enqueue("Third-High", 10);

        Assert.AreEqual("First-High", priorityQueue.Dequeue());
        Assert.AreEqual("Second-High", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Attempt to Dequeue from an empty queue.
    // Expected Result: Should throw an InvalidOperationException with the message "The queue is empty."
    // Defect(s) Found: The code may throw a generic ArgumentOutOfRangeException or fail to 
    // provide the specific error message required by the instructions.
    public void TestPriorityQueue_Empty()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
    }
}
 likely looks like this:
```csharp
if (nodes[index].Priority >= nodes[highPriorityIndex].Priority) // The BUG is the '='
    highPriorityIndex = index;