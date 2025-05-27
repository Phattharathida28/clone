using System.Collections.Generic;
using System.Linq;
using Application.Features.DB.DBRT01;
using DocumentFormat.OpenXml.Bibliography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.WebAPI_Example
{
    public class QORT01Controller : BaseController
    //{
    //    public class Book
    //    {
    //        public int Id { get; set; }
    //        public string Title { get; set; } = string.Empty;
    //        public string Author { get; set; } = string.Empty;
    //        public int Year { get; set; }

    //    }

    //    private static List<Book> books = new()
    //    {
    //        new Book { Id = 1,Title = "The Hobbit", Author = "J.R.R. Tolkien", Year = 1937 },
    //        new Book { Id = 2,Title = "1984", Author = "George Orwell", Year = 1949}
    //    };
    //    [AllowAnonymous]
    //    [HttpGet]
    //    public IActionResult GetAll() => Ok(books);

    //    [AllowAnonymous]
    //    [HttpGet("{id}")]
    //    public IActionResult Get(int id)
    //    {
    //        var book = books.FirstOrDefault(b => b.Id == id);
    //        if (book == null) return NotFound();
    //        return Ok(book);
    //    }

    //    [AllowAnonymous]
    //    [HttpPost]
    //    public IActionResult Create(Book book)
    //    {
    //        book.Id = books.Max(b => b.Id) + 1;
    //        books.Add(book);
    //        return CreatedAtAction(nameof(Get), new { id = book.Id }, book);
    //    }

    //    [AllowAnonymous]
    //    [HttpPut("{id}")]
    //    public IActionResult Update(int id, Book updateBook)
    //    {
    //        var book = books.FirstOrDefault(b => b.Id == id);
    //        if (book == null) return NotFound();

    //        book.Title = updateBook.Title;
    //        book.Author = updateBook.Author;
    //        book.Year = updateBook.Year;

    //        return NoContent();
    //    }

    //    [AllowAnonymous]
    //    [HttpDelete]
    //    public IActionResult Delete(int id)
    //    {
    //        var book = books.FirstOrDefault(b => b.Id == id);
    //        if (book == null) return NotFound();

    //        books.Remove(book);
    //        return NoContent();
    //    }

    //}
    {
        public class CartItem
        {
            public int Id { get; set; }
            public string ProductName { get; set; } = string.Empty;
            public int Quantity { get; set; }
            public decimal Price { get; set; }
        }

        private static List<CartItem> cart = new()
        {
            new CartItem { Id = 1, ProductName = "The Hobbit", Quantity = 200, Price = 1937},
        };

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll() => Ok(cart);

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var items = cart.FirstOrDefault(c => c.Id == id);
            if (items == null) return NotFound();
            return Ok(cart);
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Create(CartItem items)
        {
            items.Id = cart.Max(c => c.Id) + 1;
            cart.Add(items);
            return CreatedAtAction(nameof(Get), new { id = items.Id }, items);
        }

        [AllowAnonymous]
        [HttpPut("{id}")]
        public IActionResult Update(int id, CartItem updateCart)
        {
            var items = cart.FirstOrDefault(c => c.Id == id);
            if (items == null) return NotFound();

            items.ProductName = updateCart.ProductName;
            items.Quantity = updateCart.Quantity;
            items.Price = updateCart.Price;

            return NoContent();
        }

        [AllowAnonymous]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var items = cart.FirstOrDefault(c => c.Id == id);
            if (items == null) return NotFound();

            cart.Remove(items);
            return NoContent();
        }
        [AllowAnonymous]
        [HttpGet("total")]
        public IActionResult GetTotal()
        {
            var totals = cart.Select(c => new
            {
                c.Id,
                c.ProductName,
                c.Quantity,
                c.Price,
                Total = c.Price * c.Quantity
            });

            return Ok(totals);
        }
    }
}
