    using System;
    using System.Collections.Generic;
    using BookClass;
    using LoanClass;
using System.Media;
    class Library
    {
        private List<Books> books;
        private List<Loan> loans;
        public Library()
        {
            books = new List<Books>();
            loans = new List<Loan>();
        }
    public void AddBook(string title, string author, int totalCopies)
        {
            Books newBook = new Books
            {
                Id = GenerateBookId(),
                Title = title,
                Author = author,
                TotalCopies = totalCopies,
                AvailableCopies = totalCopies
            };
            books.Add(newBook);
            Console.WriteLine($"Kitap eklenmiştir. Kitap ID: {newBook.Id}");
        }

        public void ListBooks()
        {
        if (books.Count == 0)
        {
            Console.WriteLine("Listelenecek Kitap Yok :(");
        }
        else
        {
            Console.WriteLine("Kitap Listesi:");
            Console.WriteLine("ID\tBaşlık\tYazar\tToplam Kopya\tKullanılabilir Kopya");
            foreach (var book in books)
            {
                Console.WriteLine($"{book.Id}\t{book.Title}\t{book.Author}\t{book.TotalCopies}\t{book.AvailableCopies}");
            }
        }
        }
        public void SearchBooks(string searchTerm)
        {
        bool SearchItemAvailable = false;
        if (SearchItemAvailable)
        {
            Console.WriteLine("ID\tBaşlık\tYazar\tToplam Kopya\tKullanılabilir Kopya");
            Console.WriteLine($"Arama Sonuçları ({searchTerm}):");
        }
        else
        {
            Console.WriteLine("Aradığınız Kitap Bulunanamaktadır" + "\tLütfen İsmi Kontrol Edin ve Tekrar Deneyin");
        }
       
        foreach (var book in books)
            {
                if (book.Title.Contains(searchTerm) || book.Author.Contains(searchTerm))
                {
                    Console.WriteLine($"{book.Id}\t{book.Title}\t{book.Author}\t{book.TotalCopies}\t{book.AvailableCopies}");
                    SearchItemAvailable = true;
                }
            }
        }

        public void BorrowBook(int memberId, int bookId)
        {
            Books book = books.Find(b => b.Id == bookId);
            if (book != null && book.AvailableCopies > 0)
            {
                Loan newLoan = new Loan
                {
                    MemberId = memberId,
                    BookId = bookId,
                    DueDate = DateTime.Now.AddSeconds(10) // Örnek olarak 14 gün
                };
                loans.Add(newLoan);
                book.AvailableCopies--;
                Console.WriteLine($"Kitap başarıyla ödünç alındı. Teslim tarihi: {newLoan.DueDate}");
            }
            else
            {
                Console.WriteLine("Kitap ödünç alınamadı. Kopya sayısı sıfır.");
            }
        }

        public void ReturnBook(int memberId, int bookId)
        {
            Loan loan = loans.Find(l => l.MemberId == memberId && l.BookId == bookId);
            Books book = books.Find(b => b.Id == bookId);

            if (loan != null && book != null)
            {
                loans.Remove(loan);
                book.AvailableCopies++;
                Console.WriteLine("Kitap başarıyla iade edildi.");
            }
            else
            {
                Console.WriteLine("Kitap iade edilemedi.");
            }
        }

        public void DisplayOverdueBooks()
        {
            bool BookAvailable = false;
            if (BookAvailable)
            {
              Console.WriteLine("Süresi geçmiş kitaplar:");
            }
            else
            {
              Console.WriteLine("Süresi Geçmiş Kitap Bulunmamaktadır");
            }
        foreach (var loan in loans)
            {
                if (loan.DueDate < DateTime.Now)
                {
                    Books book = books.Find(b => b.Id == loan.BookId);
                    Console.WriteLine($"{book.Title} (Teslim Tarihi: {loan.DueDate})");
                    BookAvailable = true;
                }
            }
        }

        private int GenerateBookId()
        {
            return books.Count + 1;
        }
    }

    class ConsoleUI
    {
        public static string GetInput(string prompt)
        {
            Console.Write(prompt + ": ");
            return Console.ReadLine();
        }
    }

    class Program
    {
    public static void PrintAnim(string T , int Speed)
    {
        // Ses Efekti ve Yazı Efekti
        SoundPlayer soundPlayer = new SoundPlayer("SesEfekti.wav");
        soundPlayer.PlaySync();
        foreach (var t in T)
        {
            Console.Write(t);
            System.Threading.Thread.Sleep(Speed);
        }
        soundPlayer.Stop();
        Console.WriteLine();
    }
        static void Main(string[] args)
        {
          
        Console.ForegroundColor = ConsoleColor.Red;
        PrintAnim(new string('=', 40) , 10);
        PrintAnim("========= Kütüphane Otomasyonu =========" , 10);
        PrintAnim(new string('=', 40), 10);
        Console.ResetColor();
        Library library = new Library();
            while (true)
            {
                Console.WriteLine("1. Kitap Ekle");
                Console.WriteLine("2. Kitapları Listele");
                Console.WriteLine("3. Kitap Ara");
                Console.WriteLine("4. Kitap Ödünç Al");
                Console.WriteLine("5. Kitap İade Et");
                Console.WriteLine("6. Süresi Geçmiş Kitapları Görüntüle");
                Console.WriteLine("7. Çıkış");

                string choice = ConsoleUI.GetInput("Yapmak istediğiniz işlemi seçin");

                switch (choice)
                {
                    case "1":
                        string title = ConsoleUI.GetInput("Kitap Başlığı");
                        string author = ConsoleUI.GetInput("Kitap Yazarı");
                        int totalCopies = int.Parse(ConsoleUI.GetInput("Toplam Kopya Sayısı"));
                        library.AddBook(title, author, totalCopies);
                        break;

                    case "2":
                        library.ListBooks();
                        break;

                    case "3":
                        string searchTerm = ConsoleUI.GetInput("Aranacak Kelime");
                        library.SearchBooks(searchTerm);
                        break;

                    case "4":

                        int memberId = int.Parse(ConsoleUI.GetInput("Üye ID"));
                        int borrowBookId = int.Parse(ConsoleUI.GetInput("Ödünç Alınacak Kitap ID"));
                        library.BorrowBook(memberId, borrowBookId);
                        break;

                    case "5":
                        int returnMemberId = int.Parse(ConsoleUI.GetInput("Üye ID"));
                        int returnBookId = int.Parse(ConsoleUI.GetInput("İade Edilecek Kitap ID"));
                        library.ReturnBook(returnMemberId, returnBookId);
                        break;

                    case "6":
                        library.DisplayOverdueBooks();
                        
                        break;

                    case "7":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Geçersiz seçim.");
                        break;
                }
            }
        }
    }

