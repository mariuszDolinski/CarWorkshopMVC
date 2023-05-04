using AutoMapper;
using CarWorkhhop.MVC.Extensions;
using CarWorkshop.Application.CarWorkshop.Commands.CreateCarWorkshop;
using CarWorkshop.Application.CarWorkshop.Commands.EditCarWorkshop;
using CarWorkshop.Application.CarWorkshop.Queries.GetAllCarWorkshops;
using CarWorkshop.Application.CarWorkshop.Queries.GetCarWorkshopByName;
using CarWorkshop.Application.CarWorkshopService.Commands;
using CarWorkshop.Application.CarWorkshopService.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarWorkhhop.MVC.Controllers
{
    public class CarWorkshopController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        //private readonly RoleManager<IdentityRole> _roleManager;

        public CarWorkshopController(IMediator mediator, IMapper mapper) 
        {
            _mediator = mediator;
            _mapper = mapper;
            //_roleManager = roleManager;
        }

        //Aby zachować rozdział poszczególnych modułów tworzymy serwis w module Application.
        //Aby z kolei w tym serwisie korzystać z kontekstu bazy danych, która jest w Infrastructure
        //tworzymy interfejs ICarWorkshopRespository w module Domain a potem  w Infrastructure implementujemy go
        //(folder Repositories) kozystając z dBcontextu. Korzystamy z tego interfejsu w serwisie aby skorzystać
        //z implememtacji repozytorium. Na końcu poprzez wydzielenie interfejsu serwisu wstrzykujemy go tutaj przez
        //konstruktor. Dodatkowo trzeba pamiętać o zarejestrowaniu wszystkich tych serwisów odpowiednio przez
        //metody rozszerzające definiowane w folderach Extensions w Application i Infrastructure. Na koniec te metody
        //rozszerzające wywołujemy w Program.cs w module MVC
        //
        //implemetujemy wzorzec CQRS, który oddziela odpowiedzialności zapisu do bazy danych (commands) i odczytu (queries)
        //używamy do tego paczki Mediatr...DependencyInjcetions, dzięki temu pozbywamy się serwisu a metody z niego rozdzielone są
        //według opowidzialności na komendy i kwerendy (patrzfoldery command i queries w application)
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateCarWorkshopCommand command)
        {
            if(!ModelState.IsValid)
            {
                return View(command);//jeśli walidacja się nie powiedzie, zwracamy ten sam widok
            }
            await _mediator.Send(command);

            //tworzymy metodę rozszerzającą do wysłania do wisoku info o notyfikacji (folder Extensions)
            //dodatowo tworzymy klasę Notification do przechowywyania danych o notyfikacji
            //obsługę notyfikacji twoezymy w widoku Layout
            //używmy zewnetrznej paczki toastr.js, którą trzeba zainstalować
            //w tym celu klikamy PPM na wwwroot/lib i wybueramy add->client-side-library 
            //i tam wyszukujemy tą paczkę, provider dowolny
            this.SetNotification("success", $"Created carworkshop {command.Name}");

            return RedirectToAction(nameof(Index));    
        }
        [Authorize]//dopuszcza do akcji tylko zalogowanych userów, w przeciwnym razie wyświetla stronę logowania
        public ActionResult Create()
        { 
            return View();
        }

        [Route("CarWorkshop/{encodedName}/Details")]//podajemy adres url do akcji, param w {} jest brany z parametru akci
        public async Task<IActionResult> Details(string encodedName)
        {
            var dto = await _mediator.Send(new GetCarWorkshopByNameQuery(encodedName));
            return View(dto);
        }

        [Route("CarWorkshop/{encodedName}/Edit")]
        public async Task<IActionResult> Edit(string encodedName)
        {
            var dto = await _mediator.Send(new GetCarWorkshopByNameQuery(encodedName));

            if(!dto.IsEditable)
            {
                return RedirectToAction("NoAccess", "Home");
            }

            var model = _mapper.Map<EditCarWorkshopCommand>(dto);
            return View(model);
        }

        [HttpPost]
        [Route("CarWorkshop/{encodedName}/Edit")]
        public async Task<IActionResult> Edit(string encodedName, EditCarWorkshopCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Index()
        {
            var carWorkshops = await _mediator.Send(new GetAllCarWorkshopsQuery());
            //widok można wygenerować automatycznie, ppm>add view>razor view>podajemy list i nazwę dto
            return View(carWorkshops);
        }

        //akcja do dodawania usług warsztatów. Dodawanie jest możliwe w edycji warsztatu
        //tam na dole mamy dodany partialview a w nim formlarz w modalu z bootstrapa (wyskakujące okienko)
        //nie zwracamy widoków tylko kody badrequest lub ok aby strona nie wczytywała się ponowanie
        [HttpPost]
        [Route("CarWorkshop/CarWorkshopService")]
        public async Task<IActionResult> CreateCarWorkshopService(CreateCarWorkshopServiceCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _mediator.Send(command);

            return Ok();
        }

        [HttpGet]
        [Route("CarWorkshop/{encodedName}/CarWorkshopService")]
        public async Task<IActionResult> GetCarWorkshopServices(string encodedName)
        {
            var data = await _mediator.Send(new GetCarWorkshopServicesQuery() { EncodedName = encodedName });
            return Ok(data);
        }
    }
}
