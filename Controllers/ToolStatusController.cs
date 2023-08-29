using Microsoft.AspNetCore.Mvc;
using TSMonitor2.Models;

namespace TSMonitor2.Controllers
{
    public class ToolStatusController : Controller
    {
        private static ToolStatus _toolStatus = new ToolStatus();

        public IActionResult Index()
        {
            return View(_toolStatus);
        }

        public class ToolUpdateRequest
        {
            public string ToolName { get; set; }
        }
        //test
        // In your controller action
        [HttpPost]
        public IActionResult UpdateToolStatus([FromBody] ToolUpdateRequest request)
        {
            string toolName = request.ToolName;
            bool statusUpdated = false;

            if (request.ToolName == "A2A")
            {
                _toolStatus.LastRequestTimeA2A = DateTime.Now;
                statusUpdated = true;
            }
            else if (request.ToolName == "TS1")
            {
                _toolStatus.LastRequestTimeTS1 = DateTime.Now;
                statusUpdated = true;
            }
            else if (request.ToolName == "Bundle")
            {
                _toolStatus.LastRequestTimeBundle = DateTime.Now;
                statusUpdated = true;
            }
            else
            {
                return Json(new { success = false, message = "Invalid tool name." });
            }

            if (statusUpdated)
            {
                DateTime updatedTime;
                switch (request.ToolName)
                {
                    case "A2A":
                        updatedTime = _toolStatus.LastRequestTimeA2A;
                        break;
                    case "TS1":
                        updatedTime = _toolStatus.LastRequestTimeTS1;
                        break;
                    case "Bundle":
                        updatedTime = _toolStatus.LastRequestTimeBundle;
                        break;
                    default:
                        updatedTime = DateTime.Now;
                        break;
                }

                string message = $"Tool status for {request.ToolName} updated successfully. Last request time: {updatedTime}";
                return Json(new { success = true, message });
            }
            else
            {
                return Json(new { success = false, message = "No changes were made to the tool status." });
            }
        
    }





        [HttpGet] // Add this attribute to specify that this action responds to GET requests
        public IActionResult GetCurrentStatus()
        {
            // Create a dictionary to hold tool names and their corresponding last request times
            var toolStatuses = new Dictionary<string, DateTime>
            {
                { "A2A", _toolStatus.LastRequestTimeA2A },
                { "TS1", _toolStatus.LastRequestTimeTS1 },
                { "Bundle", _toolStatus.LastRequestTimeBundle }
            };

            // Return the tool statuses as JSON
            return Json(toolStatuses);
        }
    }
}
