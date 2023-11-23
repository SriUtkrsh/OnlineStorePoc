using Microsoft.AspNetCore.Mvc;
using OnlineStorePoc.Models;
using OnlineStorePoc.Models.NewFolder2;
using System.Diagnostics;

namespace OnlineStorePoc.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IHOmeRepository _homeRepository;

		public HomeController(ILogger<HomeController> logger, IHOmeRepository homeRepository)
		{
			_homeRepository = homeRepository;              //Inject Repository to Controller
			_logger = logger;
		}

		public async Task< IActionResult>Index(string sTerm="", int genreId=0)
		{
            
			IEnumerable<Book> books = await _homeRepository.GetBooks(sTerm, genreId);
			IEnumerable<Genre> genres = await _homeRepository.Genres();
			BookDisplayModel bookModel = new BookDisplayModel
			{
				Books = books,
				Genres = genres,
				STerm = sTerm,
				GenreId = genreId
			};

            return View(bookModel);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}