using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library.Persistence;
using Library.Persistence.Services;

namespace Library.Web.Controllers
{
    public class BooksController : Controller
    {
        private readonly int pagesize = 20;
        private readonly ILibraryService _service;

        public BooksController(ILibraryService service)
        {
            _service = service;
        }
        public IActionResult Index(ILibraryService.SortOrder sortOrder = ILibraryService.SortOrder.FAME_DESC,int id=0,string filter=" ")
        {
            var book = _service.GetBooks();
            ViewData["Filter"] = filter;

            
            ViewData["PageCount"] = book.Count / pagesize + 1;
            ViewData["NameSortParam"] = sortOrder == ILibraryService.SortOrder.NAME_DESC ? ILibraryService.SortOrder.NAME_ASC : ILibraryService.SortOrder.NAME_DESC;
            ViewData["FameSortParam"] = sortOrder == ILibraryService.SortOrder.FAME_DESC ? ILibraryService.SortOrder.FAME_ASC : ILibraryService.SortOrder.FAME_DESC;
            if(filter==null)
            {
                return View(_service.GetBooks("", sortOrder).Skip(id * pagesize).Take(pagesize));
            }
            return View(_service.GetBooks("",sortOrder).Where(b => b.Name.Contains(filter) || b.Author.Contains(filter)).Skip(id * pagesize).Take(pagesize));
        }
        public IActionResult Details(int id)
        {
            try
            {
                var book = _service.GetBookDetails(id);
               
                
                return View(book);
            }
            catch
            {
                return NotFound();
            }         
        }
        public IActionResult DisplayImage(int id)
        {
            var book =  _service.GetBookByID(id);
            if(book == null)
            {
                return null;
            }
            return File(book.Image, "image/png");
        }
    }
}
