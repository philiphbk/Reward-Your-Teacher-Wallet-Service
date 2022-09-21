using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Wallet.Core.Interfaces;
using Wallet.Utilties;

namespace Wallet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly UrlConfiguration _walletOptions;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<TestController> _logger;
        private ITestServices _testService;

        public TestController(
            ITestServices testService,
            ILogger<TestController> logger,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            IOptionsSnapshot<UrlConfiguration> walletoptions)
        {
            _walletOptions = walletoptions.Value;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _testService = testService;
        }

        [HttpGet]
        public async Task<IActionResult> Banks()
        {
            
            var result = await _testService.GetBanksAsync();
            
            return Ok(result);
                
        }
    }
}
