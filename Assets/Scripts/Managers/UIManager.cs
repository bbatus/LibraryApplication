using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UIManager : MonoBehaviour
{
    public LibraryManager libraryManager;
    public BorrowingManager borrowingManager;
    public SearchManager searchManager;

    #region addbook
    [SerializeField] private InputField titleInputField;
    [SerializeField] private InputField authorInputField;
    [SerializeField] private InputField isbnInputField;
    [SerializeField] private InputField copyCountInputField;
    [SerializeField] private Text booksListText;
    #endregion

    [SerializeField] private InputField searchInputField;
    [SerializeField] private InputField borrowIsbnInputField;
    public void ClickedAddBook()
    {
        string title = titleInputField.text;
        string author = authorInputField.text;
        int isbn = int.Parse(isbnInputField.text);
        int copyCount = int.Parse(copyCountInputField.text);

        Book newBook = new Book(title, author, isbn, copyCount);
        libraryManager.AddBook(newBook);

        Debug.Log("Kitap eklendi: " + title);
    }

    public void ClickedListBooks()
    {
        string booksList = string.Join("\n", libraryManager.ListBooks().Select(book => "Baslik :  " + book.Title + " /" + " Yazar : " +
         book.Author + " /"  + "Isbn Numarasi " +  book.ISBN + " /"  + " Odunc sayisi "+ book.BorrowedCount + " "   + " /" +" Kopya sayisi" +  book.CopyCount ));
        booksListText.text = booksList;
    }

    public void ClickedSearchBook()
    {
        string titleOrAuthor = searchInputField.text;
        if (string.IsNullOrEmpty(titleOrAuthor))
        {
            Debug.LogError("Arama icin baslik veya yazar girilmedi.");
            return;
        }

        var book = searchManager.SearchBook(titleOrAuthor);

        if (book != null)
        {
            Debug.Log($"Kitap bulundu: {book.Title} - {book.Author}");
        }
        else
        {
            Debug.LogWarning("Kitap bulunamadı.");
        }
    }

    public void ClickedBorrowBook()
    {
        int isbn = int.Parse(borrowIsbnInputField.text);

        bool success = borrowingManager.BorrowBook(isbn);

        if (success)
        {
            Debug.Log($"Kitap başarıyla ödünç alındı: ISBN {isbn}");
        }
        else
        {
            Debug.LogWarning($"Kitap ödünç alınamadı: ISBN {isbn}");
        }
    }
}
