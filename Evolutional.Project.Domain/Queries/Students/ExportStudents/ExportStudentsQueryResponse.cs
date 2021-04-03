using Evolutional.Project.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evolutional.Project.Domain.Queries.Students.ExportStudents
{
    public class ExportStudentsQueryResponse : DownloadResponse
    {
        public ExportStudentsQueryResponse(
            byte[] dataToDownload) : base(dataToDownload, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Students")
        {
        }
    }
}
