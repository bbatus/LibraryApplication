using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibrarySystemController : MonoBehaviour //Facade yaklaşımı kullan
{
    private LibraryManager libraryManager;
    private SearchManager searchManager;
    private BorrowingManager borrowingManager;
    private void Awake()
    {
        Debug.Log("Awake basladi...");
        libraryManager = new LibraryManager();
        searchManager = new SearchManager(libraryManager);
        borrowingManager = new BorrowingManager(libraryManager);
        Debug.Log("Managerlar yaratildi.");
    }
    void Start()
    {
        // Book sampleBook = new Book("Baslik", "Yazar", 123456789, 3);
        // libraryManager.AddBook(sampleBook);
        libraryManager.LoadBooksFromFile();
        libraryManager.SaveBooksToFile();
    }

    public void AddBookUI(string title, string author, int isbn, int copyCount) 
    {
        Book newBook = new Book(title, author, isbn, copyCount); 
        libraryManager.AddBook(newBook);
    }

    public void BorrowBookUI(int isbn) 
    {
        if (borrowingManager.BorrowBook(isbn)) 
        {
            Debug.Log("Kitap ödünç alindi: " + isbn);
        }
        else
        {
            Debug.Log("Kitap ödünç alinamadi: " + isbn);
        }
    }

    public void ReturnBookUI(int isbn) 
    {
        if (borrowingManager.ReturnBook(isbn))
        {
            Debug.Log("Kitap iade edildi: " + isbn);
        }
        else
        {
            Debug.Log("Kitap iade edilemedi: " + isbn);
        }
    }

    public void SearchBookUI(string titleOrAuthor)
    {
        var book = searchManager.SearchBook(titleOrAuthor);
        if (book != null)
        {
            Debug.Log($"Kitap bulundu: {book.Title} by {book.Author}");
        }
        else
        {
            Debug.Log("Kitap bulunamadi.");
        }
    }

}