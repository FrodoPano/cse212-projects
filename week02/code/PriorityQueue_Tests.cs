using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Add items with different priorities: A(1), B(2), C(3)
    // Expected Result: Dequeue should return C, then B, then A
    // Defect(s) Found: Dequeue doesn't actually remove items from the queue
    public void TestPriorityQueue_BasicPriorityOrder()
    {
        var priorityQueue = new PriorityQueue();
        
        // Enqueue items with different priorities
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 2);
        priorityQueue.Enqueue("C", 3);
        
        // Dequeue should return highest priority first
        Assert.AreEqual("C", priorityQueue.Dequeue());
        Assert.AreEqual("B", priorityQueue.Dequeue());
        Assert.AreEqual("A", priorityQueue.Dequeue());
        
        // Queue should be empty now
        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Should have thrown exception for empty queue");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
    }

    [TestMethod]
    // Scenario: Add items with same priorities: A(2), B(2), C(2)
    // Expected Result: Dequeue should return A, then B, then C (FIFO for same priority)
    // Defect(s) Found: Loop condition in Dequeue is incorrect (index < _queue.Count - 1)
    //                  This causes it to miss checking the last item
    public void TestPriorityQueue_SamePriorityFIFO()
    {
        var priorityQueue = new PriorityQueue();
        
        // Enqueue items with same priority
        priorityQueue.Enqueue("A", 2);
        priorityQueue.Enqueue("B", 2);
        priorityQueue.Enqueue("C", 2);
        
        // Dequeue should return in FIFO order for same priority
        Assert.AreEqual("A", priorityQueue.Dequeue());
        Assert.AreEqual("B", priorityQueue.Dequeue());
        Assert.AreEqual("C", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Add items with mixed priorities: A(1), B(3), C(2), D(3), E(1)
    // Expected Result: Dequeue should return B (first highest priority), then D, then C, then A, then E
    // Defect(s) Found: When finding highest priority, uses ">=" which picks the last highest priority
    //                  instead of the first one (FIFO for same priority)
    public void TestPriorityQueue_MixedPriorities()
    {
        var priorityQueue = new PriorityQueue();
        
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 3);
        priorityQueue.Enqueue("C", 2);
        priorityQueue.Enqueue("D", 3);  // Same priority as B, should come after B
        priorityQueue.Enqueue("E", 1);
        
        // B and D both have priority 3, B should come first (FIFO)
        Assert.AreEqual("B", priorityQueue.Dequeue());
        Assert.AreEqual("D", priorityQueue.Dequeue());  // Next highest priority 3
        Assert.AreEqual("C", priorityQueue.Dequeue());  // Priority 2
        Assert.AreEqual("A", priorityQueue.Dequeue());  // Priority 1 (FIFO with E)
        Assert.AreEqual("E", priorityQueue.Dequeue());  // Priority 1
    }

    [TestMethod]
    // Scenario: Test empty queue
    // Expected Result: Should throw InvalidOperationException with message "The queue is empty."
    // Defect(s) Found: Exception message doesn't match requirement ("The queue is empty." vs "No one in the queue.")
    // Note: Actually the Dequeue method throws the correct message, but let's verify
    public void TestPriorityQueue_EmptyQueue()
    {
        var priorityQueue = new PriorityQueue();
        
        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Should have thrown exception for empty queue");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (Exception)
        {
            Assert.Fail("Wrong exception type thrown");
        }
    }

    [TestMethod]
    // Scenario: Single item in queue
    // Expected Result: Should return that item
    // Defect(s) Found: No specific defect expected, but good boundary test
    public void TestPriorityQueue_SingleItem()
    {
        var priorityQueue = new PriorityQueue();
        
        priorityQueue.Enqueue("Only", 5);
        Assert.AreEqual("Only", priorityQueue.Dequeue());
        
        // Verify queue is empty
        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Should have thrown exception for empty queue");
        }
        catch (InvalidOperationException) { }
    }

    [TestMethod]
    // Scenario: Add items with negative priorities
    // Expected Result: Should still work correctly
    // Defect(s) Found: No specific defect, but tests edge cases
    public void TestPriorityQueue_NegativePriorities()
    {
        var priorityQueue = new PriorityQueue();
        
        priorityQueue.Enqueue("Low", -5);
        priorityQueue.Enqueue("Medium", 0);
        priorityQueue.Enqueue("High", 5);
        
        Assert.AreEqual("High", priorityQueue.Dequeue());  // Priority 5
        Assert.AreEqual("Medium", priorityQueue.Dequeue()); // Priority 0
        Assert.AreEqual("Low", priorityQueue.Dequeue());    // Priority -5
    }
}