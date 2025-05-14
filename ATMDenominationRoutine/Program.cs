class Program
{
    static int[] amounts = [30, 50, 60, 80, 100, 140, 230, 370, 610, 980];
    // If we put amounts like 5, 53 should throw no valid combinations found message

    static void Main()
    {
        foreach (int amount in amounts)
        {
            Console.WriteLine($"Payout: {amount} EUR");

            bool hasCombination = false;

            for (int count100 = 0; count100 <= amount / 100; count100++) {
                for (int count50 = 0; count50 <= amount / 50; count50++) {
                    for (int count10 = 0; count10 <= amount / 10; count10++) {
                        int total = count100 * 100 + count50 * 50 + count10 * 10;
                        if (total == amount) {
                            Console.Write("  - ");
                            if (count10 > 0) Console.Write($"{count10} x 10 EUR ");
                            if (count50 > 0) Console.Write($"{count50} x 50 EUR ");
                            if (count100 > 0) Console.Write($"{count100} x 100 EUR");
                            Console.WriteLine();
                            hasCombination = true;
                        }
                    }
                }
            }

            if (!hasCombination)
            {
                Console.WriteLine("No valid combinations found");
            }

            Console.WriteLine();
        }
    }
}
