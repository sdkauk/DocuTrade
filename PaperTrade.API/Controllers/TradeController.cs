using Microsoft.AspNetCore.Mvc;
using PaperTrade.BusinessLogic.Services;
using PaperTrade.BusinessLogic.Services.Briefs.Requests;
using PaperTrade.BusinessLogic.Services.Trades.Requests;
using PaperTrade.Common.Models;

namespace PaperTrade.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TradeController : ControllerBase
    {
        private readonly ITradeService tradeService;

        public TradeController(ITradeService tradeService)
        {
            this.tradeService = tradeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trade>>> Get()
        {
            var trades = await tradeService.GetAllTradesAsync();
            return Ok(trades);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<Trade>> Get(Guid id)
        {
            var trade = await tradeService.GetTradeAsync(id);
            return Ok(trade);
        }

        [HttpPost]
        public async Task<ActionResult<Trade>> Post(TradePostRequest request)
        {
            var trade = await tradeService.CreateTradeAsync(request);
            return CreatedAtAction(nameof(Get), new { id = trade.Id }, trade);
        }

        [HttpPut]
        public async Task<IActionResult> Put(TradePutRequest request)
        {
            var trade = await tradeService.UpdateTradeAsync(request);
            return Ok(trade);
        }

        [HttpPost("{id}/accept")]
        public async Task<IActionResult> Accept(Guid id)
        {
            var trade = await tradeService.AcceptTradeAsync(id);
            return Ok(trade);
        }

        [HttpPost("{id}/reject")]
        public async Task<IActionResult> Reject(Guid id)
        {
            var trade = await tradeService.DeclineTradeAsync(id);
            return Ok(trade);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await tradeService.DeleteTradeAsync(id);
            return NoContent();
        }
    }
}
