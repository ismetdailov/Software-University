namespace Book.Tests
{
    using System;

    using NUnit.Framework;
    [TestFixture]
    public class Tests
    {
        private Book Book;
        [SetUp]
        public void SetUp()
        {
            Book = new Book("name", "author");
        }

        [Test]
        public void CtorInitializesCollectionOfPresents()
        {
            Assert.That(Book, Is.Not.Null);
        }
        [Test]
        public void ThrowsAIsNull()
        {
            Book nullPresent = null;

            Assert.Throws<ArgumentException>(() => new Book(null, "neshto"));
        }
        [Test]
        public void ThrowsANull()
        {
            Book nullPresent = null;

            Assert.Throws<ArgumentException>(() => new Book("", "neshto"));
        }
        [Test]
        public void ThrowsAnExceptionWhenAuthorsNull()
        {
            Book nullPresent = null;

            Assert.Throws<ArgumentException>(() => new Book("neshto", null));
        }
        [Test]
        public void ThrowsAnExceptionAuthorsNull()
        {
            Book nullPresent = null;

            Assert.Throws<ArgumentException>(() => new Book("neshto", ""));
        }
        [Test]
        public void ThrowExceptionAuthorsNull()
        {
            Book = new Book("name", "author");

            Assert.AreEqual(Book.BookName , "name");
        }
        [Test]
        public void ThrowExceptionAuthorsl()
        {
            Book = new Book("name", "author");

            Assert.AreEqual(Book.Author, "author");

        }
        [Test]
        public void TfootneteCountonAuthorsl()
        {
            Book = new Book("name", "author");
            Book.AddFootnote(5, "str");
            Assert.AreEqual(Book.FootnoteCount, 1);

        }
        [Test]
        public void TfootneteCoasdauntonAuthorsl()
        {
            Book = new Book("name", "author");
            Book.AddFootnote(5, "str");
            Assert.Throws<InvalidOperationException>(() => Book.AddFootnote(5, "st"));
        }
        [Test]
        public void TfootneteCoasdauntsadasdonAuthorsl()
        {
            Book = new Book("name", "author");
            Book.AddFootnote(5, "str");
            Book.AddFootnote(6, "sr");
            Assert.Throws<InvalidOperationException>(() => Book.FindFootnote(7));
        }
        [Test]
        public void TfootneteauntsadasdonAuthorsl()
        {
            Book = new Book("name", "author");
            Book.AddFootnote(5, "str");
            Book.AddFootnote(6, "sr");
            Assert.AreEqual(Book.FindFootnote(6), $"Footnote #6: sr");
        }
        [Test]
        public void TfootneteCoasdauntsaasdasddasdonAuthorsl()
        {
            Book = new Book("name", "author");
            Book.AddFootnote(5, "str");
            Book.AddFootnote(6, "sr");
            ;
            Assert.Throws<InvalidOperationException>(() => Book.AlterFootnote(1, "sssss"));
        }
     
    }
}