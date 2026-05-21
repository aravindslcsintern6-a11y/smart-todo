// This file runs nmap and the results saved into 
// networkscans table in a database smarttodo in postgresql
// And the backend sends result back
// Then Angular displays result + history 
// This is the API entry point

using Microsoft.AspNetCore.Mvc;
using backend_dotnet.Data;
using backend_dotnet.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace backend_dotnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NetworkController : ControllerBase//creates API endpoint
    {
        private readonly AppDbContext _context;

        public NetworkController(AppDbContext context)//Connects PostgreSQL database
        {
            _context = context;//_context is used to save and read data

        }

        [HttpPost("scan")]// scan endpoint
        public async Task<IActionResult> Scan([FromBody] TargetRequest request)
        //API URL: POST /api/network/scan, 
        // input(from angular) {target:"scanme.nmap.org"}]
        {
            if (string.IsNullOrWhiteSpace(request.Target)) //validation: if user send empty target->error
                return BadRequest("Target required");

            // Run Nmap
            var process = new Process();
            process.StartInfo.FileName = @"C:\Program Files (x86)\Nmap\nmap.exe";//This launches nmap tool installed in our pc
            process.StartInfo.Arguments = request.Target;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;

            //Capture Output[Runs the scan>>Captures the result as text]
            process.Start();
            string output = await process.StandardOutput.ReadToEndAsync();
            process.WaitForExit();

            //save to database
            var scan = new NetworkScan
            {
                Target = request.Target,
                ScanResult = output,
                ScannedAt = DateTime.UtcNow
            };

            _context.NetworkScans.Add(scan);
            await _context.SaveChangesAsync();
            //send response back
            return Ok(scan);
        }
//get history
        [HttpGet("history")]
        public async Task<IActionResult> GetHistory()
        {
            //fetch from DB
            var scans = await _context.NetworkScans
                .OrderByDescending(x => x.ScannedAt)
                .ToListAsync();
            //return data
            return Ok(scans);
        }
    }
    //Request Model
    public class TargetRequest
    {
        public string Target { get; set; } = string.Empty;
    }
}