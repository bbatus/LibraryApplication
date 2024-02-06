using System.Linq;
using UnityEngine;
using System;

public class SearchManager //kitap arama
{
    private LibraryManager libraryManager;

    public SearchManager(LibraryManager libraryManager) //Dependency kullan
    {
        if (libraryManager == null)
            throw new ArgumentNullException(nameof(libraryManager), "libraryManager bos");
        this.libraryManager = libraryManager;
    }

    public Book SearchBook(string titleOrAuthor) //basliga ya da yazara göre arama yap! Önemli
    {
        if (string.IsNullOrEmpty(titleOrAuthor))
        {
            Debug.LogError("SearchBook bos cagirildi.");
            return null;
        }
        var book = libraryManager.ListBooks().FirstOrDefault(book => book.Title == titleOrAuthor || book.Author == titleOrAuthor);

        if (book != null)
        {
            Debug.Log($"Kitap bulundu: {book.Title} 'dan {book.Author}");
            return book;
        }
        else
        {
            Debug.LogWarning($"Bu baslikta/yazarda kitap yok: {titleOrAuthor}");
            return null;
        }
    }
}

