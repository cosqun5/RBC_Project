using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SelectPdf;
using System.Data;
using System.Text;

namespace RBCProjectMVC.Controllers
{
    public class ExportPdfController : Controller
    {
        private readonly IConfiguration _configuration;

        public ExportPdfController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult ExportPdf()
        {
            var connectionString = _configuration.GetConnectionString("Default");

            var dt = new DataTable();
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM vw_EmployeesForExport", conn))
                using (var adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }

            var sb = new StringBuilder();
            sb.Append("<h1>Employees Report</h1>");
            sb.Append($"<p>Export Date: {DateTime.Now}</p>");
            sb.Append("<table border='1' cellpadding='5' cellspacing='0'>");
            sb.Append("<thead><tr>");

            foreach (DataColumn col in dt.Columns)
            {
                sb.Append($"<th>{col.ColumnName}</th>");
            }

            sb.Append("</tr></thead><tbody>");

            foreach (DataRow row in dt.Rows)
            {
                sb.Append("<tr>");
                foreach (var item in row.ItemArray)
                {
                    sb.Append($"<td>{item}</td>");
                }
                sb.Append("</tr>");
            }

            sb.Append("</tbody></table>");
            sb.Append("<p style='margin-top:20px;'>Signature: ____________________</p>");

            var pdf = new HtmlToPdf();
            var doc = pdf.ConvertHtmlString(sb.ToString());

            byte[] pdfBytes = doc.Save();

            var exportDir = Path.Combine(Directory.GetCurrentDirectory(), "Export");
            Directory.CreateDirectory(exportDir);
            var fileName = $"Employees_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
            var filePath = Path.Combine(exportDir, fileName);
            System.IO.File.WriteAllBytes(filePath, pdfBytes);

            doc.Close();

            return File(pdfBytes, "application/pdf", fileName);
        }
    }
}
