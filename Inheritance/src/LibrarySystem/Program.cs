namespace LibrarySystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region  1 - Compile-Time Restrictions (Must NOT Compile)
            // must NOT compile: Person cannot be created directly.
            // Person person = new Person(...);

            // must NOT compile: Member cannot be created directly.
            // Member member = new Member(...);

            // must NOT compile: Staff cannot be created directly.
            // Staff staff = new Staff(...);

            // must NOT compile: LibraryItem cannot be created directly.
            // LibraryItem item = new LibraryItem(...);

            // must NOT compile: FullName is read-only.
            // member.FullName = "New Name";

            // must NOT compile: Loans is read-only.
            // member.Loans.Add(loan);

            // must NOT compile: IsOnLoan cannot be set from outside.
            // item.IsOnLoan = true;
            #endregion

            #region 2 - Invalid Borrowing Tests

            // Test 1: Borrow a withdrawn item.
            Console.WriteLine("\n--- Test 1: Borrow a Withdrawn Item ---");

            // Creates a student member.
            StudentMember student1 = new StudentMember(
                1,
                "Ahmed",
                "0501234567");

            // Creates a book.
            Book withdrawnBook = new Book(
                101,
                "C# Programming",
                5m);

            // Withdraws the book from the library.
            withdrawnBook.Withdraw();

            // Attempts to borrow the withdrawn book.
            try
            {
                student1.Borrow(
                    withdrawnBook,
                    1001,
                    new DateTime(2026, 10, 9));
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Rejected: {ex.Message}");
            }


            // Test 2: Borrow an item that is already on loan.
            Console.WriteLine("\n--- Test 2: Borrow an Item Already on Loan ---");

            // Creates two student members.
            StudentMember student2 = new StudentMember(
                2,
                "Omar",
                "0501111111");

            StudentMember student3 = new StudentMember(
                3,
                "Ali",
                "0502222222");

            // Creates a book.
            Book borrowedBook = new Book(
                102,
                "Object-Oriented Programming",
                5m);

            // The first student borrows the book successfully.
            student2.Borrow(
                borrowedBook,
                1002,
                new DateTime(2026, 10, 9));

            // The second student attempts to borrow the same book.
            try
            {
                student3.Borrow(
                    borrowedBook,
                    1003,
                    new DateTime(2026, 10, 9));
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Rejected: {ex.Message}");
            }

            // Test 3: Student attempts to borrow a fourth item.
            Console.WriteLine("\n--- Test 3: Student Borrows a Fourth Item ---");

            // Creates a student member.
            StudentMember student4 = new StudentMember(
                4,
                "Mahmoud",
                "0503333333");

            // Creates four different books.
            Book book1 = new Book(201, "C# Basics", 5m);
            Book book2 = new Book(202, "OOP Basics", 5m);
            Book book3 = new Book(203, "Data Structures", 5m);
            Book book4 = new Book(204, "Algorithms", 5m);

            // The student borrows the first three books successfully.
            student4.Borrow(book1, 2001, new DateTime(2026, 10, 9));
            student4.Borrow(book2, 2002, new DateTime(2026, 10, 9));
            student4.Borrow(book3, 2003, new DateTime(2026, 10, 9));

            // The fourth borrowing attempt must be rejected.
            try
            {
                student4.Borrow(book4, 2004, new DateTime(2026, 10, 9));
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Rejected: {ex.Message}");
            }

            #endregion

            #region 3 - Staff Polymorphism Test

            // Creates a list that can store different types of staff.
            List<Staff> staffMembers = new List<Staff>
                {
                    // Creates a librarian.
                    new Librarian(
                        10,
                        "Ahmed",
                        "0501111111",
                        new DateTime(2020, 1, 1),
                        5000m),

                    // Creates a head librarian.
                    new HeadLibrarian(
                        11,
                        "Omar",
                        "0502222222",
                        new DateTime(2019, 1, 1),
                        7000m),

                    // Creates a shelver.
                    new Shelver(
                        12,
                        "Ali",
                        "0503333333",
                        new DateTime(2021, 1, 1),
                        3500m,
                        "Science")
                };

            // Prints the monthly pay of each staff member.
            Console.WriteLine("\n--- Test 4: Staff Polymorphism ---");

            foreach (Staff staff in staffMembers)
            {
                Console.WriteLine(
                    $"{staff.FullName}: {staff.CalculateMonthlyPay():N2} SAR");
            }

            #endregion

            #region 4 - Library Item Polymorphism Test

            // Creates a list that can store different library item types.
            List<LibraryItem> items = new List<LibraryItem>();

            // Creates a book, a DVD, and a magazine.
            Book book = new Book(301, "C# Programming", 5m);
            DVD dvd = new DVD(302, "Learning C#", 5m);
            Magazine magazine = new Magazine(303, "Technology Today", 5m);

            // Adds all items to the same list.
            items.Add(book);
            items.Add(dvd);
            items.Add(magazine);

            // Prints the loan period and daily late fee of each item.
            Console.WriteLine("\n--- Test 4: Library Item Polymorphism ---");

            foreach (LibraryItem item in items)
            {
                Console.WriteLine($"Title: {item.Title}");
                Console.WriteLine($"Loan Period: {item.LoanPeriodDays} days");
                Console.WriteLine($"Daily Late Fee: {item.GetDailyLateFee():0.00}");
                Console.WriteLine();
            }

            #endregion

            #region 5 - Premium Member Late Return Test

            // Creates a premium member with a 10% late fee discount.
            PremiumMember premiumMember = new PremiumMember(
                5,
                "Khaled",
                "0504444444",
                10m);

            // Creates a DVD with a 7-day loan period.
            DVD lateDVD = new DVD(
                401,
                "C# Advanced",
                5m);

            // Sets the borrowing date.
            DateTime borrowDate = new DateTime(2026, 10, 1);

            // Borrows the DVD.
            Loan premiumLoan = premiumMember.Borrow(
                lateDVD,
                4001,
                borrowDate);

            // Returns the DVD 5 days after the due date.
            DateTime returnDate = premiumLoan.DueDate.AddDays(5);
            premiumLoan.Return(returnDate);

            // Prints the due date, late fee, and reading points.
            Console.WriteLine("\n--- Test 5: Premium Member Late Return ---");
            Console.WriteLine($"Due Date: {premiumLoan.DueDate:yyyy-MM-dd}");
            Console.WriteLine($"Return Date: {returnDate:yyyy-MM-dd}");
            Console.WriteLine($"Late Fee: {premiumLoan.LateFee:0.00}");
            Console.WriteLine($"Reading Points: {premiumMember.ReadingPoints}");

            #endregion

            #region 6 - Invalid Loan Operation Tests

            // Creates a student member.
            StudentMember student5 = new StudentMember(
                6,
                "Youssef",
                "0505555555");

            // Creates a book.
            Book returnedBook = new Book(
                501,
                "C# Fundamentals",
                5m);

            // Borrows the book.
            Loan returnedLoan = student5.Borrow(
                returnedBook,
                5001,
                new DateTime(2026, 10, 1));

            // Returns the loan successfully.
            returnedLoan.Return(new DateTime(2026, 10, 5));

            // Test 1: Attempts to return the same loan again.
            Console.WriteLine("\n--- Test 6.1: Return the Same Loan Twice ---");

            try
            {
                returnedLoan.Return(new DateTime(2026, 10, 6));
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Rejected: {ex.Message}");
            }

            // Test 2: Attempts to mark a returned loan as lost.
            Console.WriteLine("\n--- Test 6.2: Mark a Returned Loan as Lost ---");

            try
            {
                returnedLoan.MarkAsLost();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Rejected: {ex.Message}");
            }

            #endregion



        }
    }
}
