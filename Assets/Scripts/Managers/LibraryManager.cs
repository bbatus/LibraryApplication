using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using System.Linq;
using UnityEditor;
[System.Serializable]

public class LibraryManager //genel library manager //dependency injection sağlayacak
{
    private List<Book> _books; 

    public LibraryManager()
    {
        _books = new List<Book>();
        LoadBooksFromFile(); // LM'nin constructoru.
    }

    public void AddBook(Book book) //kitap ekleme metodu /Jsona ekleme metodu calistir!
    {
        if (book == null)
            throw new ArgumentNullException(nameof(book), "Kitap giriniz");

        _books.Add(book);
        SaveBooksToFile(); // Kitap eklendikten sonra listeyi güncelleyip kaydeder
        Debug.Log($"Kitap eklendi: {book.Title}");
    }

    public List<Book> ListBooks()
    {
        return _books;
    }

    public void SaveBooksToFile()
    {
        string desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
        string filePath = Path.Combine(desktopPath, "books.json");

        string json = JsonUtility.ToJson(new BooksWrapper { Books = _books }, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Kitaplar jsona kaydedildi: " + filePath);
    }

    [System.Serializable]
    class BooksWrapper //jsonda serilestirme
    {
        public List<Book> Books;
    }

    public void LoadBooksFromFile()
    {
        string desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
        string filePath = Path.Combine(desktopPath, "books.json");
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            BooksWrapper booksWrapper = JsonUtility.FromJson<BooksWrapper>(json);
            _books = booksWrapper.Books;
            Debug.Log("Book.json yüklendi.");
        }
        else
        {
            Debug.LogWarning("Book.json yüklenemedi.");
        }
    }
}
