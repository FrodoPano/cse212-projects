/// <summary>
/// Maintain a Customer Service Queue.  Allows new customers to be 
/// added and allows customers to be serviced.
/// </summary>
public class CustomerService {
    public static void Run() {
        // Example code to see what's in the customer service queue:
        // var cs = new CustomerService(10);
        // Console.WriteLine(cs);

        // Test Cases

        // Test 1: Serve customer from empty queue
        // Scenario: Try to serve a customer when the queue is empty
        // Expected Result: Should display error message
        Console.WriteLine("Test 1 - Serve from empty queue");
        var cs = new CustomerService(5);
        cs.ServeCustomer(); // Should show error message
    
        // Defect(s) Found: No check for empty queue in ServeCustomer()

        Console.WriteLine("=================");

        // Test 2: Add customer and serve them
        // Scenario: Add one customer, then serve them
        // Expected Result: Should display the customer that was added
        Console.WriteLine("Test 2 - Add and serve one customer");
        cs = new CustomerService(5);
        cs.AddNewCustomer();
        cs.ServeCustomer();
    
        // Defect(s) Found: In ServeCustomer(), removes first then tries to access

        Console.WriteLine("=================");

        // Test 3: Test queue size enforcement
        // Scenario: Add more customers than max size
        // Expected Result: Should display error when trying to add beyond max size
        Console.WriteLine("Test 3 - Queue size enforcement");
        cs = new CustomerService(2);
        cs.AddNewCustomer();
        cs.AddNewCustomer();
        cs.AddNewCustomer(); // Should show "Maximum Number of Customers in Queue."
    
        // Defect(s) Found: Queue full check uses > instead of >=

        Console.WriteLine("=================");

        // Test 4: Test default size when invalid size provided
        // Scenario: Create queue with size 0 or negative
        // Expected Result: Should default to size 10
        Console.WriteLine("Test 4 - Default size for invalid input");
        cs = new CustomerService(0);
        Console.WriteLine($"Queue should have max_size=10: {cs}");

        Console.WriteLine("=================");

        // Test 5: Serve customers in correct order (FIFO)
        // Scenario: Add multiple customers, serve them
        // Expected Result: Should serve in the order they were added
        Console.WriteLine("Test 5 - Serve in FIFO order");
        cs = new CustomerService(5);
        cs.AddNewCustomer();
        cs.AddNewCustomer();
        Console.WriteLine($"Queue before serving: {cs}");
        cs.ServeCustomer(); // Should serve first customer
        cs.ServeCustomer(); // Should serve second customer
    }

    private readonly List<Customer> _queue = new();
    private readonly int _maxSize;

    public CustomerService(int maxSize) {
        if (maxSize <= 0)
            _maxSize = 10;
        else
            _maxSize = maxSize;
    }

    /// <summary>
    /// Defines a Customer record for the service queue.
    /// This is an inner class.  Its real name is CustomerService.Customer
    /// </summary>
    private class Customer {
        public Customer(string name, string accountId, string problem) {
            Name = name;
            AccountId = accountId;
            Problem = problem;
        }

        private string Name { get; }
        private string AccountId { get; }
        private string Problem { get; }

        public override string ToString() {
            return $"{Name} ({AccountId})  : {Problem}";
        }
    }

    /// <summary>
    /// Prompt the user for the customer and problem information.  Put the 
    /// new record into the queue.
    /// </summary>
    private void AddNewCustomer() {
        // Verify there is room in the service queue
        // FIXED: Changed > to >= to properly enforce max size
        if (_queue.Count >= _maxSize) {
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }

        Console.Write("Customer Name: ");
        var name = Console.ReadLine()!.Trim();
        Console.Write("Account Id: ");
        var accountId = Console.ReadLine()!.Trim();
        Console.Write("Problem: ");
        var problem = Console.ReadLine()!.Trim();

        // Create the customer object and add it to the queue
        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
    }

    /// <summary>
    /// Dequeue the next customer and display the information.
    /// </summary>
    private void ServeCustomer() {
        // FIXED: Added check for empty queue
        if (_queue.Count == 0) {
            Console.WriteLine("No customers in the queue.");
            return;
        }
        
        // FIXED: Get customer BEFORE removing from queue
        var customer = _queue[0];
        _queue.RemoveAt(0);
        Console.WriteLine(customer);
    }

    /// <summary>
    /// Support the WriteLine function to provide a string representation of the
    /// customer service queue object. This is useful for debugging. If you have a 
    /// CustomerService object called cs, then you run Console.WriteLine(cs) to
    /// see the contents.
    /// </summary>
    /// <returns>A string representation of the queue</returns>
    public override string ToString() {
        return $"[size={_queue.Count} max_size={_maxSize} => " + string.Join(", ", _queue) + "]";
    }
}