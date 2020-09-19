using System;
using System.IO;
using System.Threading.Tasks;
using BookRepo.data.Repository;
using BookRepo.wwwMain.Models.Books;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;

namespace BookRepo.wwwMain.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class BookController : ControllerBase {
		private readonly IBookRepo _bookRepo;
		private readonly ILogger _log;

		public BookController(ILogger<BookController> log, IBookRepo bookRepo) {
			_log = log;
			_bookRepo = bookRepo;
		}

		[HttpGet("")]
		public async Task<IActionResult> ListBooks() {
			try {
				var retval = await _bookRepo.GetAll(BookSummaryModel.GetMap());
				return Ok(retval);
			} catch (Exception ex) {
				_log.LogError(ex, ex.Message);
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("{bookId}")]
		public async Task<IActionResult> GetBookDetail(string bookId) {
			try {
				var id = ObjectId.Parse(bookId);
				var retval = await _bookRepo.GetOne(id, BookDetailModel.GetMap()) ?? throw new ArgumentException($"Invalid id {bookId}");
				return Ok(retval);
			} catch (ArgumentException ex) {
				_log.LogWarning(ex, ex.Message);
				return BadRequest(ex.Message);
			} catch (Exception ex) {
				_log.LogError(ex, ex.Message);
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("{bookId}/cover")]
		public async Task<IActionResult> GetBookCover(string bookId) {
			try {
				var id = ObjectId.Parse(bookId);
				var retval = await _bookRepo.GetOne(id, BookImageModel.GetMap()) ?? throw new ArgumentException($"Invalid id {bookId}");
				var str = new MemoryStream(retval.Cover);
				return File(str, "image/jpeg");
			} catch (ArgumentException ex) {
				_log.LogWarning(ex, ex.Message);
				return BadRequest(ex.Message);
			} catch (Exception ex) {
				_log.LogError(ex, ex.Message);
				return StatusCode(500, ex.Message);
			}
		}
	}
}
