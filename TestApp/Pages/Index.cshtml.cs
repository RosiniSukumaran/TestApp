using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        //public void OnGet()
        //{
        //    //RestClient client = new RestClient("http://localhost:58179/api/");
        //    //RestRequest request = new RestRequest("Default",Method.GET);
        //    var client = new RestClient("https://demoappdesapi.azure-api.net/DemoApp4DES/GetKeyVaultSecret?SecretName=SecretName");
        //    var request = new RestRequest(Method.GET);
        //    request.AddHeader("Ocp-Apim-Subscription-Key", "6a724be531ab4a1a91fbcdc0a31c7de4");
        //    IRestResponse response = client.Execute(request);
        //    if (response.IsSuccessful)
        //    {
        //        ViewData.Add("Secret", response.Content);
        //    } 


        //}

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string Msg { get; set; }

        public void OnGet()
        {
            var client = new RestClient("https://demoappdesapi.azure-api.net/DemoApp4DES/GetKeyVaultSecret?SecretName=SecretName");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Ocp-Apim-Subscription-Key", "6a724be531ab4a1a91fbcdc0a31c7de4");
            IRestResponse response = client.Execute(request);
            Password=response.Content;
            if (response.IsSuccessful)
            {
                ViewData.Add("Secret", response.Content);
            }
        }

        public IActionResult OnPost()
        {
            if (Username.Equals("Test") && Password.Equals("Test"))
            {
                HttpContext.Session.SetString("username", Username);
                return RedirectToPage("Welcome");
            }
            else
            {
                Msg = "Invalid";
                return Page();
            }
        }
    }
}
