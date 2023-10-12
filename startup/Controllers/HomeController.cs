using Microsoft.AspNetCore.Mvc;
using startup.Models;
using System.Diagnostics;

namespace startup.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;

        public HomeController(ILogger<HomeController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        //khi vào route: / thì render ra giao diện trang chủ
        public IActionResult Index()
        {
            return View();
        }

        // khi vào route: /privacy thì render ra giao diện trang privacy
        [Route("/privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        // route này tạo ra để test
        [Route("/test")]
        public IActionResult Test()
        {
            return View();
        }
       

        // ví dụ của route naỳ: /post-slug-2.html hoặc /post-bai-viet-2.html
        [Route("/post-{slug}-{id:long}.html",Name = "Details")]
        public IActionResult Details(long? id)
        {
            // kiểm tra xem có id từ param truyền vào k
            if( id == null)
            {
                return NotFound();
            }
            // tìm bài post có id =id truyền vào từ param và IsActive = true
            var post = _context.Posts.FirstOrDefault(m=>(m.PostID == id) && (m.IsActive == true));
            // nếu không có bài post thì
            if(post == null )
            {
                return NotFound();
            }
            // nếu có bài post thì chuyển dữ liệu bài post qua Views/Home/Details.cshtml
            return View(post);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}