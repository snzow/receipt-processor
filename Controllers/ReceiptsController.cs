using Microsoft.AspNetCore.Mvc;
using ReceiptProcessor.Managers;
using ReceiptProcessor.Models;

namespace ReceiptProcessor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReceiptsController : ControllerBase
    {
        private readonly ILogger<ReceiptsController> _logger;
        private readonly IReceiptManager _receiptManager;

        public ReceiptsController(ILogger<ReceiptsController> logger, IReceiptManager receiptManager)
        {
            _logger = logger;
            _receiptManager = receiptManager;
        }

        // POST: receipts/process
        [HttpPost("process")]
        public IActionResult PostReceipt([FromBody] Receipt receipt)
        {
            try
            {
                string id = _receiptManager.SaveReceipt(receipt);
                return Ok(new { id = id } );
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST: receipts/{id}/points
        [HttpGet("{id}/points")]
        public IActionResult GetPoints(string id)
        {
            try
            {
                int? points = _receiptManager.GetPoints(id);
                if(points == null)
                {
                    return BadRequest();
                }
                return Ok(new { points = points});
            } 
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
