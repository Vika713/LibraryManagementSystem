using AutoMapper;
using Business.Books;
using Business.Books.DTOs;
using Common.Enumeration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Web.ViewModels.Books;

namespace Web.Controllers
{
    public partial class BooksController : Controller
    {
        private readonly IBookQueriesService _bookQueriesService;
        private readonly IBookUpdateService _bookUpdateService;
        private readonly IMapper _mapper;

        public BooksController(IBookQueriesService queriesService, IBookUpdateService updateService, IMapper mapper)
        {
            _bookQueriesService = queriesService;
            _bookUpdateService = updateService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        public virtual IActionResult Index(BooksIndexViewModel model)
        {
            if (ModelState.IsValid)
            {
                BooksFilterDTO filter = new BooksFilterDTO()
                {
                    Title = model.TitleFilter,
                    Author = model.AuthorFilter,
                    Subject = model.SubjectFilter,
                    PublicationDate = model.PublicationDateFilter
                };

                List<BooksIndexItemDTO> indexItems = _bookQueriesService
                    .GetIndexItems(filter, model.PageNumber, model.PageSize);

                model.Index = _mapper.Map<List<BookItemViewModel>>(indexItems);

                model.SetPaginationParameters(
                    _bookQueriesService.GetItemsCount(filter), model.PageNumber, model.PageSize);
            }

            return View(model);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Librarian)]
        public virtual ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Librarian)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(ISBNCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_bookQueriesService.BookExists(model.ISBN))
                {
                    return RedirectToAction(
                        nameof(MVC.BookItems.Create), nameof(MVC.BookItems), new { id = _bookQueriesService.GetBookId(model.ISBN) });
                }
                else
                {
                    BookCreateDTO createDTO = _bookQueriesService.GetCreateDTO(model.ISBN);
                    BookCreateViewModel createModel = _mapper.Map<BookCreateViewModel>(createDTO);

                    return View(Views.CreateBook, createModel);
                }
            }
            return View(model);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Librarian)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult CreateBook(BookCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                BookCreateDTO createDTO = _mapper.Map<BookCreateDTO>(model);
                Guid id = _bookUpdateService.Create(createDTO);

                return RedirectToAction(
                    nameof(MVC.BookItems.Create),
                    nameof(MVC.BookItems),
                    new { id = id });
            }
            return View(model);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Librarian)]
        public virtual ActionResult Edit(Guid id)
        {
            BookEditDTO editDTO = _bookQueriesService.GetEditDTO(id);
            BookEditViewModel model = _mapper.Map<BookEditViewModel>(editDTO);

            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Librarian)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(BookEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                BookEditDTO editDTO = _mapper.Map<BookEditDTO>(model);
                _bookUpdateService.Edit(editDTO);
                return RedirectToAction(nameof(Index)); ;
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult AddAuthorName(BookCreateViewModel book)
        {
            book.AuthorsNames.Add(new AuthorName());
            return PartialView(Views.AuthorsNames, book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult RemoveAuthorName(BookCreateViewModel book, int id)
        {
            book.AuthorsNames.Remove(new AuthorName());
            return PartialView(Views.AuthorsNames, book);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Librarian)]
        public virtual ActionResult Delete(Guid id)
        {
            BookDeleteDTO deleteDTO = _bookQueriesService.GetDeleteDTO(id);
            BookDeleteViewModel model = _mapper.Map<BookDeleteViewModel>(deleteDTO);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Librarian)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Delete(BookDeleteViewModel model)
        {
            if (ModelState.IsValid)
            {
                _bookUpdateService.Delete(model.BookId);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
    }
}
