using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebAPIClient.Repository;
using WebAPIClient.ViewModel;

namespace WebAPIClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        readonly IService cardService;

        public HomeController(IService context)
        {
            cardService = context;
        }

        [HttpGet]
        public List<CardModel> Get()
        {
            return cardService.GetAllCard();
        }


        [HttpPost]
        public ActionResult Post([FromBody] CardModel card)
        {
            try
            {
                cardService.CreateCard(card);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] CardModel card)
        {
            try
            {
                cardService.UpdateCard(id, card);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                cardService.DeleteCard(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
