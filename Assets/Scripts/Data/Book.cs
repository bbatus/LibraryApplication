using System;
[System.Serializable]
public class Book //Yalnızca veri tutacak
// Single Responsibility kullanılacak
{
    public string _title;
    public string _author;
    public int _isbn; 
    public int _copyCount;
    public int _borrowedCount;
    // Kitap nesnesi yaratılacağında zorunlu özellikler 
    // için constructor kullanacağız
    public Book(string title, string author, int isbn, int copyCount) 
    {
        this._title = title;
        this._author = author;
        this._isbn = isbn; 
        this._copyCount = copyCount;
        this._borrowedCount = 0; //başta hiç ödünç alınmadığını garanti edecek

        //hata blokları
        if (string.IsNullOrEmpty(title))
            throw new ArgumentException("Title boş olamaz", nameof(title));

        if (string.IsNullOrEmpty(author))
            throw new ArgumentException("Author boş olamaz", nameof(author));

        if (isbn <= 0) // ISBN negatif veya sıfır olamaz
            throw new ArgumentException("ISBN negatif veya 0 olamaz", nameof(isbn));

        if (copyCount < 0)
            throw new ArgumentException("Copy count negatif olamaz", nameof(copyCount));
    }
    //Encapsulation kullanacağız
    public string Title //Readonly
    {
        get { return _title; }
    }
    public string Author //Readonly
    {
        get { return _author; }
    }

    public int ISBN //Readonly
    {
        get { return _isbn; }
    }

    public int CopyCount //Readonly
    {
        get { return _copyCount; }
    }
    public int BorrowedCount // Hem okunabilir Hem yazılabilir
    {
        get { return _borrowedCount; }
        set { _borrowedCount = value; } // ileride ek kontrol ekleyebiliriz(0'dan düşükse hiç ulaşılamasın)
    }
}